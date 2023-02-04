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

    public void SavePlayerName()
    {
        playerName = playerNameInputField.text;
        airtableController.UpdatePlayerData();
    }

    public void LoadPlayerName()
    {
        StartCoroutine("LoadPlayerNameCoroutine");
    }

    public void SaveVolumeLevel()
    {
        volume = volumeSlider.value.ToString();
        airtableController.UpdatePlayerData();
    }

    public void LoadVolumeLevel()
    {
        StartCoroutine("LoadVolumeLevelCoroutine");
    }

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

    public IEnumerator LoadPlayerNameCoroutine()
    {
        airtableController.LoadPlayerInfo();
        yield return new WaitForSeconds(1f);
        playerNameInputField.text = playerName;
    }

    public IEnumerator LoadVolumeLevelCoroutine()
    {
        airtableController.LoadPlayerInfo();
        yield return new WaitForSeconds(1f);
        volumeSlider.value = float.Parse(volume);
    }


    // Update is called once per frame
    void Update()
    {
        volumeLevel.text = volumeSlider.value.ToString();
    }
}
