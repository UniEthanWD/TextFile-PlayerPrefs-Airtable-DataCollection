using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using TMPro;



public class AirtableManager : MonoBehaviour
{
    [Header("Airtable")]
    public string airtableEndpoint = "https://api.airtable.com/v0/";
    public string baseId = "YOUR_BASE_ID";
    public string tableName = "YOUR_TABLE_NAME";
    public string accessToken = "YOUR_ACCESS_TOKEN";
    private string dataToParse;


    [Header("Assesment Information")]
    public string dateTime;
    public string patientOneDiagnosis;
    public string patientTwoDiagnosis;
    public string patientThreeDiagnosis;
    public string patientOneComments;
    public string patientTwoComments;
    public string patientThreeComments;



    public void CreateRecord()
    {
        dateTime = System.DateTime.Now.ToString("dd.MM.yyyy HH.mm");

        // Create the URL for the API request
        string url = airtableEndpoint + baseId + "/" + tableName;

        // Create the data to be sent in the request
        string jsonFields = "{\"fields\": {" +
                                    "\"Date and Time\":\"" + dateTime + "\", " +
                                    "\"Patient One Diagnosis\":\"" + patientOneDiagnosis + "\", " +
                                    "\"Patient One Comments\":\"" + patientOneComments + "\", " +
                                    "\"Patient Two Diagnosis\":\"" + patientTwoDiagnosis + "\", " +
                                    "\"Patient Two Comments\":\"" + patientTwoComments + "\", " +
                                    "\"Patient Three Diagnosis\":\"" + patientThreeDiagnosis + "\", " +
                                    "\"Patient Three Comments\":\"" + patientThreeComments + "\"" +
                                    "}}";

        // Start the coroutine to send the API request
        StartCoroutine(SendRequest(url, "POST", response =>
        {
            Debug.Log("Record created: " + response);
        }, jsonFields));
    }

    //public void TestCreateRecord()
    //{
    //    dateTime = System.DateTime.Now.ToString("dd.MM.yyyy HH.mm");

    //    // Create the URL for the API request
    //    string url = airtableEndpoint + baseId + "/" + tableName;

    //    Debug.Log(url);

    //    // Create the data to be sent in the request
    //    string jsonFields = "{\"fields\": {" +
    //                                "\"Date and Time\":\"" + dateTime + "\", " +
    //                                "\"Extra Time Added\":\"" + extraTimeString + "\"" +
    //                                "}}";
    //    string jsonData = "{\"fields\": " + jsonFields + "}";

    //    Debug.Log(jsonData);

    //    // Start the coroutine to send the API request
    //    StartCoroutine(SendRequest(url, "POST", response =>
    //    {
    //        Debug.Log("Record created: " + response);
    //    }, jsonData));
    //}

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

    public void CallGetCellValue()
    {
        RetrieveRecord("recWq2NPUgOhAqx3w", "App Table References");
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
        Debug.Log("Table to be used: " + source);
    }
}
