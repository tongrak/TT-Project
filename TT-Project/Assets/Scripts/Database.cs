using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

// UnityWebRequest.Get example

// Access a website and use UnityWebRequest.Get to download a page.
// Also try to download a non-existing page. Display the error.
public class Database : MonoBehaviour
{
    private string DATABASE_URL = "https://tuxwkiinqssipykgyyau.supabase.co/rest/v1/";
    private string SUPABASE_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InR1eHdraWlucXNzaXB5a2d5eWF1Iiwicm9sZSI6ImFub24iLCJpYXQiOjE2NzI0MTQ2MDMsImV4cCI6MTk4Nzk5MDYwM30.QUl9cKY1h-NRtPlLz2lQN0UOWUAWMSbAF6FnQF2Ga20";
    private StudentList myStudentList;

    void Start()
    {
        getTopTenStudentData(DATABASE_URL, SUPABASE_KEY);
    }

    // Method section.

    // This function use to return all Student data.
    public StudentList getStudentData(string ref_url, string key)
    {
        UnityWebRequest request = RequestURL_GET_AllStudenData(ref_url);
        StartCoroutine(API_Coroutine(request, key));
        return myStudentList;
    }

    // Use to return top ten Student data.
    public StudentList getTopTenStudentData(string ref_url, string key)
    {
        UnityWebRequest request = RequestURL_GET_topTenStudent(ref_url);
        StartCoroutine(API_Coroutine(request, key));
        return myStudentList;
    }

    // Request section

    // Use to create web request for get all Student data.
    private UnityWebRequest RequestURL_GET_AllStudenData(string ref_url)
    {
        string api_url = ref_url + "Student?select=*";
        UnityWebRequest request = UnityWebRequest.Get(api_url);
        return request;
    }

    private UnityWebRequest RequestURL_GET_topTenStudent(string ref_url)
    {
        string api_url = ref_url + "Student?limit=10&order=id.asc";
        UnityWebRequest request = UnityWebRequest.Get(api_url);
        return request;
    }

    // Do web request by follow request from input.
    private IEnumerator API_Coroutine(UnityWebRequest request, string key)
    {
        // add important header to make request complete.
        request.SetRequestHeader("apikey", key);
        request.SetRequestHeader("Authorization", "Bearer " + key);
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
            string jsonData = "{\"students\":" + request.downloadHandler.text + "}";

            print(jsonData);
            myStudentList = JsonUtility.FromJson<StudentList>(jsonData);
        }
    }
}
