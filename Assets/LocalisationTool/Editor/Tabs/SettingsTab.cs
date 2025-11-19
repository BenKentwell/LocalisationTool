using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using LocalisationToolset;

namespace LocalisationToolset
{
    public class SettingsTab
    {
        //Body of UI
        public VisualElement container = new VisualElement();

        //Font Objects
        private Object assignedAsset;
        private ObjectField objectField = new ObjectField();

        [SerializeField] private int m_MySerializedInt = 0;

        public void OpenFilePath()
        {
            LocalisationManager.Instance.localisationFilePath = EditorUtility.OpenFilePanel("Select the localisation file", "", "csv");
            Debug.Log($"Set filepath to {LocalisationManager.Instance.localisationFilePath}");

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
            _root.Add(container);

            LoadFilePath(container2, _root);
            FontAsset(_root);


        }

        private void FontAsset(VisualElement _root)
        {
            objectField.value = assignedAsset;
            objectField.RegisterValueChangedCallback(evt =>
            {
                assignedAsset = evt.newValue;
            });
            _root.Add(objectField);
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