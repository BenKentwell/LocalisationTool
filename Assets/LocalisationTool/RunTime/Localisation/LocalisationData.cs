using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace LocalisationToolset
{
    [Serializable]
    public class LocalisationData
    {
        public string LanguageCode;
        public List<LocalisationEntry> localisationEntries = new List<LocalisationEntry>();

        public LocalisationData()
        {
            localisationEntries = new List<LocalisationEntry>();
        }

        public string GetValue(string _key)
        {
            LocalisationEntry entry = localisationEntries.Find(e => e.key == _key);

            if(entry != null)
            {
                return entry.value;
            }

            Debug.LogError($"{_key} was not found in language {LanguageCode}");

            return null;
        }

        public void AddEntry(string _key, string _value, int _column)
        {
            LocalisationEntry entry = localisationEntries.Find(e =>e.key == _key);

            if(entry != null)
            {
                Debug.LogError($"Duplicate Title ({_key}) was found in language {LanguageCode}, Column: {_column}");
                return;
            }

            entry = new LocalisationEntry(_key, _value);
            localisationEntries.Add(entry);
        }
    }


    //KeyValue pair for rep
    [Serializable]
    public class LocalisationEntry
    {
        /// <summary>
        /// Title name for text
        /// </summary>
        public string key;
       

        /// <summary>
        /// text value 
        /// </summary>
        public string value;

        public LocalisationEntry() { }

        public LocalisationEntry(string _key, string _value)
        {
            key = _key;
            value = _value;
        }
    }

    public struct CSVLocation 
    {
        public CSVLocation(int _x, int _y)
        {
            x = _x; y = _y;
        }
        int x;
        int y;
    }

}

