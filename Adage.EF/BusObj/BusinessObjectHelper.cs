using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Adage.EF.Interfaces;
using System.Data.Common;
using System.Data.Metadata.Edm;
using System.Reflection;
using System.Data.Objects.DataClasses;

namespace Adage.EF.BusObj
{
    public static class BusinessObjectHelper
    {
        public static object ReadObjectValue(IGenericBusinessObj obj, int index, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = obj.GetTableFields(currentEntry);
            if (index < 0 || index > tableFields.Count)
                throw new ApplicationException(string.Format(
                    "Could not find the index:{0} in {1}",
                    index, obj.GetType().ToString()));

            BusinessObjectStructure fieldToGet = tableFields[index];

            if (fieldToGet.IsCSpaceColumn)
                return currentEntry.CurrentValues.GetValue(fieldToGet.CSpaceIndex.Value);
            else
                return fieldToGet.PropertyInfo.GetValue(obj, null);
        }

        public static object ReadObjectValue(IGenericBusinessObj obj, string name, ObjectStateEntry currentEntry)
        {
            List<BusinessObjectStructure> tableFields = obj.GetTableFields(currentEntry);
            Adage.EF.Interfaces.BusinessObjectStructure fieldToGet = tableFields.Where(c => c.Name == name).FirstOrDefault();

            if (fieldToGet == null)
                throw new ApplicationException("Could not find the property:" + name);

            if (fieldToGet.IsCSpaceColumn)
                return currentEntry.CurrentValues.GetValue(fieldToGet.CSpaceIndex.Value);
            else
                return fieldToGet.PropertyInfo.GetValue(obj, null);
        }

        public static List<BusinessObjectStructure> GetTableFields(Type currentType, ObjectStateEntry entry)
        {
            if (entry.Entity.GetType() != currentType)
                throw new ApplicationException(string.Format(
                    "GetTableFields was called for '{0}' with a '{1}'",
                    currentType, entry.Entity.GetType()));

            List<BusinessObjectStructure> _tableFields = new List<BusinessObjectStructure>();

            EntityType currentEntityType = GetEntityType(currentType, entry);
            // find all properities in the cspace
            foreach (FieldMetadata prop in entry.CurrentValues.DataRecordInfo.FieldMetadata)
            {
                EdmProperty prop2 = currentEntityType.Properties[prop.FieldType.Name];
                if (prop.FieldType.BuiltInTypeKind != BuiltInTypeKind.ComplexType)
                {
                    PropertyInfo currentProperty = GetPropertyInfo(currentType, prop2.Name);

                    // don't add hidden properities
                    if (currentProperty == null)
                        continue;

                    _tableFields.Add(new Adage.EF.Interfaces.BusinessObjectStructure(
                        currentProperty.PropertyType,
                        prop.FieldType.Name, prop2.Nullable, 0,
                        true, prop.Ordinal, currentProperty));
                }
            }

            //find all properities marked as DataObjectField
            PropertyInfo[] props;
            props = currentType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (System.Reflection.PropertyInfo propInfo in props)
            {
                AddDataObjectFieldProperties(_tableFields, propInfo);
            }

            PropertyInfo[] childProps;
            childProps = currentType.BaseType.GetProperties(BindingFlags.Instance |
              BindingFlags.FlattenHierarchy | BindingFlags.Public);
            foreach (PropertyInfo propInfo in childProps)
            {
                AddDataObjectFieldProperties(_tableFields, propInfo);
            }

            return _tableFields;
        }

        private static EntityType GetEntityType(Type currentType, ObjectStateEntry currentEntry)
        {
            EntityType currentEType = (EntityType)currentEntry.EntitySet.ElementType;

            // if entity set matches the typename return it
            if (currentEType.Name.EndsWith(currentEntry.Entity.GetType().Name))
                return currentEType;

            currentEType = GetCSpacetype(currentType, currentEntry.ObjectStateManager.MetadataWorkspace);
            if (currentEType == null)
            {
                throw new ApplicationException(string.Format(
                    "Could not load the EntityType for: {0}.  " +
                    "It is probably an inherited type and the MetatDataWorkspace hasn't been loaded yet.  " +
                    "Run a query against the context before running the operation.",
                    currentType.Name));
            }
            return currentEType;
        }

