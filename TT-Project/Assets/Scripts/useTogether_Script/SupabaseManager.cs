using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

public class SupabaseManager
{
    private readonly string DATABASE_URL = "https://tuxwkiinqssipykgyyau.supabase.co/rest/v1/";
    private readonly string SUPABASE_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InR1eHdraWlucXNzaXB5a2d5eWF1Iiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTY3MjQxNDYwMywiZXhwIjoxOTg3OTkwNjAzfQ.NO3eFnFFEASXg6zoRD-JPh-plmt9vhZ_B1SaGiK5hqs";
    //private static SupabaseManager instance = new SupabaseManager();
    public string jsonData { get; set; }
    public string errData { get; set; }

    // --------------------Method section--------------------

    public static SupabaseManager getInstance()
    {
        return new SupabaseManager();
    }

    // --------------------send API section--------------------

    // Do web request by follow request from input.
    // @params request is HTTP request that want to send.
    // @params jsonHeader use for specify which type of json want to collect such as "students" by refer from class.
    public IEnumerator API_GET_Coroutine(string parameter)
    {
        // create request for GET API
        string api_url = DATABASE_URL + parameter;
        UnityWebRequest request = UnityWebRequest.Get(api_url);
        // add important header to make request complete.
        request.SetRequestHeader("apikey", SUPABASE_KEY);
        request.SetRequestHeader("Authorization", "Bearer " + SUPABASE_KEY);
        yield return request.SendWebRequest();

        // if request is error for some reason.
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.DataProcessingError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
            Debug.LogError("Check from this request: " + request.url);
            request.Dispose();
            //yield break;
        }
        // if request success
        else
        {
            errData = null;
            jsonData = "{\"jsonData\":" + request.downloadHandler.text + "}";
            request.Dispose();
            //yield break;
        }
    }

    public IEnumerator API_PATCH_Coroutine(Dictionary<string, string> data, string parameter)
    {
        // Serialize body as a Json string
        string requestBodyString = JsonConvert.SerializeObject(data);
        // Convert Json body string into a byte array
        byte[] requestBodyData = System.Text.Encoding.UTF8.GetBytes(requestBodyString);
        // create request for POST API
        string api_url = DATABASE_URL + parameter;

        UnityWebRequest request = UnityWebRequest.Put(api_url, requestBodyData);
        // Specify that our method is of type 'patch'
        request.method = "PATCH";
        // add important header to make request complete.
        request.SetRequestHeader("apikey", SUPABASE_KEY);
        request.SetRequestHeader("Authorization", "Bearer " + SUPABASE_KEY);
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Prefer", "return=minimal");
        yield return request.SendWebRequest();

        // if request is error for some reason.
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.DataProcessingError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            errData = request.error;
            Debug.LogError(request.error);
            Debug.LogError("Check from this request: " + request.url);
            request.Dispose();
            //yield break;
        }
        // if request success
        else
        {
            errData = null;
            jsonData = "{\"jsonData\":" + request.downloadHandler.text + "}";
            request.Dispose();
            //yield break;
        }
    }

    public IEnumerator API_POST_Coroutine(Dictionary<string, string> data, string parameter)
    {
        // add data for each attribute to form
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<string, string> d in data)
        {
            form.AddField(d.Key, d.Value);
        }
        // create request for POST API
        string api_url = DATABASE_URL + parameter;
        UnityWebRequest request = UnityWebRequest.Post(api_url, form);
        // add important header to make request complete.
        request.SetRequestHeader("apikey", SUPABASE_KEY);
        request.SetRequestHeader("Authorization", "Bearer " + SUPABASE_KEY);
        request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        request.SetRequestHeader("Prefer", "return=minimal");
        yield return request.SendWebRequest();

        // if request is error for some reason.
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.DataProcessingError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            errData = request.error;
            Debug.LogError(request.error);
            Debug.LogError("Check from this request: " + request.url);
            request.Dispose();
            //yield break;
        }
        // if request success
        else
        {
            errData = null;
            jsonData = "{\"jsonData\":" + request.downloadHandler.text + "}";
            request.Dispose();
            //yield break;
        }
    }
}
