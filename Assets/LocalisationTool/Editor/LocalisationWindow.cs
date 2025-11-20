using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using LocalisationToolset;

public class LocalisationWindow : EditorWindow
{
    int toolbarSelection = 0;
    string[] tabs = {  "Settings", "Preview", "TextKeys" };

    private VisualElement root;

    private SettingsTab settingsTab;
    private VisualElement bodyContainer;

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
        menu.menu.AppendAction("Settings", a => SetTab(0));
        menu.menu.AppendAction("Preview", a => SetTab(1));
        menu.menu.AppendAction("TextKeys", a => SetTab(2));
        toolbar.Add(menu);
        root.Add(toolbar);

        // Initialise tabs
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
            case 0:
                TryRemoveBody();
                settingsTab = new SettingsTab();
                settingsTab.SettingsTabGUIClicked(root);
                bodyContainer = settingsTab.container;
                break;
            case 1: PreviewTabGUIClicked(); break;
            case 2: KeysTabGUIClicked(); break;
        }
    }

    void TryRemoveBody()
    {
        if (root.Contains(bodyContainer))
        {
            root.Remove(bodyContainer);
        }
                
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