        public static EntityType GetCSpacetype(Type currentType, MetadataWorkspace mdw)
        {
            mdw.LoadFromAssembly(currentType.Assembly);

            EntityType ospaceEntityType = null;
            StructuralType cspaceEntityType = null;
            if (mdw.TryGetItem<EntityType>(
                currentType.FullName, DataSpace.OSpace, out ospaceEntityType))
            {
                if (mdw.TryGetEdmSpaceType(ospaceEntityType,
                    out cspaceEntityType))
                    return cspaceEntityType as EntityType;
            }

            return null;
        }

        static readonly Type dataPropType = typeof(System.ComponentModel.DataObjectFieldAttribute);

        /// <summary>
        /// This method adds the fields only if they do not already exist in the array
        /// </summary>
        /// <param name="_tableFields"></param>
        /// <param name="propInfo"></param>
        private static void AddDataObjectFieldProperties(List<Adage.EF.Interfaces.BusinessObjectStructure> _tableFields, System.Reflection.PropertyInfo propInfo)
        {
            Adage.EF.Interfaces.BusinessObjectStructure currStruct;
            System.ComponentModel.DataObjectFieldAttribute dataPropInfo;

            if (propInfo.IsDefined(dataPropType, true))
            {
                bool alreadyContainsProperty = false;
                _tableFields.ForEach(p =>
                    alreadyContainsProperty = p.Name == propInfo.Name
                    ? true : alreadyContainsProperty);

                if (!alreadyContainsProperty)
                {
                    dataPropInfo = (System.ComponentModel.DataObjectFieldAttribute)Attribute.GetCustomAttribute(propInfo, dataPropType);
                    currStruct = new Adage.EF.Interfaces.BusinessObjectStructure(
                        propInfo.PropertyType, propInfo.Name, dataPropInfo.IsNullable,
                        dataPropInfo.Length, false, null, propInfo);

                    _tableFields.Add(currStruct);
                }
            }
        }

        private static PropertyInfo GetPropertyInfo(Type type, string name)
        {
            return type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance, null, null, Type.EmptyTypes, null);
        }

        public static void GetBrokenRulesString(IBusinessObject currentObj,
                List<IBusinessObject> checkEntities,
                List<string> errorMessages,
                System.Data.Objects.DataClasses.EntityObject parent)
        {
            if (checkEntities.Contains(currentObj))
                return;

            checkEntities.Add(currentObj);

            if (parent == null)
                errorMessages.Add(currentObj.GetCurrentBrokenRules());
            else
                errorMessages.Add(currentObj.GetCurrentBrokenRules());

            if (IsNew(currentObj))
                return;

            foreach (Adage.EF.Interfaces.RelatedObject eachObj in currentObj.RelatedObjects)
            {
                IRelatedEnd childColl;
                if (eachObj.RelatedType == RelatedEnum.Many)
                {
                    childColl = eachObj.GetRelatedEnd((EntityObject)currentObj);
                    if (childColl == null || childColl.IsLoaded == false)
                    {
                        continue;
                    }
                }
                else
                {
                    if (eachObj.GetReference((EntityObject)currentObj).IsLoaded == false)
                    {
                        continue;
                    }

                    childColl = eachObj.GetRelatedEnd((EntityObject)currentObj);
                }

                foreach (Adage.EF.Interfaces.IBusinessObject eachChild in childColl)
                {
                    eachChild.FindBrokenRules(checkEntities,
                        errorMessages, (EntityObject)currentObj);
                }
            }
        }

        public static bool IsNew(IBusinessObject currObj)
        {
            string UniqueKey = currObj.UniqueKey;

            return UniqueKey == string.Empty || UniqueKey == "0";
        }

        public static bool CheckIsValid(IBusinessObject currentObj,
                List<IBusinessObject> checkEntities)
        {
            if (checkEntities.Contains(currentObj))
                return true;

            checkEntities.Add(currentObj);
            foreach (Adage.EF.Interfaces.RelatedObject eachObj in currentObj.RelatedObjects)
            {
                IRelatedEnd childColl;
                if (eachObj.RelatedType == RelatedEnum.Many)
                {
                    childColl = eachObj.GetRelatedEnd((EntityObject)currentObj);
                    if (childColl == null || childColl.IsLoaded == false)
                    {
                        continue;
                    }
                }
                else
                {
                    if (eachObj.GetReference((EntityObject)currentObj).IsLoaded == false)
                    {
                        continue;
                    }

                    childColl = eachObj.GetRelatedEnd((EntityObject)currentObj);
                }

                foreach (IBusinessObject eachChild in childColl)
                {
                    if (eachChild.CheckIsValid(checkEntities) == false)
                        return false;
                }
            }

            return true;
        }
    }
}
