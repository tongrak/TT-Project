using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSBUI : MonoBehaviour
{
    [SerializeField] private string headerSB;
    [SerializeField] private string gameTypeTable;
    [SerializeField] private IntSO bestScoreSO;
    [SerializeField] private ScoreboardSO sb_SO;

    // Start is called before the first frame update
    void Start()
    {
        sb_SO.scoreboardHeader = headerSB;
        sb_SO.gameTypeTable = gameTypeTable;
        sb_SO.bestScoreSO = bestScoreSO;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
