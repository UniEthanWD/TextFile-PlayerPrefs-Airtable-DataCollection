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

    public string dataToParse;
    private string dataRequestToLoad;


    public void LoadPlayerInfo()
    {
        StartCoroutine("LoadPlayerInfoCoroutine");
    }

    public void LoadGameData()
    {
        StartCoroutine("LoadGameDataCoroutine");
    }

    //sets our custom TableName, RecordID and NewRecordJson in the updateRecord script, then call the UpdateAirtable function, which requires those variables (our 'field' here is pointing to PlayerName in the Json)
    public void UpdatePlayerName()
    {
        updateRecordExample.TableName = "PlayerData";
        updateRecordExample.RecordId = "rec8TvVzZtJ8JbJI7";
        updateRecordExample.NewRecordJson = "{\"fields\": {" +
                                            "\"PlayerName\":\"" + sceneController.playerName + "\"" +
                                            "}}";
        updateRecordExample.UpdateAirtableRecord();
    }

    //sets our custom TableName, RecordID and NewRecordJson in the updateRecord script, then call the UpdateAirtable function, which requires those variables (our 'field' here is pointing to VolumePref in the Json)
    public void UpdateVolumePref()
    {
        updateRecordExample.TableName = "PlayerData";
        updateRecordExample.RecordId = "rec8TvVzZtJ8JbJI7";
        updateRecordExample.NewRecordJson = "{\"fields\": {" +
                                    "\"VolumePref\":\"" + sceneController.volume + "\"" +
                                    "}}";
        updateRecordExample.UpdateAirtableRecord();
    }

    //doing the same as the above 2 functions, but combined
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

    //doing similar to the update, but this time we are receiving. we are then storing the response in the dataToParse variable so that we can get info back from the Json string
    //that we can use as strings
    public IEnumerator LoadPlayerInfoCoroutine()
    {
        getRecordExample.TableName = "PlayerData";
        getRecordExample.RecordId = "rec8TvVzZtJ8JbJI7";
        getRecordExample.GetRecord();
        yield return new WaitForSeconds(0.75f);
        dataToParse = UnityWebRequestExtension.getResponse;
        Debug.Log(dataToParse);
        JSONParse();
    }

    //this function turns the returning Json string from airtable, puts it back into Json format, then looks at each field by name and gets the value - this is then given back to the sceneController script
    public void JSONParse()
    {
        string source = dataToParse;
        dynamic data = JObject.Parse(source);       
        
        sceneController.playerName = data.fields.PlayerName;
        sceneController.volume = data.fields.VolumePref;

        sceneController.nonsence = data.fields.Nonsence;

        sceneController.recordID = data.id;

        //Debug.Log("Player Name: " + data.fields.PlayerName);
        //Debug.Log("Volume Preference: " + data.fields.VolumePref);

    }

    //tells create record the table name and feeds it the Json string containing the data from the sceneController script, then initiates the createAirtableRecordFunction
    public void CreateGameDataEntry()
    {
        createRecord.TableName = "GameData";
        createRecord.NewRecordJson = "{\"fields\": {" +
                                    "\"Coins\":\"" + sceneController.coins + "\", " +
                                    "\"TimePlayed\":\"" + sceneController.timePlayed + "\", " +
                                    "\"Health\":\"" + sceneController.health + "\", " +
                                    "\"Score\":\"" + sceneController.score + "\"" +
                                    "}}";
        createRecord.CreateAirtableRecord();
    }

    //sets the table we want to look at, then request all the data from that table
    public void ListAllEntries()
    {
        listRecords.TableName = "GameData";
        listRecords.GetAirtableTableRecords();
    }
}
