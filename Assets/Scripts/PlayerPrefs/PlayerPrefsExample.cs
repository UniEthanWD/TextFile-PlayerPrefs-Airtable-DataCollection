using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerPrefsExample : MonoBehaviour
{
    [Header ("PlayerPrefsInteger")]
    public int intForPlayerPrefs;
    public int currentIntValue;
    public int variableIntValue;
    public TMP_InputField intInputFieldTMP;
    public TMP_Text intInputFieldInfoTMP;
    public TMP_Text intCurrentValueTMP;
    public TMP_Text intPlayerPrefValueTMP;


    [Header("PlayerPrefsFloat")]
    public float floatForPlayerPrefs;
    public float currentFloatValue;
    public float variableFloatValue;
    public TMP_InputField floatInputFieldTMP;
    public TMP_Text floatInputFieldInfoTMP;
    public TMP_Text floatCurrentValueTMP;
    public TMP_Text floatPlayerPrefValueTMP;

    [Header("PlayerPrefsString")]
    public string stringForPlayerPrefs;
    public string currentStringValue;
    public string variableStringValue;
    public TMP_InputField stringInputFieldTMP;
    public TMP_Text stringInputFieldInfoTMP;
    public TMP_Text stringCurrentValueTMP;
    public TMP_Text stringPlayerPrefValueTMP;

    public void SaveInteger()
    {
        if (int.TryParse(intInputFieldTMP.text, out int value))
        {
            PlayerPrefs.SetInt("playerPrefsInt", currentIntValue);
            intInputFieldTMP.text = ("");
            intInputFieldInfoTMP.text = "Value saved";
        }
        else
        {
            intInputFieldTMP.text = ("");
            intInputFieldInfoTMP.text = "Try again with an integer";
        }
    }

    public void LoadInteger()
    {
        if(PlayerPrefs.HasKey("playerPrefsInt"))
        {
            intForPlayerPrefs = PlayerPrefs.GetInt("playerPrefsInt");
            intPlayerPrefValueTMP.text = intForPlayerPrefs.ToString();
            intInputFieldInfoTMP.text = "Value loaded";
        }
        else
        {
            intPlayerPrefValueTMP.text = "";
            intInputFieldInfoTMP.text = "No player pref to load";
        }
    }

    public void IntUpdateValue()
    {
        if(int.TryParse(intInputFieldTMP.text, out int value))
        {
            currentIntValue = value;
            intCurrentValueTMP.text = currentIntValue.ToString();
        }
        else
        {
            intInputFieldTMP.text = ("");
            intInputFieldInfoTMP.text = "Try again with an integer";
        }
    }

    public void DeleteIntPlayerPref()
    {
        PlayerPrefs.DeleteKey("playerPrefsInt");
        intInputFieldInfoTMP.text = "Int player pref deleted";
    }

    public void SaveFloat()
    {
        if (float.TryParse(floatInputFieldTMP.text, out float value))
        {
            PlayerPrefs.SetFloat("playerPrefsFloat", currentFloatValue);
            floatInputFieldTMP.text = ("");
            floatInputFieldInfoTMP.text = "Value saved";
        }
        else
        {
            floatInputFieldTMP.text = ("");
            floatInputFieldInfoTMP.text = "Try again with a float";
        }
    }

    public void LoadFloat()
    {
        if(PlayerPrefs.HasKey("playerPrefsFloat"))
        {
            floatForPlayerPrefs = PlayerPrefs.GetFloat("playerPrefsFloat");
            floatPlayerPrefValueTMP.text = floatForPlayerPrefs.ToString();
            floatInputFieldInfoTMP.text = "Value loaded";
        }
        else
        {
            floatPlayerPrefValueTMP.text = "";
            floatInputFieldInfoTMP.text = "No player pref to load";
        }
    }

    public void FloatUpdateValue()
    {
        if (float.TryParse(floatInputFieldTMP.text, out float value))
        {
            currentFloatValue = value;
            floatCurrentValueTMP.text = currentFloatValue.ToString();
        }
        else
        {
            floatInputFieldTMP.text = ("");
            floatInputFieldInfoTMP.text = "Try again with a float";
        }
    }

    public void DeleteFloatPlayerPref()
    {
        PlayerPrefs.DeleteKey("playerPrefsFloat");
        floatInputFieldInfoTMP.text = "Float player pref deleted";
    }

    public void SaveString()
    {
        PlayerPrefs.SetString("playerPrefsString", currentStringValue);
        stringInputFieldTMP.text = ("");
        stringInputFieldInfoTMP.text = "Value saved";
    }

    public void LoadString()
    {
        if(PlayerPrefs.HasKey("playerPrefsString"))
        {
            stringForPlayerPrefs = PlayerPrefs.GetString("playerPrefsString");
            stringPlayerPrefValueTMP.text = stringForPlayerPrefs;
            stringInputFieldInfoTMP.text = "Value loaded";
        }
        else
        {
            stringPlayerPrefValueTMP.text = "";
            stringInputFieldInfoTMP.text = "No value to load";
        }
    }

    public void StringUpdateValue()
    {
        currentStringValue = stringInputFieldTMP.text;
        stringCurrentValueTMP.text = currentStringValue;
    }

    public void DeleteStringPlayerPref()
    {
        PlayerPrefs.DeleteKey("playerPrefsString");
        stringInputFieldInfoTMP.text = "String player pref deleted";
    }

    public void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        intInputFieldInfoTMP.text = "All player prefs deleted";
        floatInputFieldInfoTMP.text = "All player prefs deleted";
        stringInputFieldInfoTMP.text = "All player prefs deleted";
    }

    public void ChangeScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "PlayerPrefsExamplePage1")
        {
            SceneManager.LoadScene("PlayerPrefsExamplePage2");
        }
        else
        {
            SceneManager.LoadScene("PlayerPrefsExamplePage1");
        }
    }
}
