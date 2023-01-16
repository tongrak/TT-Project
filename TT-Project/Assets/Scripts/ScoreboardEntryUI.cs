using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreboardEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI entryRankText = null;
    [SerializeField] private TextMeshProUGUI entryNameText = null;
    [SerializeField] private TextMeshProUGUI entryScoreText = null;

    public void Initialise(int rank,Student player)
    {
        entryRankText.text = rank.ToString();
        entryNameText.text = player.fname;
        entryScoreText.text = player.id.ToString();
    }
}
