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

    // @param amountOfPlayer นั้นคือจำนวนผู้เล่นทั้งหมด(รวมทั้งตัว player ปัจจุบันด้วย)+1
    public void Initialise(int recentScoreRank, int bestScoreRank, int amountOfPlayer)
    {
        RectTransform rt_bar = (RectTransform)bar.transform;
        float barLength = rt_bar.rect.width;

        float xPos_recent = (barLength / (amountOfPlayer - 1)) * (recentScoreRank - 1);
        float xPos_best = (barLength / (amountOfPlayer - 1)) * (bestScoreRank - 1);

        // set recent score line position
        recentLine.transform.position = new Vector2(xPos_recent, recentLine.transform.position.y);
        // set best score line position
        bestLine.transform.position = new Vector2(xPos_best, bestLine.transform.position.y);

        if(xPos_recent == xPos_best)
        {
            // set best&recent text position
            bestText.transform.position = new Vector2(xPos_best, bestLine.transform.position.y);
            bestText.text = "Best,Recent";
        }
        else
        {
            // set recent text position
            recentLine.transform.position = new Vector2(xPos_recent, recentLine.transform.position.y);
            // set best text position
            bestLine.transform.position = new Vector2(xPos_best, bestLine.transform.position.y);

        }
    }
}
