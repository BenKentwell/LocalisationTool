using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using LocalisationToolset;
using System.Collections.Generic;

namespace LocalisationToolset
{
    public class SettingsTab
    {
        //Body of UI
        public VisualElement container = new VisualElement();

        //Font Objects
        private Object assignedAsset;
        private List<ObjectField> objectFields = new List<ObjectField>();

        public void OpenFilePath()
        {
            LocalisationManager.Instance.localisationFilePath = EditorUtility.OpenFilePanel("Select the localisation file", "", "csv");
            Debug.Log($"Set filepath to {LocalisationManager.Instance.localisationFilePath}");
            CSVHandler.CheckForAllLanguages();
            //LocalisationManager.Instance.ChangeLanguage(LocalisationManager.Instance.defaultLanguage);
        }

        public void CSVTabGUIClicked(VisualElement _root)
        {
            container.style.paddingTop = 10;
            container.style.paddingLeft = 10;
            Label label = new Label("CSV Importer Tab");
            container.Add(label);
            VisualElement container2 = new VisualElement();
            Button importButton = new Button(OpenFilePath)
            {
                text = "Import CSV file"
            };

            container.Add(importButton);
            
            LoadFilePath(container2, _root);
            FontAsset(_root);

            _root.Add(container);
        }

        private void FontAsset(VisualElement _root)
        {
            foreach(LanguageFont lf in LocalisationManager.Instance.GetLanguageFontData())
            {
                Label label = new Label(lf.key);
                container.Add(label);
                ObjectField objectField = new ObjectField();
                
                objectField.value = lf.value;
                objectField.RegisterValueChangedCallback(evt =>
                {
                    lf.value = (TMPro.TMP_FontAsset)evt.newValue;
                });

                objectFields.Add(objectField);
                container.Add(objectField);
            }
        }

        public void LoadFilePath(VisualElement _ve, VisualElement _root)
        {
            _ve.Clear();
            _ve.style.paddingTop = 10;
            _ve.style.paddingLeft = 10;
            Label label2 = new Label(LocalisationManager.Instance.localisationFilePath);
            _ve.Add(label2);
            _root.Add(_ve);
        }

        public void SettingsTabGUIClicked(VisualElement _root)
        {
            //Create Header Label
            container.style.paddingTop = 10;
            container.style.paddingLeft = 10;
            Label label = new Label("Settings Tab");
            container.Add(label);
            _root.Add(container);
            CSVTabGUIClicked(_root);
        }
    }
}