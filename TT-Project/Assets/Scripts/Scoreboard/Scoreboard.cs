using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private int maxScoreboardEntries = 10;
    [SerializeField] private TextMeshProUGUI header;
    [SerializeField] private Transform highscoreContainerTransform = null;
    [SerializeField] private GameObject scoreboardEntryObject = null;
    [SerializeField] private Transform currRankContainerTransform = null;
    [SerializeField] private GameObject currRankEntryObject = null;

    [SerializeField] private StringSO usernameSO;
    [SerializeField] private IntSO rankFromBS_SO;
    [SerializeField] private IntSO rankFromRS_SO;
    [SerializeField] private IntSO numberOfPlayer_SO;
    [SerializeField] private ScoreboardSO scoreboard_SO;

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
        // �֧������ username ��� best score �ͧ���Ф��Ҩҡ supabase � table ������͡��
        yield return DBConnector.API_GET_Coroutine(scoreboard_SO.gameTypeTable+"?select=username,best_score,recent_score&order=best_score.desc,username.asc");
        // �Ӣ������ jsonData ���ŧ�� class �ͧ C# �·������� class ��鹵�ͧ�ժ��ͷ��ç�Ѻ database Ẻ��� �
        allPlayer_bestScore = JsonUtility.FromJson<Player_AllScoreList>(DBConnector.jsonData);
        Debug.Log("All player score: " + DBConnector.jsonData);

        UpdateUI();

        // save rank for current player to unity
        rankFromBS_SO.Value = computeCurrRankFromBS();
        rankFromRS_SO.Value = computeCurrRankFromRS();
        numberOfPlayer_SO.Value = allPlayer_bestScore.jsonData.Length + 1;
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
            // ����ҡ recent score ����դ����ҡѺ best score �ͧ���ͧ ���� recent score �դ���ҡ���Ҽ����蹤��Ѵ�
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
}
