using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// LocalizationManager: Singleton for runtime localization.
/// Handles loading languages, providing text by key,
/// firing OnLanguageChanged events, and formatting tokens.
/// </summary>
public class LocalizationManager : MonoBehaviour
{
    #region Singleton
    public static LocalizationManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Fields
    [Header("Default Language")]
    public string defaultLanguage = "EN";

    // Current active language
    public string CurrentLanguage { get; private set; }

    // Dictionary holding key -> translated text
    private Dictionary<string, string> localizationDictionary = new Dictionary<string, string>();

    // Event for UI components to subscribe to
    public event Action<string> OnLanguageChanged;
    #endregion

    #region Public Methods

    /// <summary>
    /// Loads a language at runtime (from JSON, ScriptableObject, or other source)
    /// </summary>
    public void LoadLanguage(string language)
    {
        CurrentLanguage = language.ToUpper();

        // TODO: Replace with real data loading (JSON/CSV/ScriptableObject)
        // For now, sample dictionary
        localizationDictionary = SampleLoadLanguage(CurrentLanguage);

        // Notify subscribers
        OnLanguageChanged?.Invoke(CurrentLanguage);
    }

    /// <summary>
    /// Returns the localized text for a key.
    /// If missing, returns <KEY> to highlight missing translation.
    /// </summary>
    public string GetText(string key)
    {
        if (localizationDictionary.TryGetValue(key, out string value))
        {
            return value;
        }

        Debug.LogWarning($"Missing localization key: {key} for language {CurrentLanguage}");
        return $"<{key}>";
    }

    /// <summary>
    /// Returns text with token replacements
    /// Example: "Hello {playerName}" with tokens["playerName"] = "Alex"
    /// </summary>
    public string GetText(string key, Dictionary<string, string> tokens)
    {
        string text = GetText(key);

        if (tokens == null) return text;

        foreach (var token in tokens)
        {
            text = text.Replace($"{{{token.Key}}}", token.Value);
        }

        return text;
    }

    /// <summary>
    /// Reloads default language
    /// </summary>
    public void ReloadDefaultLanguage()
    {
        LoadLanguage(defaultLanguage);
    }

    #endregion

    #region Private / Sample Data
    /// <summary>
    /// Sample dictionary for demo purposes
    /// Replace with actual CSV/JSON loader
    /// </summary>
    private Dictionary<string, string> SampleLoadLanguage(string language)
    {
        var dict = new Dictionary<string, string>();

        switch (language)
        {
            case "EN":
                dict["MENU_PLAY"] = "Play";
                dict["MENU_SETTINGS"] = "Settings";
                dict["MENU_QUIT"] = "Quit";
                dict["dlg_intro_001"] = "Hey there! Welcome to the ruins.";
                break;

            case "DE":
                dict["MENU_PLAY"] = "Spielen";
                dict["MENU_SETTINGS"] = "Einstellungen";
                dict["MENU_QUIT"] = "Beenden";
                dict["dlg_intro_001"] = "Hey! Willkommen in den Ruinen.";
                break;

            case "JP":
                dict["MENU_PLAY"] = "???";
                dict["MENU_SETTINGS"] = "??";
                dict["MENU_QUIT"] = "??";
                dict["dlg_intro_001"] = "??? ????????";
                break;

            default:
                dict["MENU_PLAY"] = "Play";
                dict["MENU_SETTINGS"] = "Settings";
                dict["MENU_QUIT"] = "Quit";
                dict["dlg_intro_001"] = "Hey there!";
                break;
        }

        return dict;
    }
    #endregion
}
