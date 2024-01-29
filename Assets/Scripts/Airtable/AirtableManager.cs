using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.IO;

public class AirtableManager : MonoBehaviour
{
    // Airtable API endpoint
    public string airtableEndpoint = "https://api.airtable.com/v0/";

    // Your Airtable base ID
    public string baseId = "YOUR_BASE_ID";

    // Your Airtable table name
    public string tableName = "YOUR_TABLE_NAME";

    // Your Airtable Personal Access Token
    public string accessToken = "YOUR_ACCESS_TOKEN";

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

    // Example method to create a record in Airtable
    public void CreateRecord()
    {
        // Create the URL for the API request
        string url = airtableEndpoint + baseId + "/" + tableName;

        // Create the data to be sent in the request
        string jsonFields = "{" +
            "\"Date and Time\":\"" + "12:00" + "\", " +
            "\"Bone Scene Time Remaining\":\"" + "12:01" + "\", " +
            "\"Bone Scene Score\":\"" + "12:02" + "\", " +
            "\"Bone Scene Max Score\":\"" + "12:03" + "\", " +
            "\"Muscle Learning Time Remaining\":\"" + "12:04" + "\", " +
            "\"Muscle Learning Score\":\"" + "12:05" + "\", " +
            "\"Muscle Learning Max Score\":\"" + "12:06" + "\", " +
            "\"Muscle Testing Time Remaining\":\"" + "12:07" + "\", " +
            "\"Muscle Testing Score\":\"" + "12:08" + "\", " +
            "\"Muscle Testing Max Score\":\"" + "12:09" + "\"" +
            "}";
        string jsonData = "{\"fields\": " + jsonFields + "}";

        // Start the coroutine to send the API request
        StartCoroutine(SendRequest(url, "POST", response =>
        {
            Debug.Log("Record created: " + response);
        }, jsonData));
    }

    // Example method to retrieve records from Airtable
    public void RetrieveRecords()
    {
        // Create the URL for the API request
        string url = airtableEndpoint + baseId + "/" + tableName;

        // Start the coroutine to send the API request
        StartCoroutine(SendRequest(url, "GET", response =>
        {
            Debug.Log("Records retrieved: " + response);
        }));
    }
}
