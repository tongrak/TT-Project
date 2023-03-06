using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private int maxScoreboardEntries = 10;
    [SerializeField] private Transform highscoreContainerTransform = null;
    [SerializeField] private GameObject scoreboardEntryObject = null;
    [SerializeField] private Transform CurrRankContainerTransform = null;
    [SerializeField] private GameObject currRankEntryObject = null;
    [SerializeField] private string score_table;
    [SerializeField] private StringSO usernameSO;
    [SerializeField] private IntSO bestScoreSO;

    private SupabaseManager DBConnector;
    private Player_AllScoreList allPlayer_bestScore;

    private void Awake()
    {
        DBConnector = SupabaseManager.getInstance();
    }

    private void Start()
    {
        GetAllPlayerBestScore();
    }

    // Use to update scoreboard UI
    private void UpdateUI()
    {
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
        foreach (Transform child in CurrRankContainerTransform)
        {
            if(transform_index != 0)
            {
                Destroy(child.gameObject);
            }
            transform_index++;
        }
        // Create current player rank form
        rank = computeCurrRankFromBS();
        Instantiate(currRankEntryObject, CurrRankContainerTransform).
                GetComponent<ScoreboardEntryUI>().Initialise(rank, new Player_BestScore(usernameSO.Value, bestScoreSO.Value));
    }

    private void GetAllPlayerBestScore()
    {
        StartCoroutine(GetAllPlayerBestScore_Coroutine());
    }

    private IEnumerator GetAllPlayerBestScore_Coroutine()
    {
        // �֧������ username ��� best score �ͧ���Ф��Ҩҡ supabase � table ������͡��
        yield return DBConnector.API_GET_Coroutine(score_table+"?select=username,best_score,recent_score&order=best_score.desc,username.asc");
        // �Ӣ������ jsonData ���ŧ�� class �ͧ C# �·������� class ��鹵�ͧ�ժ��ͷ��ç�Ѻ database Ẻ��� �
        allPlayer_bestScore = JsonUtility.FromJson<Player_AllScoreList>(DBConnector.jsonData);
        Debug.Log("All player score: " + DBConnector.jsonData);

        UpdateUI();
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
        int recentScore = findRecenScore();
        foreach (Player_AllScore p in allPlayer_bestScore.jsonData)
        {
            
            rank++;
        }
        return 0;
    }

    private int findRecenScore()
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

    //private void GetTopTenPlayer()
    //{
    //    StartCoroutine(GetTopTenPlayer_Coroutine());
    //}

    //IEnumerator GetTopTenPlayer_Coroutine()
    //{
    //    yield return DBConnector.API_GET_Coroutine("Player_Score?limit=10^&order=Best_score.desc");
    //    //Debug.Log(DBConnector.jsonData);
        
    //    //// �Ӣ������ jsonData ���ŧ�� class �ͧ C# �·������� class ��鹵�ͧ�ժ��ͷ��ç�Ѻ database Ẻ��� �
    //    //playerData = JsonUtility.FromJson<PlayerList>(DBConnector.jsonData);
    //    UpdateUI();
    //}
}
