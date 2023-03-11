using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreboardEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI entryRankText = null;
    [SerializeField] private TextMeshProUGUI entryNameText = null;
    [SerializeField] private TextMeshProUGUI entryScoreText = null;

    public void Initialise(int rank,Player_BestScore player)
    {
        entryNameText.text = player.username;
        entryRankText.text = rank.ToString();
        entryScoreText.text = player.best_score.ToString();
    }
}
