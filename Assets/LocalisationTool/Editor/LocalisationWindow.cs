using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using LocalisationToolset;

public class LocalisationWindow : EditorWindow
{
    int toolbarSelection = 0;
    string[] tabs = { "CSV Importer", "Settings", "Preview", "TextKeys" };
    VisualElement root;


    [MenuItem("Window/Localisation/LocalisationWindow")]
    public static void ShowWindow()
    {
        var wnd = GetWindow<LocalisationWindow>();
        wnd.titleContent = new GUIContent("Localisation");
    }

    // UI Toolkit initialization
    private void CreateGUI()
    {
        root = rootVisualElement;
        // Toolbar
        Toolbar toolbar = new Toolbar();
        ToolbarMenu menu = new ToolbarMenu();
        menu.text = tabs[toolbarSelection];
        menu.menu.AppendAction("CSV Importer", a => SetTab(0));
        menu.menu.AppendAction("Settings", a => SetTab(1));
        menu.menu.AppendAction("Preview", a => SetTab(2));
        menu.menu.AppendAction("TextKeys", a => SetTab(3));
        toolbar.Add(menu);
        root.Add(toolbar);

        // Initial tab
        ShowTab(toolbarSelection);
    }

    void SetTab(int index)
    {
        toolbarSelection = index;
        ShowTab(index);
    }

    void ShowTab(int index)
    {
        // Clear previous content (except toolbar)
        if (root.childCount > 1)
        {
            root.RemoveAt(1);
        }

        switch (index)
        {
            case 0: CSVTabGUIClicked(); break;
            case 1: SettingsTabGUIClicked(); break;
            case 2: PreviewTabGUIClicked(); break;
            case 3: KeysTabGUIClicked(); break;
        }
    }

    void OpenFilePath()
    {
       LocalisationManager.Instance.localisationFilePath =  EditorUtility.OpenFilePanel("Select the localisation file", "", "csv");
        Debug.Log($"Set filepath to {LocalisationManager.Instance.localisationFilePath}");
        //LoadFilePath(_ve);
    }

    void CSVTabGUIClicked()
    {
        VisualElement container = new VisualElement();
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
        root.Add(container);
        
        LoadFilePath(container2);
    }

    void LoadFilePath(VisualElement _ve)
    {
        _ve.Clear();
        _ve.style.paddingTop = 10;
        _ve.style.paddingLeft = 10;
        Label label2 = new Label(LocalisationManager.Instance.localisationFilePath);
        _ve.Add(label2);
        root.Add(_ve);

    }

    void SettingsTabGUIClicked()
    {
        VisualElement container = new VisualElement();
        container.style.paddingTop = 10;
        container.style.paddingLeft = 10;

        Label label = new Label("Settings Tab");
        container.Add(label);

        root.Add(container);
    }

    void PreviewTabGUIClicked()
    {
        VisualElement container = new VisualElement();
        container.style.paddingTop = 10;
        container.style.paddingLeft = 10;

        Label label = new Label("Preview Tab");
        container.Add(label);

        root.Add(container);
    }

    void KeysTabGUIClicked()
    {
        VisualElement container = new VisualElement();
        container.style.paddingTop = 10;
        container.style.paddingLeft = 10;

        Label label = new Label("TextKeys Tab");
        container.Add(label);

        root.Add(container);
    }
}
