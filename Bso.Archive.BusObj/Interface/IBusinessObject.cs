
namespace Bso.Archive.BusObj.Interface
{
    interface IBusinessObject
    {
        System.Data.EntityKey GetEntityKey(string currKey);
        void Save(System.Data.Objects.ObjectContext context);
        void UpdateProperty(string name, object value, System.Data.Objects.ObjectStateEntry entry);
        string UniqueKey { get; }

    }
}
