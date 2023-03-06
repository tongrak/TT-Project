using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBarEntryUI : MonoBehaviour
{
    [SerializeField] private GameObject bar;
    [SerializeField] private GameObject recentLine;
    [SerializeField] private GameObject bestLine;
    [SerializeField] private TextMeshProUGUI recentText;
    [SerializeField] private TextMeshProUGUI bestText;

    public void Initialise(int rank, Player_BestScore player)
    {
        
    }
}
