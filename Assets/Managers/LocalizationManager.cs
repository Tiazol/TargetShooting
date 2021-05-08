using System;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance { get; private set; }
    public event Action LocalizationChanged;

    private const string LOCALIZATION_PATH = "Localization";
    private const string ENDLINE_REPLACEMENT = "$";
    private Dictionary<SystemLanguage, Dictionary<string, string>> localizations;
    private SystemLanguage currentLanguage;

    private void Awake()
    {
        Instance = this;

        localizations = new Dictionary<SystemLanguage, Dictionary<string, string>>();
        LoadLocalizationResources();
        currentLanguage = SystemLanguage.English;
    }

    private void LoadLocalizationResources()
    {
        var textFiles = Resources.LoadAll<TextAsset>(LOCALIZATION_PATH);

        foreach (var textFile in textFiles)
        {
            var separators = new char[] { '\r', '\n' };
            var lines = textFile.text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            var language = (SystemLanguage)Enum.Parse(typeof(SystemLanguage), lines[0].Split('=')[1]);

            var dictionary = new Dictionary<string, string>();
            for (int i = 1; i < lines.Length; i++)
            {
                var key = lines[i].Split('=')[0];
                var value = lines[i].Split('=')[1];
                value = value.Replace(ENDLINE_REPLACEMENT, "\r\n");
                dictionary.Add(key, value);
            }

            localizations.Add(language, dictionary);
        }
    }

    public void SetLocalization(string language)
    {
        var newLanguage = (SystemLanguage)Enum.Parse(typeof(SystemLanguage), language);
        if (currentLanguage != newLanguage)
        {
            currentLanguage = newLanguage;
            LocalizationChanged?.Invoke();
        }
    }

    public string GetLocalizedString(string stringId)
    {
        string localized = stringId;

        if (localizations.ContainsKey(currentLanguage))
        {
            if (localizations[currentLanguage].ContainsKey(stringId))
            {
                localized = localizations[currentLanguage][stringId];
            }
        }

        return localized;
    }
}
