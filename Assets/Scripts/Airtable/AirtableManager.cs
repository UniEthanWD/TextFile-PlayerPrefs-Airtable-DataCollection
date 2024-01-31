using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;



public class AirtableManager : MonoBehaviour
{
    [Header ("Scripts")]
    public AirtableSceneController airtableSceneController;

    [Header("Airtable")]
    public string airtableEndpoint = "https://api.airtable.com/v0/";
    public string baseId = "YOUR_BASE_ID";
    public string tableName = "YOUR_TABLE_NAME";
    public string accessToken = "YOUR_ACCESS_TOKEN";
    private string dataToParse;

    [Header("Data For Airtable")]
    public string dateTime;
    public string playerName;
    public string volume;
    public string coins;
    public string timePlayed;
    public string health;
    public string score;


    public void CreateRecord()
    {
        dateTime = DateTime.Now.ToString("dd.MM.yyyy HH.mm");

        // Create the URL for the API request
        string url = airtableEndpoint + baseId + "/" + tableName;

        // Create the data to be sent in the request
        string jsonFields = "{\"fields\": {" +
                                    "\"DateTime\":\"" + dateTime + "\", " + 
                                    "\"PlayerName\":\"" + playerName + "\", " +
                                    "\"Volume\":\"" + volume + "\", " +
                                    "\"Coins\":\"" + coins + "\", " +
                                    "\"TimePlayed\":\"" + timePlayed + "\", " +
                                    "\"Health\":\"" + health + "\", " +
                                    "\"Score\":\"" + score + "\"" +
                                    "}}";

        // Start the coroutine to send the API request
        StartCoroutine(SendRequest(url, "POST", response =>
        {
            Debug.Log("Record created: " + response);

            //parsing JSON to retrieve record ID....
            dataToParse = response;
            JSONParse();
        }, jsonFields));
    }

    // Unity coroutine to make API requests
    private IEnumerator SendRequest(string url, string method, Action<string> callback, string jsonData = "")
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = method;
        request.ContentType = "application/json";
        request.Headers["Authorization"] = "Bearer " + accessToken;

        if (!string.IsNullOrEmpty(jsonData))
        {
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(jsonData);
            }
        }

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        {
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string jsonResponse = reader.ReadToEnd();
                if (callback != null)
                {
                    callback(jsonResponse);
                }
            }
        }

        yield return null;
    }


    public void TestSetPlayerName()
    {
        string url = airtableEndpoint + baseId + "/" + tableName + "/" + "recmLNtUus5c5O4Bf";

        // Create the data to be sent in the request
        string jsonFields = "{\"fields\": {" +
                                    "\"PlayerName\":\"" + playerName + "\", " +
                                    "\"Volume\":\"" + volume + "\"" +
                                    "}}";

        // Start the coroutine to send the API request
        StartCoroutine(SendRequest(url, "POST", response =>
        {
            Debug.Log("Record created: " + response);
        }, jsonFields));
    }


    public void GetRecordValue(string recordID)
    {
        RetrieveRecord(recordID, "ExampleLoadTable");
    }


    // Example method to retrieve a record from Airtable based on record ID
    public void RetrieveRecord(string recordId, string readTableName)
    {
        // Create the URL for the API request
        string url = airtableEndpoint + baseId + "/" + readTableName + "/" + recordId;

        // Start the coroutine to send the API request
        StartCoroutine(SendRequest(url, "GET", response =>
        {
            // Parse the JSON response
            var responseObject = JsonUtility.FromJson<Dictionary<string, object>>(response);

            dataToParse = response;
            JSONParse();

        }));
    }

    public void JSONParse()
    {
        string source = dataToParse;
        dynamic data = JObject.Parse(source);

        Debug.Log(data.id);

        //playerName = data.fields.PlayerName;
        //volume = data.fields.Volume;

        //airtableSceneController.playerName = playerName;
        //airtableSceneController.volume = volume;

        Debug.Log("Player Name Is: " + playerName + ". Volume Data: " + volume);
    }
}
