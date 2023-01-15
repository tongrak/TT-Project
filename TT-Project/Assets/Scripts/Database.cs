using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

// UnityWebRequest.Get example

// Access a website and use UnityWebRequest.Get to download a page.
// Also try to download a non-existing page. Display the error.
public class Database : MonoBehaviour
{
    private string URL = "https://tuxwkiinqssipykgyyau.supabase.co/rest/v1/Student?select=*";
    private string SUPABASE_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InR1eHdraWlucXNzaXB5a2d5eWF1Iiwicm9sZSI6ImFub24iLCJpYXQiOjE2NzI0MTQ2MDMsImV4cCI6MTk4Nzk5MDYwM30.QUl9cKY1h-NRtPlLz2lQN0UOWUAWMSbAF6FnQF2Ga20";

    private StudentList myStudentList = new StudentList();

    void Start()
    {
        getAllData();
    }

    void Update()
    {
        
    }

    public void getAllData()
    {
        StartCoroutine(Getdata_Coroutine(URL, SUPABASE_KEY));
    }

    IEnumerator Getdata_Coroutine(string url, string key)
    {
        // make request for getting score
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("apikey", key);
        request.SetRequestHeader("Authorization", "Bearer " + key);
        yield return request.SendWebRequest();

        //if request for some reason.
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.DataProcessingError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
            yield break;
        }
        // if request success
        else
        {
            print("Get data successful.");

            //print data
            string jsonData = "{\"students\":" + request.downloadHandler.text + "}";

            myStudentList = JsonUtility.FromJson<StudentList>(jsonData);
            int studentID = myStudentList.students[0].id;
            Debug.Log("Received: " + studentID);
        }
    }
}
