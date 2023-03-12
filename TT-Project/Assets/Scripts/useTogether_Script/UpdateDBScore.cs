using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateDBScore : MonoBehaviour
{
    [SerializeField] private ScoreboardSO sb_SO;
    [SerializeField] private FloatSO scoreSO;
    [SerializeField] private StringSO usernameSO;

    private SupabaseManager dbConnector = new SupabaseManager();

    private void Start()
    {
        UpdateScoreInDB();
    }

    private void UpdateScoreInDB()
    {
        StartCoroutine(UpdateScoreInDB_Coroutine());   
    }

    IEnumerator UpdateScoreInDB_Coroutine()
    {
        // create request body
        Dictionary<string, string> reqBody = new Dictionary<string, string>();
        reqBody.Add("recent_score", scoreSO.Value.ToString());
        // if recent score is more than best score
        if (scoreSO.Value > sb_SO.bestScoreSO.Value)
        {
            reqBody.Add("best_score", scoreSO.Value.ToString());
        }
        // update score in DB
        yield return dbConnector.API_PATCH_Coroutine(reqBody, sb_SO.gameTypeTable+"?username=eq." + usernameSO.Value);
    }
}
