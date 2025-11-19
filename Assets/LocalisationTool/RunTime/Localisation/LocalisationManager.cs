using System.Collections.Generic;
using System;
using UnityEngine;
using LocalisationToolset;

namespace LocalisationToolset
{
    /// <summary>
    /// Assign this Component to a persistant game object
    /// </summary>
    public class LocalisationManager : MonoBehaviour
    {
        public static LocalisationManager Instance { get; private set; }

        public string localisationFilePath = "";

        [Header("Default Language")]
        public string defaultLanguage = "English";

        LocalisationData languageData = new LocalisationData();

        // Event for UI components to subscribe to
        public event Action<string> OnLanguageChanged;

        private void OnValidate()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            Instance = this;
            
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            ChangeLanguage(defaultLanguage);

        }

        public string GetText(string _key)
        {
            return languageData.GetValue(_key);
        }

        public void ChangeLanguage(string _newLanguage)
        {
            languageData.localisationEntries.Clear();
            //
            languageData.LanguageCode = _newLanguage;
            languageData = CSVHandler.LoadLanguage(_newLanguage);
            OnLanguageChanged?.Invoke(languageData.LanguageCode);
        }
    }
}

