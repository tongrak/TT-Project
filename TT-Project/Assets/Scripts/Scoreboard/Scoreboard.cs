using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private int maxScoreboardEntries = 10;
    [Header("Game objects")]
    [SerializeField] private TextMeshProUGUI header;
    [SerializeField] private Transform highscoreContainerTransform = null;
    [SerializeField] private GameObject scoreboardEntryObject = null;
    [SerializeField] private Transform currRankContainerTransform = null;
    [SerializeField] private GameObject currRankEntryObject = null;
    [Header("SO_Data")]
    [SerializeField] private StringSO usernameSO;
    [SerializeField] private IntSO rankFromBS_SO;
    [SerializeField] private IntSO rankFromRS_SO;
    [SerializeField] private IntSO numberOfPlayer_SO;
    [SerializeField] private ScoreboardSO scoreboard_SO;
    [SerializeField] private IntSO recentSO;
    [SerializeField] private IntSO bestSO;
    [SerializeField] private IntSO highestScore_SO;
    [SerializeField] private IntSO lowestScore_SO;

    private SupabaseManager DBConnector;
    private Player_AllScoreList allPlayer_bestScore;

    private void Awake()
    {
        DBConnector = SupabaseManager.getInstance();
    }

    private void Start()
    {
        GetAllPlayerAllScore();
    }

    // Use to update scoreboard UI
    private void UpdateUI()
    {
        // Change scoreboard header follow by each game type
        header.text = scoreboard_SO.scoreboardHeader;
        // Delete child object in HigscoreContainer
        foreach (Transform child in highscoreContainerTransform)
        {
            Destroy(child.gameObject);
        }

        // Create score list form 
        int rank = 1;
        foreach (Player_AllScore highscore in allPlayer_bestScore.jsonData.Take(maxScoreboardEntries))
        {
            // Instantiate use for cloning scoreboardEntryObject inside highscoreContainer to build ranking list.
            // GetComponent use for getting object that same type as ScoreboardEntryUI.
            Instantiate(scoreboardEntryObject, highscoreContainerTransform).
                GetComponent<ScoreboardEntryUI>().Initialise(rank, new Player_BestScore(highscore.username, highscore.best_score));
            rank++;
        }

        // Delete child object in CurrRankContainer except first gameObject
        int transform_index = 0;
        foreach (Transform child in currRankContainerTransform)
        {
            if(transform_index != 0)
            {
                Destroy(child.gameObject);
            }
            transform_index++;
        }
        // Create current player rank form
        rank = computeCurrRankFromBS();
        Instantiate(currRankEntryObject, currRankContainerTransform).
                GetComponent<ScoreboardEntryUI>().Initialise(rank, new Player_BestScore(usernameSO.Value, scoreboard_SO.bestScoreSO.Value));
    }

    private void GetAllPlayerAllScore()
    {
        StartCoroutine(GetAllPlayerAllScore_Coroutine());
    }

    private IEnumerator GetAllPlayerAllScore_Coroutine()
    {
        // ดึงข้อมูล username และ best score ของแต่ละคนมาจาก supabase ใน table ที่เลือกมา
        yield return DBConnector.API_GET_Coroutine(scoreboard_SO.gameTypeTable+"?select=username,best_score,recent_score&order=best_score.desc,username.asc");
        // นำข้อมูลใน jsonData มาแปลงเป็น class ของ C# โดยที่ตัวแปรใน class นั้นต้องมีชื่อที่ตรงกับ database แบบเป๊ะ ๆ
        allPlayer_bestScore = JsonUtility.FromJson<Player_AllScoreList>(DBConnector.jsonData);
        Debug.Log("All player score: " + DBConnector.jsonData);

        UpdateUI();

        // save rank for current player to unity
        rankFromBS_SO.Value = computeCurrRankFromBS();
        rankFromRS_SO.Value = computeCurrRankFromRS();
        numberOfPlayer_SO.Value = allPlayer_bestScore.jsonData.Length + 1;

        // set best and recent score to unity
        recentSO.Value = findRecentScore();
        bestSO.Value = scoreboard_SO.bestScoreSO.Value;

        // set highest score
        highestScore_SO.Value = findHighestScore();

        // set lowest score
        lowestScore_SO.Value = findLowestScore();
    }

    // find rank of current player form best score.
    private int computeCurrRankFromBS()
    {
        int rank = 1;
        foreach (Player_AllScore p in allPlayer_bestScore.jsonData)
        {
            if (p.username == usernameSO.Value)
            {
                return rank;
            }
            rank++;
        }
        return 0;
    }

    // find rank of current player form recent score.
    private int computeCurrRankFromRS()
    {
        int rank = 1;
        int recentScore = findRecentScore();
        foreach (Player_AllScore p in allPlayer_bestScore.jsonData)
        {
            // ถ้าหาก recent score นั้นมีค่าเท่ากับ best score ของตนเอง หรือ recent score มีค่ามากกว่าผู้เล่นคนถัดไป
            if((recentScore == p.best_score && p.username == usernameSO.Value) || recentScore > p.best_score)
            {
                return rank;
            }
            rank++;
        }
        return rank;
    }

    private int findRecentScore()
    {
        foreach (Player_AllScore p in allPlayer_bestScore.jsonData)
        {
            if(p.username == usernameSO.Value)
            {
                return p.recent_score;
            }
        }
        return 0;
    }

    private int findHighestScore()
    {
        return allPlayer_bestScore.jsonData[0].best_score;
    }

    private int findLowestScore()
    {
        return allPlayer_bestScore.jsonData[allPlayer_bestScore.jsonData.Length - 1].best_score;
    }
}
