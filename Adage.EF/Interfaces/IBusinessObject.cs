using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

namespace Adage.EF.Interfaces
{
    public interface IBusinessObject : IGenericBusinessObj
    {
        void UpdateProperty(string name, object value, ObjectStateEntry entry);

        string UniqueKey { get; }
        void Save(ObjectContext context);
        
        bool IsValid { get; }
        bool CheckIsValid(List<IBusinessObject> checkEntities);

        string BrokenRulesString { get; }
        void FindBrokenRules(List<IBusinessObject> checkEntities,
            List<string> currentMessages, EntityObject parent);

        string GetCurrentBrokenRules();

        System.Data.EntityKey GetEntityKey(string currKey);
    }
}