using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adage.EF.Interfaces
{
    public interface IGenericBusinessObj
    {
        object ReadProperty(string name, System.Data.Objects.ObjectStateEntry currentEntry);
        object ReadProperty(int indexm, System.Data.Objects.ObjectStateEntry currentEntry);

        bool CanReadProperty(string name);
        bool CanWriteProperty(string name);

        List<BusinessObjectStructure> GetTableFields(System.Data.Objects.ObjectStateEntry currentEntry);
        List<RelatedObject> GetSingleRelatedObjects(System.Data.Objects.ObjectStateEntry currentEntry);

        List<RelatedObject> RelatedObjects { get; }
    }
}
