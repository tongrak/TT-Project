using System.Collections;
using System.Linq;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private int maxScoreboardEntries = 10;
    [SerializeField] private Transform highscoreContainerTransform = null;
    [SerializeField] private GameObject scoreboardEntryObject = null;
    private PlayerList playerData;
    private SupabaseManager DBConnector;

    private void Start()
    {
        DBConnector = SupabaseManager.getInstance();
        GetTopTenPlayer();
    }

    // Use to update scoreboard UI
    private void UpdateUI()
    {
        // Delete child object in HigscoreContainer
        foreach(Transform child in highscoreContainerTransform)
        {
            Destroy(child.gameObject);
        }

        // Create score list from 
        int rank = 1;
        foreach(Player_Score highscore in playerData.players.Take(maxScoreboardEntries))
        {
            // Instantiate use for cloning scoreboardEntryObject inside highscoreContainer to build ranking list.
            // GetComponent use for getting object that same type as ScoreboardEntryUI.
            Instantiate(scoreboardEntryObject, highscoreContainerTransform).
                GetComponent<ScoreboardEntryUI>().Initialise(rank, highscore);
            rank++;
        }
    }

    private void GetTopTenPlayer()
    {
        StartCoroutine(GetTopTenPlayer_Coroutine());
    }

    IEnumerator GetTopTenPlayer_Coroutine()
    {
        yield return DBConnector.API_GET_Coroutine("Player_Score?limit=10^&order=Best_score.desc", "players");
        Debug.Log(DBConnector.jsonData);
        
        // �Ӣ������ jsonData ���ŧ�� class �ͧ C# �·������� class ��鹵�ͧ�ժ��ͷ��ç�Ѻ database Ẻ��� �
        playerData = JsonUtility.FromJson<PlayerList>(DBConnector.jsonData);
        UpdateUI();
    }
}
