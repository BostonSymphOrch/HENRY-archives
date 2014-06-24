using System.Xml.Linq;

namespace Bso.Archive.BusObj.Interface
{
    interface IOPASData
    {
        void UpdateData(XDocument element, string columnName, string tagName);
    }
}
