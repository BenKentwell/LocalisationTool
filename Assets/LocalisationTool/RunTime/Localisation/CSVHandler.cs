using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace LocalisationToolset
{
    public static class CSVHandler
    {
        private static string path = LocalisationManager.Instance.localisationFilePath;
        public static LocalisationData LoadLanguage(string _newLanguage)
        {
            string[] line;
            LocalisationData loadedLanguage = new LocalisationData();
            loadedLanguage.LanguageCode = _newLanguage;

            //Filepath to Language file.
            StreamReader sr = File.OpenText(path);

            string s;

            //Find Index of language
            s = sr.ReadLine();
            line = s.Split(",");
            int newLangaugeIndex = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == _newLanguage)
                {
                    newLangaugeIndex = i;
                    break;
                }
            }

            while ((s = sr.ReadLine()) != null)
            {
                line = s.Split(",");
                LocalisationEntry entry = new LocalisationEntry(line[0], line[newLangaugeIndex]);
                loadedLanguage.LocalisationEntries.Add(entry);
                Debug.Log($"{entry.key} , {entry.value}");
            }
     
            return loadedLanguage;
        }

        public static void CheckForAllLanguages()
        { 
            LocalisationData loadedLanguage = new LocalisationData();
            string[] line;

            StreamReader sr = File.OpenText(path);
            string s;
            s = sr.ReadLine();
            line = s.Split(",");

            for (int i = 0; i < line.Length; i++)
            {
                if (i != 0)
                    loadedLanguage.AddToAllLanguages(line[i]);
            }
        }
    }

}

