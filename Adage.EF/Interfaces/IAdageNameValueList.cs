using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adage.EF.Interfaces
{
    public interface IAdageNameValueList
    {
        string GetAnyValue(string key);
        System.Collections.DictionaryEntry GetDictionaryEntry(int currentIndex);    
        
        System.Collections.ICollection InActiveItems { get; }
        System.Collections.ICollection ShowAllItems { get; }

        System.Collections.IEnumerable GetActiveDictionary();
        System.Collections.IEnumerable GetFullDictionary();

        System.Collections.IEnumerable GetActiveDictionary(string filterName);
        System.Collections.IEnumerable GetFullDictionary(string filterName);
        bool IsInFilter(string filterName, string valueToFind);
    }
}
