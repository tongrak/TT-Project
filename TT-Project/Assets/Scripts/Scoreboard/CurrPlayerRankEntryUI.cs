using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrPlayerRankEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI entryRankText = null;
    [SerializeField] private TextMeshProUGUI entryNameText = null;
    [SerializeField] private TextMeshProUGUI entryScoreText = null;

    public void Initialise(int rank,int best_score)
    {
        entryNameText.text = "Yours";
        entryRankText.text = rank.ToString();
        entryScoreText.text = best_score.ToString();
    }
}
