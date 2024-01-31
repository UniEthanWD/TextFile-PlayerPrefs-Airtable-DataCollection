using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AirtableSceneController : MonoBehaviour
{
    [Header("Scripts")]
    public AirtableManager airtableManager;

    [Header("Player Name")]
    public TMP_InputField playerNameInputField;
    public TMP_Text playerNameFeedback;
    public string playerName;


    [Header("Volume")]
    public Slider volumeSlider;
    public TMP_Text volumeFeedback;
    public TMP_Text volumeLevel;
    public string volume;

    [Header("Game Data")]
    public TMP_InputField coinInputField;
    public TMP_InputField timePlayedInputField;
    public TMP_InputField healthInputField;
    public TMP_InputField scoreInputField;
    public string coins;
    public string timePlayed;
    public string health;
    public string score;


    public void UpdatePlayerName()
    {
        playerName = playerNameInputField.text;
    }

    //sets playerName variable to input fields value, then calls custom function from airtable controller
    public void SavePlayerName()
    {
        airtableManager.playerName = playerName;
        airtableManager.CreateRecord();
    }

    public void UpdateVolume()
    {
        volume = volumeSlider.value.ToString();
    }

    //sets volume variable to slider value and calls custom function from airtable controller
    public void SaveVolumeLevel()
    {
        airtableManager.volume = volume;
        airtableManager.CreateRecord();
    }

    //sets both playerName and volume values
    public void SavePlayerData()
    {
        airtableManager.playerName = playerName;
        airtableManager.volume = volume;
        airtableManager.CreateRecord();
    }

    //sets coin value to what is in the input field (used by inputFields "onEndEdit" event)
    public void UpdateCoinValue()
    {
        coins = coinInputField.text;
    }

    //sets timePlayed value to what is in the input field (used by inputFields "onEndEdit" event)
    public void UpdateTimePlayedValue()
    {
        timePlayed = timePlayedInputField.text;
    }

    //sets health value to what is in the input field (used by inputFields "onEndEdit" event)
    public void UpdateHealthValue()
    {
        health = healthInputField.text;
    }

    //sets score value to what is in the input field (used by inputFields "onEndEdit" event)
    public void UpdateScoreValue()
    {
        score = scoreInputField.text;
    }

    public void SaveAllData()
    {
        airtableManager.playerName = playerName;
        airtableManager.volume = volume;
        airtableManager.coins = coins;
        airtableManager.timePlayed = timePlayed;
        airtableManager.health = health;
        airtableManager.score = score;
        airtableManager.CreateRecord();
    }

    // Update is called once per frame
    void Update()
    {
        //ensures the text feedback is always the sliders value
        volumeLevel.text = volumeSlider.value.ToString();
    }

    public void LoadPlayerData()
    {
        airtableManager.GetRecordValue("recmLNtUus5c5O4Bf");
    }
}
