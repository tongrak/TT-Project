using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrPlayerRank : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI RankText = null;
    [SerializeField] private TextMeshProUGUI NameText = null;
    [SerializeField] private TextMeshProUGUI ScoreText = null;
    [SerializeField] private StringSO usernameSO;
    [SerializeField] private IntSO Seq_bestScore;
    [SerializeField] private IntSO MemRand_bestScoreSO;
    [SerializeField] private IntSO Rev_bestScoreSO;
    [SerializeField] private IntSO Mix_bestScoreSO;
    private SupabaseManager dbConnector;
    private PlayerList playerData;
    private int playerRank = 1;
    // Start is called before the first frame update
    void Start()
    {
        dbConnector = SupabaseManager.getInstance();
        GetAllPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateUI()
    {
        RankText.text = playerRank.ToString();
        NameText.text = usernameSO.Value;
        ScoreText.text = Seq_bestScore.Value.ToString();
    }

    private void searchRank()
    {
        int rank = 1;
        foreach(Player_Score p in playerData.players)
        {
            if(p.Player_name == PlayerData.username)
            {
                playerRank = rank;
                break;
            }
            rank++;
        }
    }

    private void GetAllPlayer()
    {
        StartCoroutine(GetAllPlayer_Coroutine());
    }

    IEnumerator GetAllPlayer_Coroutine()
    {
        yield return dbConnector.API_GET_Coroutine("Player_Score?order=Best_score.desc", "players");
        Debug.Log(dbConnector.jsonData);

        // นำข้อมูลใน jsonData มาแปลงเป็น class ของ C# โดยที่ตัวแปรใน class นั้นต้องมีชื่อที่ตรงกับ database แบบเป๊ะ ๆ
        playerData = JsonUtility.FromJson<PlayerList>(dbConnector.jsonData);

        searchRank();
        UpdateUI();
    }
}
