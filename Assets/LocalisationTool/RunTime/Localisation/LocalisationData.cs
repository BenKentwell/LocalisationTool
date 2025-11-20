using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.InputSystem;
using TMPro;

namespace LocalisationToolset
{
    [Serializable]
    public class LocalisationData
    {
        public string LanguageCode;
        public List<LocalisationEntry> LocalisationEntries = new List<LocalisationEntry>();
        public List<LanguageFont> AllLanguages = new List<LanguageFont>();

        public LocalisationData()
        {
            LocalisationEntries = new List<LocalisationEntry>();
            AllLanguages = new List<LanguageFont>();
        }

        public string GetValue(string _key)
        {
            LocalisationEntry entry = LocalisationEntries.Find(e => e.key == _key);

            if(entry != null)
            {
                return entry.value;
            }

            Debug.LogError($"{_key} was not found in language {LanguageCode}");

            return null;
        }

        public void AddEntry(string _key, string _value, int _column)
        {
            LocalisationEntry entry = LocalisationEntries.Find(e =>e.key == _key);

            if(entry != null)
            {
                Debug.LogError($"Duplicate Title ({_key}) was found in language {LanguageCode}, Column: {_column}");
                return;
            }

            entry = new LocalisationEntry(_key, _value);
            LocalisationEntries.Add(entry);
        }

        public void AddToAllLanguages(string _newLanguage)
        {
            LanguageFont entry = AllLanguages.Find(e => e.key == _newLanguage);

            if (AllLanguages.Contains(entry))
            {
                return;
            }
            entry = new LanguageFont();
            entry.key = _newLanguage;
            entry.value = null;
            AllLanguages.Add(entry);
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

    //KeyValue pair for rep
    [Serializable]
    public class LanguageFont
    {
        public string key;

        public TMP_FontAsset value;

        public LanguageFont() { }

        public LanguageFont(string _key, TMP_FontAsset _value)
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

