using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectScoreboard : MonoBehaviour
{
    [SerializeField] private ScoreboardSO sb_SO;
    public void SelectSB_button(string header, string gameTypeTable)
    {
        sb_SO.scoreboardHeader = header;
        sb_SO.gameTypeTable = gameTypeTable;
        //sb_SO.bestScoreSO = bestScore_SO;
    }
    
    public void Test_button()
    {

    }
}
