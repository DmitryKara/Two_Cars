using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LanguageSwitcher : MonoBehaviour
{
    public Toggle englishToggle;
    public Toggle russianToggle;

    void Start()
    {
        LoadLanguage();
    }

    void Update()
    {
        CheckToggleState();
    }

    void CheckToggleState()
    {
        if (englishToggle.isOn)
        {
            SetEnglish();
        }
        else if (russianToggle.isOn)
        {
            SetRussian();
        }
    }

    void SetEnglish()
    {
        SetLanguage("en");
    }

    void SetRussian()
    {
        SetLanguage("ru");
    }

    void SetLanguage(string localeCode)
    {
        var selectedLocale = LocalizationSettings.AvailableLocales.Locales.Find(locale => locale.Identifier.Code == localeCode);

        if (selectedLocale != null)
        {
            LocalizationSettings.SelectedLocale = selectedLocale;
            SaveLanguage(localeCode);
        }
        else
        {
            Debug.LogError("Locale not found: " + localeCode);
        }
    }

    void UpdateToggles()
    {
        if (englishToggle == null || russianToggle == null)
        {
            Debug.LogWarning("One or both of the toggles are missing!");
            return;
        }

        string currentLocale = LocalizationSettings.SelectedLocale.Identifier.Code;

        if (currentLocale == "en")
        {
            englishToggle.isOn = true;
            russianToggle.isOn = false;
        }
        else if (currentLocale == "ru")
        {
            englishToggle.isOn = false;
            russianToggle.isOn = true;
        }
    }

    void SaveLanguage(string localeCode)
    {
        PlayerPrefs.SetString("selectedLanguage", localeCode);
        PlayerPrefs.Save();
    }

    void LoadLanguage()
    {
        string savedLanguage = PlayerPrefs.GetString("selectedLanguage", "en");
        SetLanguage(savedLanguage);
        UpdateToggles();
    }
}