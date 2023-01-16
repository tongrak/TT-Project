using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private int maxScoreboardEntries = 10;
    [SerializeField] private Transform highscoreContainerTransform = null;
    [SerializeField] private GameObject scoreboardEntryObject = null;
    private StudentList playerData;
    private SupabaseManager DBConnector;

    private void Start()
    {
        DBConnector = new SupabaseManager();
        GetTopStudent();
    }

    // Use to update scoreboard UI
    private void UpdateUI()
    {
        foreach(Transform child in highscoreContainerTransform)
        {
            Destroy(child.gameObject);
        }
        int rank = 1;
        foreach(Student highscore in playerData.students)
        {
            Instantiate(scoreboardEntryObject, highscoreContainerTransform).
                GetComponent<ScoreboardEntryUI>().Initialise(rank, highscore);
            rank++;
        }
    }

    private void GetTopStudent()
    {
        StartCoroutine(GetTopTenPlayer_Coroutine());
        //return JsonUtility.FromJson<StudentList>(DBConnector.jsonData);
    }

    IEnumerator GetTopTenPlayer_Coroutine()
    {
        yield return DBConnector.GetTopTenStudentData();
        Debug.Log(DBConnector.jsonData);
        playerData = JsonUtility.FromJson<StudentList>(DBConnector.jsonData);
        UpdateUI();
    }
}
