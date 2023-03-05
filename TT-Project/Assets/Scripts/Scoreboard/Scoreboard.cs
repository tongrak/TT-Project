using System.Collections;
using System.Linq;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private int maxScoreboardEntries = 10;
    [SerializeField] private Transform highscoreContainerTransform = null;
    [SerializeField] private GameObject scoreboardEntryObject = null;
    [SerializeField] private GameObject currPlayerRankEntryObject = null;
    [SerializeField] private string score_table;
    [SerializeField] private StringSO usernameSO;
    [SerializeField] private IntSO bestScoreSO;
    private PlayerList playerData;
    private SupabaseManager DBConnector;
    private Player_BestScoreList allPlayer_bestScore;

    private void Awake()
    {
        DBConnector = SupabaseManager.getInstance();
    }

    private void Start()
    {
        StartCoroutine(GetAllPlayerBestScore_Coroutine());
    }

    // Use to update scoreboard UI
    private void UpdateUI()
    {
        // Delete child object in HigscoreContainer
        foreach (Transform child in highscoreContainerTransform)
        {
            Destroy(child.gameObject);
        }

        // Create score list from 
        int rank = 1;
        foreach (Player_BestScore highscore in allPlayer_bestScore.jsonData.Take(maxScoreboardEntries))
        {
            // Instantiate use for cloning scoreboardEntryObject inside highscoreContainer to build ranking list.
            // GetComponent use for getting object that same type as ScoreboardEntryUI.
            Instantiate(scoreboardEntryObject, highscoreContainerTransform).
                GetComponent<ScoreboardEntryUI>().Initialise(rank, highscore);
            rank++;
        }
    }

    private IEnumerator GetAllPlayerBestScore_Coroutine()
    {
        // ดึงข้อมูล username และ best score ของแต่ละคนมาจาก supabase ใน table ที่เลือกมา
        yield return DBConnector.API_GET_Coroutine(score_table+"?select=username,best_score&order=best_score.desc,username.asc");
        // นำข้อมูลใน jsonData มาแปลงเป็น class ของ C# โดยที่ตัวแปรใน class นั้นต้องมีชื่อที่ตรงกับ database แบบเป๊ะ ๆ
        allPlayer_bestScore = JsonUtility.FromJson<Player_BestScoreList>(DBConnector.jsonData);
        Debug.Log("All player best score: " + DBConnector.jsonData);

        UpdateUI();
    }

    //private void GetTopTenPlayer()
    //{
    //    StartCoroutine(GetTopTenPlayer_Coroutine());
    //}

    //IEnumerator GetTopTenPlayer_Coroutine()
    //{
    //    yield return DBConnector.API_GET_Coroutine("Player_Score?limit=10^&order=Best_score.desc");
    //    //Debug.Log(DBConnector.jsonData);
        
    //    //// นำข้อมูลใน jsonData มาแปลงเป็น class ของ C# โดยที่ตัวแปรใน class นั้นต้องมีชื่อที่ตรงกับ database แบบเป๊ะ ๆ
    //    //playerData = JsonUtility.FromJson<PlayerList>(DBConnector.jsonData);
    //    UpdateUI();
    //}
}
