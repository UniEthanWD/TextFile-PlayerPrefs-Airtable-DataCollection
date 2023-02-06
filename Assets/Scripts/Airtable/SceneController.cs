using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneController : MonoBehaviour
{
    [Header("Scripts")]
    public AirtableController airtableController;

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


    //sets playerName variable to input fields value, then calls custom function from airtable controller
    public void SavePlayerName()
    {
        playerName = playerNameInputField.text;
        airtableController.UpdatePlayerData();
    }

    public void LoadPlayerName()
    {
        StartCoroutine("LoadPlayerNameCoroutine");
    }

    //sets volume variable to slider value and calls custom function from airtable controller
    public void SaveVolumeLevel()
    {
        volume = volumeSlider.value.ToString();
        airtableController.UpdatePlayerData();
    }

    public void LoadVolumeLevel()
    {
        StartCoroutine("LoadVolumeLevelCoroutine");
    }

    //sets both playerName and volume values
    public void SavePlayerData()
    {
        playerName = playerNameInputField.text;
        volume = volumeSlider.value.ToString();
        airtableController.UpdatePlayerData();
    }

    public void LoadPlayerData()
    {
        StartCoroutine("LoadPlayerNameCoroutine");
        StartCoroutine("LoadVolumeLevelCoroutine");
    }

    //calls custom load info function in airtableController and waits 1 second for response from airtable server before setting playerName to airtable value
    public IEnumerator LoadPlayerNameCoroutine()
    {
        airtableController.LoadPlayerInfo();
        yield return new WaitForSeconds(1f);
        playerNameInputField.text = playerName;
    }

    //calls custom load info function in airtableController and waits 1 second for response from airtable server before setting playerName to airtable value
    public IEnumerator LoadVolumeLevelCoroutine()
    {
        airtableController.LoadPlayerInfo();
        yield return new WaitForSeconds(1f);
        volumeSlider.value = float.Parse(volume);
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

    // Update is called once per frame
    void Update()
    {
        //ensures the text feedback is always the sliders value
        volumeLevel.text = volumeSlider.value.ToString();
    }
}
