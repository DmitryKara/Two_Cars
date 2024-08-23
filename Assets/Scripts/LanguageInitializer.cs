using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageInitializer : MonoBehaviour
{
    public void Awake()
    {
        LoadAndApplyLanguage();
    }

    public void LoadAndApplyLanguage()
    {
        string savedLanguage = PlayerPrefs.GetString("selectedLanguage", "en");

        SetLanguage(savedLanguage);
    }

    public void SetLanguage(string localeCode)
    {
        var selectedLocale = LocalizationSettings.AvailableLocales.Locales.Find(locale => locale.Identifier.Code == localeCode);

        if (selectedLocale != null)
        {
            if (LocalizationSettings.SelectedLocale != selectedLocale)
            {
                LocalizationSettings.SelectedLocale = selectedLocale;
            }
        }
        else
        {
            Debug.LogError("Locale not found: " + localeCode);
        }
    }
}