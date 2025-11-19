using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace LocalisationToolset
{
    public static class CSVHandler
    {
        public static LocalisationData LoadLanguage(string _newLanguage)
        {
            string[] line;
            LocalisationData loadedLanguage = new LocalisationData() ;
            loadedLanguage.LanguageCode = _newLanguage ;
            //Filepath to Language file.
            string path = LocalisationManager.Instance.localisationFilePath;

            StreamReader sr = File.OpenText(path);

            string s;

            s = sr.ReadLine();
            line = s.Split(",");

            int i = 0;
            for (i = 0; i < line.Length -1; i++)
            {
                if (line[i] == _newLanguage)
                    break;
            }

            while((s = sr.ReadLine()) != null)
            {
                line = s.Split(",");
                LocalisationEntry entry = new LocalisationEntry(line[0], line[i]) ;
                loadedLanguage.localisationEntries.Add(entry);
                Debug.Log($"{entry.key} , {entry.value}");
            }

            return loadedLanguage;
        }
    }
}

