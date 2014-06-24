using System;
using System.Text;
using System.Xml.Linq;

namespace Bso.Archive.BusObj.Utility
{
    public static class Extensions
    {
        public static string GetXElement(this XElement node, XName name)
        {
            if (node == null) return String.Empty;

            var nodeElement = node.Element(name);

            if (nodeElement == null) return String.Empty;

            return (string)nodeElement.Value;
        }
    }
}
