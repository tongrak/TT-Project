using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreboardEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI entryRankText = null;
    [SerializeField] private TextMeshProUGUI entryNameText = null;
    [SerializeField] private TextMeshProUGUI entryScoreText = null;

    public void Initialise(int rank,Player_Score player)
    {
        entryRankText.text = rank.ToString();
        entryNameText.text = player.Player_name;
        entryScoreText.text = player.Best_score.ToString();
    }
}
