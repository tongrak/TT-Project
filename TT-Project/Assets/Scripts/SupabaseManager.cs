using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class SupabaseManager
{
    private readonly string DATABASE_URL = "https://tuxwkiinqssipykgyyau.supabase.co/rest/v1/";
    private readonly string SUPABASE_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InR1eHdraWlucXNzaXB5a2d5eWF1Iiwicm9sZSI6ImFub24iLCJpYXQiOjE2NzI0MTQ2MDMsImV4cCI6MTk4Nzk5MDYwM30.QUl9cKY1h-NRtPlLz2lQN0UOWUAWMSbAF6FnQF2Ga20";
    public string jsonData { get; set; }

    // --------------------Method section--------------------

    // Use to return top ten Student data.
    public IEnumerator GetTopTenStudentData()
    {
        UnityWebRequest request = RequestURL_GET_topTenStudent();
        yield return API_GET_Coroutine(request,"students");
    }

    // --------------------create Request section--------------------

    // Use to create web request for get all Student data.
    private UnityWebRequest RequestURL_GET_AllStudenData()
    {
        string api_url = DATABASE_URL + "Student?select=*";
        UnityWebRequest request = UnityWebRequest.Get(api_url);
        return request;
    }

    // Use to create web request for get top 10 Student data.
    private UnityWebRequest RequestURL_GET_topTenStudent()
    {
        string api_url = DATABASE_URL + "Student?limit=10&order=id.asc";
        UnityWebRequest request = UnityWebRequest.Get(api_url);
        return request;
    }

    // --------------------send API section--------------------

    // Do web request by follow request from input.
    // @params jsonHeader use for specify which type of json want to collect such as "students" by refer from class.
    // @params request is HTTP request that want to send.
    private IEnumerator API_GET_Coroutine(UnityWebRequest request, string jsonHeader)
    {
        // add important header to make request complete.
        request.SetRequestHeader("apikey", SUPABASE_KEY);
        request.SetRequestHeader("Authorization", "Bearer " + SUPABASE_KEY);
        yield return request.SendWebRequest();

        // if request is error for some reason.
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.DataProcessingError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
            Debug.LogError("Check from this request: " + request.url);
            yield break;
        }
        // if request success
        else
        {
            jsonData = "{\""+jsonHeader+"\":" + request.downloadHandler.text + "}";
            //studentList = JsonUtility.FromJson<StudentList>(jsonData);
            yield break;
        }
    }

    //private IEnumerator API_POST_Coroutine(UnityWebRequest request)
    //{

    //}
}
