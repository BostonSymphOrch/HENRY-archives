using Adage.EF.Interfaces;
using System;
using System.Data;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Linq;
using System.Reflection;
using System.Web;
using Bso.Archive.BusObj.Utility;

namespace Bso.Archive.BusObj
{
    public partial class BsoArchiveEntities
    {
        private static BsoArchiveEntities _bsoArchiveEntity = null;
        public static BsoArchiveEntities Current
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Items["BsoArchiveEntities"] == null)
                        HttpContext.Current.Items.Add("BsoArchiveEntities", new BsoArchiveEntities());

                    return (BsoArchiveEntities)HttpContext.Current.Items["BsoArchiveEntities"];
                }

                if (_bsoArchiveEntity == null)
                    _bsoArchiveEntity = new BsoArchiveEntities();

                return _bsoArchiveEntity;
            }
        }

        /// <summary>
        /// Use Save to save changes so that we can abstract it out in future
        /// </summary>
        public void Save()
        {
            BsoArchiveEntities.Current.SaveChanges();
        }

        partial void OnContextCreated()
        {
            try
            {
                MetadataWorkspace.LoadFromAssembly(Assembly.GetAssembly(typeof(BsoArchiveEntities)));
            }
            catch (Exception ex) { }

            SavingChanges += new EventHandler(Entities_SavingChanges);
        }

        void Entities_SavingChanges(object sender, EventArgs e)
        {
            var stateManager = ((BsoArchiveEntities)sender).ObjectStateManager;

            var updatedEntities =
                stateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified).Where(
                    entities => entities.IsRelationship == false);

            int userIdentity = 0;

            // look for created / modified dates
            foreach (ObjectStateEntry stateEntryEntity in updatedEntities)
            {
                if (!(stateEntryEntity.Entity is Adage.EF.Interfaces.IBusinessObject))
                    return;

                IBusinessObject businessObject = (Adage.EF.Interfaces.IBusinessObject)stateEntryEntity.Entity;

                if (businessObject.UniqueKey == "0" && stateEntryEntity.State == EntityState.Modified)
                {
                    stateManager.ChangeObjectState(stateEntryEntity.Entity, EntityState.Added);

                    foreach (System.Data.Common.FieldMetadata md in stateEntryEntity.CurrentValues.DataRecordInfo.FieldMetadata.Where(
                            md => md.FieldType.TypeUsage.EdmType.Name.ToUpper() == "GUID"))
                        stateEntryEntity.CurrentValues.SetValue(md.Ordinal, Guid.NewGuid());
                }

                // update created on / by
                if (stateEntryEntity.State == EntityState.Added)
                {
                    SetDefaultValue(stateEntryEntity);
                    UpdateValue(stateEntryEntity, "CreatedOn", PrimitiveTypeKind.DateTime, DateTime.Now);
                    UpdateValue(stateEntryEntity, "CreatedBy", PrimitiveTypeKind.Int32, userIdentity);
                }

                // updated modified on / by
                UpdateValue(stateEntryEntity, "ModifiedOn", PrimitiveTypeKind.DateTime, DateTime.Now);
                UpdateValue(stateEntryEntity, "ModifiedBy", PrimitiveTypeKind.Int32, userIdentity);
            }

            // call Save on all business objects
            var changedEntities =
                stateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified | EntityState.Deleted).Where
                    (entities => entities.IsRelationship == false);

            foreach (ObjectStateEntry stateEntryEntity in changedEntities)
            {
                IBusinessObject businessObject = (Adage.EF.Interfaces.IBusinessObject)stateEntryEntity.Entity;
                businessObject.Save(this);
            }

        }

        internal static void UpdateObject(IBusinessObject busObject, object newValue, string columnName)
        {
            ObjectStateEntry stateEntry;
            BsoArchiveEntities.Current.ObjectStateManager.TryGetObjectStateEntry(busObject, out stateEntry);

            var createdProps = stateEntry.CurrentValues.DataRecordInfo.FieldMetadata;

            var prop = createdProps.FirstOrDefault(p => p.FieldType.Name.ToUpper() == columnName.ToUpper());

            if (prop.FieldType == null)
                return;

            var value = EFUtility.MapType(newValue, prop.FieldType.TypeUsage.EdmType);

            stateEntry.CurrentValues.SetValue(prop.Ordinal, value);

        }

        internal static void TestUpdate(IBusinessObject busObject, object newValue, string columnName)
        {
            ObjectStateEntry stateEntry;
            BsoArchiveEntities.Current.ObjectStateManager.TryGetObjectStateEntry(busObject, out stateEntry);

            var createdProps = stateEntry.CurrentValues.DataRecordInfo.FieldMetadata;

            var prop = createdProps.FirstOrDefault(p => p.FieldType.Name.ToUpper() == columnName.ToUpper());
            stateEntry.CurrentValues.SetValue(prop.Ordinal, newValue);
        }

        /// <summary>
        /// Used during save to upated created / modified dates
        /// </summary>
        /// <param name="stateEntryEntity">State entry to update</param>
        /// <param name="columnNameLike">what the column name should contain</param>
        /// <param name="typeToUpdateFor">The datatype to update</param>
        /// <param name="value">The value to update to</param>
        internal static void UpdateValue(ObjectStateEntry stateEntryEntity, string columnNameLike, PrimitiveTypeKind typeToUpdateFor, object value)
        {
            var createdProps = stateEntryEntity.CurrentValues.DataRecordInfo.FieldMetadata.Where(
              meta => meta.FieldType.Name.ToLower().Contains(columnNameLike.ToLower()));

            foreach (var eachProp in createdProps)
            {
                if (eachProp.FieldType.TypeUsage.EdmType.Name == typeToUpdateFor.ToString())
                    stateEntryEntity.CurrentValues.SetValue(eachProp.Ordinal, value);
            }
        }

        /// <summary>
        /// Set Default values for business objects
        /// </summary>
        /// <param name="newBusinessObject">Business Object</param>
        public static void SetDefaultValue(IBusinessObject newBusinessObject)
        {
            ObjectStateEntry stateEntry = null;
            Current.ObjectStateManager.TryGetObjectStateEntry(newBusinessObject, out stateEntry);
            SetDefaultValue(stateEntry);
        }

        private static void SetDefaultValue(ObjectStateEntry stateEntryEntity)
        {
            var createdProps = stateEntryEntity.CurrentValues.DataRecordInfo.FieldMetadata;

            foreach (var eachProp in createdProps)
            {
                //Add the field defaults: String gets NULL, DateTime is DateTime.MIN (01/01/0001)
                //Other fields seem to be fine (except bool which is set to false)

                if (eachProp.FieldType.Name.ToUpper() == "ACTIVE")
                {
                    stateEntryEntity.CurrentValues.SetValue(eachProp.Ordinal, true);
                    continue;
                }

                //There are some date fields that should be set to NULL - so we cannot default these
                //if (eachProp.FieldType.TypeUsage.EdmType.Name.ToUpper() == "DATETIME")
                //    stateEntryEntity.CurrentValues.SetValue(eachProp.Ordinal, DateTime.Now);

                if (eachProp.FieldType.TypeUsage.EdmType.Name.ToUpper() == "GUID"
                    && stateEntryEntity.CurrentValues.GetValue(eachProp.Ordinal).ToString() == Guid.Empty.ToString())
                    stateEntryEntity.CurrentValues.SetValue(eachProp.Ordinal, Guid.NewGuid());

                if (!stateEntryEntity.CurrentValues.IsDBNull(eachProp.Ordinal))
                    continue;

                if (eachProp.FieldType.TypeUsage.EdmType.Name.ToUpper() == "STRING")
                    stateEntryEntity.CurrentValues.SetValue(eachProp.Ordinal, String.Empty);


            }
        }
    }
}
