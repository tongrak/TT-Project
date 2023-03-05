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
    [SerializeField] private string score_table;
    //[SerializeField] private IntSO MemRand_bestScoreSO;
    //[SerializeField] private IntSO Rev_bestScoreSO;
    //[SerializeField] private IntSO Mix_bestScoreSO;
    private SupabaseManager dbConnector;
    private PlayerList playerData;
    private int playerRank = 1;
    // Start is called before the first frame update
    void Start()
    {
        GetAllPlayer();
    }

    private void Awake()
    {
        dbConnector = SupabaseManager.getInstance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateUI()
    {
        //string username = usernameSO.Value;
        RankText.text = playerRank.ToString();
        NameText.text = usernameSO.Value;
        ScoreText.text = Seq_bestScore.Value.ToString();
    }

    private void searchRank()
    {
        Debug.Log("search rank for: " + usernameSO.Value);
        int rank = 1;
        foreach(Player_Score p in playerData.jsonData)
        {
            if(p.Player_name == usernameSO.Value)
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
        yield return dbConnector.API_GET_Coroutine("Player_Score?order=Best_score.desc");

        // นำข้อมูลใน jsonData มาแปลงเป็น class ของ C# โดยที่ตัวแปรใน class นั้นต้องมีชื่อที่ตรงกับ database แบบเป๊ะ ๆ
        playerData = JsonUtility.FromJson<PlayerList>(dbConnector.jsonData);
        Debug.Log(dbConnector.jsonData);
        searchRank();
        UpdateUI();
    }
}
