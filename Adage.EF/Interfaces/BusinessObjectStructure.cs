using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adage.EF.Interfaces
{
    public enum RelatedEnum
    {
        ZeroOne,
        One,
        Many
    }

    public delegate System.Data.EntityKey GetKey(string uniqueKey);

    public class RelatedObjectGen<T> : RelatedObject where T : System.Data.Objects.DataClasses.EntityObject
    {
        GetKey _getTargetKey;
        public RelatedObjectGen(string relationshipName, string targetRoleName, string NavigationPropertyName, 
            RelatedEnum relatedType, GetKey getTargetKey)
            : base(relationshipName, targetRoleName, NavigationPropertyName, relatedType)
        {
            _getTargetKey = getTargetKey;
        }

        public override System.Data.Objects.DataClasses.EntityReference GetReference(System.Data.Objects.DataClasses.IEntityWithRelationships currentObject)
        {
            return currentObject.RelationshipManager.GetRelatedReference<T>(RelationshipName, TargetRoleName);
        }

        public override System.Data.EntityKey GetTargetEntityKey(string currentKey)
        {
            return _getTargetKey.Invoke(currentKey);
        }
    }

    public abstract class RelatedObject
    {
        public readonly string RelationshipName;
        public readonly string TargetRoleName;
        public readonly string NavigationPropertyName;
        public readonly RelatedEnum RelatedType;

        public RelatedObject(string relationshipName, string targetRoleName, 
            string navigationPropertyName, RelatedEnum relatedType)
        {
            RelationshipName = relationshipName;
            TargetRoleName = targetRoleName;
            NavigationPropertyName = navigationPropertyName;
            RelatedType = relatedType;
        }

        public abstract System.Data.Objects.DataClasses.EntityReference 
            GetReference(System.Data.Objects.DataClasses.IEntityWithRelationships currentObject);

        public abstract System.Data.EntityKey GetTargetEntityKey(string currentKey);

        public override bool Equals(object obj)
        {
            // if the objects are equal
            RelatedObject otherObj = obj as RelatedObject;
            if (otherObj != null)
                return otherObj.RelationshipName == RelationshipName
                    && otherObj.TargetRoleName == TargetRoleName;

            System.Data.Objects.DataClasses.IRelatedEnd otherEnd = obj as System.Data.Objects.DataClasses.IRelatedEnd;
            if (otherEnd != null)
                return otherEnd.RelationshipName == RelationshipName &&
                    otherEnd.TargetRoleName == TargetRoleName;

            return false;
        }

        public System.Data.Objects.DataClasses.IRelatedEnd GetRelatedEnd(System.Data.Objects.DataClasses.IEntityWithRelationships currentObject)
        {
            return currentObject.RelationshipManager.GetRelatedEnd(RelationshipName, TargetRoleName);
        }

        public IGenericBusinessObj GetRelation(System.Data.Objects.DataClasses.IEntityWithRelationships currentObject, 
            System.Data.Objects.ObjectStateEntry currentState)
        {
            System.Data.Objects.DataClasses.IRelatedEnd currentEnd = GetRelatedEnd(currentObject);

            if (currentEnd == null)
                throw new ApplicationException(string.Format("Could not find {0} on {1}", TargetRoleName, RelationshipName));

            // loaded from DB then call load check to see if loaded before enumerating
            if (currentState.State == System.Data.EntityState.Modified || currentState.State == System.Data.EntityState.Unchanged)
            {
                if (currentEnd.IsLoaded == false)
                    return null;
            }

            // find the first one and return it
            foreach (object childObject in currentEnd)
                return childObject as IGenericBusinessObj;
            
            // if first not found then return
            return null;
        }
    }

    public class BusinessObjectStructure
    {
        public readonly System.Type DataType;
        public readonly string Name;
        public readonly bool IsNullable;
        public readonly int MaxLength;
        public readonly bool IsCSpaceColumn;
        public readonly System.Reflection.PropertyInfo PropertyInfo;
        public readonly int? CSpaceIndex;

        public BusinessObjectStructure(System.Type dataType,
            string name, bool isNullable, int maxLength, 
            bool isCSpaceColumn, int? cSpaceIndex,
            System.Reflection.PropertyInfo propertyInfo)
        {
            DataType = dataType;
            Name = name;
            IsNullable = isNullable;
            MaxLength = maxLength;
            CSpaceIndex = cSpaceIndex;
            IsCSpaceColumn = isCSpaceColumn;
            PropertyInfo = propertyInfo;
        }

        /// <summary>
        /// Use for comparison purposes only
        /// </summary>
        /// <param name="name"></param>
        public BusinessObjectStructure(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            if ( obj is string)
                return obj == Name;

            else if (obj is BusinessObjectStructure)
            {
                BusinessObjectStructure tmpObj = obj as BusinessObjectStructure;
                return tmpObj.Name == Name;
            }
            else
                return base.Equals(obj);
        }

        #region Get Valid Column Value
        public static object GetValidColumnValue(object input,
            BusinessObjectStructure dc)
        {
            Type datatype = Type.GetType(dc.DataType.FullName);
            bool isNullable = dc.IsNullable;

            if (IsNullableType(datatype))
            {
                datatype = System.Nullable.GetUnderlyingType(datatype);
                isNullable = true;
            }

            if (datatype != typeof(System.String))
            {
                if ((input == null) || (input.ToString() == ""))
                {
                    if (dc.IsNullable)
                    {
                        return null;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    try
                    {
                        if (datatype.Equals(typeof(decimal)))
                                return System.Decimal.Parse(input.ToString(), System.Globalization.NumberStyles.Currency);

                        if (datatype.Equals(typeof(System.Guid)))
                            return new Guid(input.ToString());

                        return System.Convert.ChangeType(input, datatype);
                    }
                    catch (Exception)
                    {
                        if (dc.IsNullable)
                        {
                            return null;
                        }
                        else
                            throw new ArgumentOutOfRangeException(dc.Name, input.ToString(),
                                "The datatype conversion to " + datatype.ToString() + " failed from the value:" + input.ToString());
                    }
                }
            }
            else
            {
                if (input == null)
                {
                    if (dc.IsNullable)
                    {
                        return null;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return input.ToString();
                }
            }
        }

        private static bool IsNullableType(Type theType)
        {
            return (theType.IsGenericType && theType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
        }

        #endregion
    }
}
