using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


public class AirtableController : MonoBehaviour
{
    [Header("Scripts")]
    public CreateRecord createRecord;
    public GetRecordExample getRecordExample;
    public DeleteRecord deleteRecord;
    public UpdateRecordExample updateRecordExample;
    public ListRecords listRecords;
    public SceneController sceneController;

    [Header("Feedback Strings")]
    public string userFeedback;

    private string dataToParse;
    private string dataRequestToLoad;


    public void LoadPlayerInfo()
    {
        StartCoroutine("LoadPlayerInfoCoroutine");
    }

    public void LoadGameData()
    {
        StartCoroutine("LoadGameDataCoroutine");
    }


    public void UpdatePlayerName()
    {
        updateRecordExample.TableName = "PlayerData";
        updateRecordExample.RecordId = "rec8TvVzZtJ8JbJI7";
        updateRecordExample.NewRecordJson = "{\"fields\": {" +
                            "\"PlayerName\":\"" + sceneController.playerName + "\"" +
                            "}}";
        updateRecordExample.UpdateAirtableRecord();
    }

    public void UpdateVolumePref()
    {
        updateRecordExample.TableName = "PlayerData";
        updateRecordExample.RecordId = "rec8TvVzZtJ8JbJI7";
        updateRecordExample.NewRecordJson = "{\"fields\": {" +
                                    "\"VolumePref\":\"" + sceneController.volume + "\"" +
                                    "}}";
        updateRecordExample.UpdateAirtableRecord();
    }

    public void UpdatePlayerData()
    {
        updateRecordExample.TableName = "PlayerData";
        updateRecordExample.RecordId = "rec8TvVzZtJ8JbJI7";
        updateRecordExample.NewRecordJson = "{\"fields\": {" +
                                    "\"PlayerName\":\"" + sceneController.playerName + "\", " +
                                    "\"VolumePref\":\"" + sceneController.volume + "\"" +
                                    "}}";
        updateRecordExample.UpdateAirtableRecord();
    }

    public IEnumerator LoadPlayerInfoCoroutine()
    {
        dataRequestToLoad = "playerInfo";
        getRecordExample.TableName = "PlayerData";
        getRecordExample.RecordId = "rec8TvVzZtJ8JbJI7";
        getRecordExample.GetRecord();
        yield return new WaitForSeconds(0.75f);
        userFeedback = UnityWebRequestExtension.getResponse;
        dataToParse = userFeedback;
        JSONParse();
    }

    public IEnumerator LoadGameDataCoroutine()
    {
        userFeedback = "Loading...";
        dataRequestToLoad = "gameData";
        getRecordExample.TableName = "GameData";
        getRecordExample.RecordId = "rec6kEe6pFk7yv6lE";
        getRecordExample.GetRecord();
        yield return new WaitForSeconds(0.5f);
        userFeedback = UnityWebRequestExtension.getResponse;
        dataToParse = userFeedback;
        JSONParse();
    }

    public void JSONParse()
    {
        string source = dataToParse;
        dynamic data = JObject.Parse(source);
        
        if(dataRequestToLoad == "playerInfo")
        {
            sceneController.playerName = data.fields.PlayerName;
            sceneController.volume = data.fields.VolumePref;

            Debug.Log("Player Name: " + data.fields.PlayerName);
            Debug.Log("Volume Preference: " + data.fields.VolumePref);
        }
        
        if(dataRequestToLoad == "gameData")
        {
            Debug.Log("Coins: " + data.fields.Coins);
            Debug.Log("Time Played: " + data.fields.TimePlayed);
            Debug.Log("Health: " + data.fields.Health);
            Debug.Log("Score: " + data.fields.Score);
        }
    }
}
