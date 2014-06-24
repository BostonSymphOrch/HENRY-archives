using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Adage.EF.Interfaces
{
    public interface IBusinessCollection : IEnumerable
    {
        object GetItem(string UniqueKey);
        object GetNewItem();
        ArrayList GetView(string FilterExp, string SortExp);
        System.Type GetSwitchable();
        int VirtualItemCount { get; }
        object ParentObject();
        int Count { get; }

        void RemoveAt(int index);
        void DeleteItem(IBusinessObject currObj);
    }
}
