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

        float xPos_recent = ((barLength / (amountOfPlayer - 1)) * (recentScoreRank - 1));
        float xPos_best = ((barLength / (amountOfPlayer - 1)) * (bestScoreRank - 1));

        // set recent score line position
        recentLine.transform.localPosition = new Vector3(recentLine.transform.localPosition.x - xPos_recent, recentLine.transform.localPosition.y, recentLine.transform.localPosition.z);
        // set best score line position
        bestLine.transform.localPosition = new Vector3(bestLine.transform.localPosition.x - xPos_best, bestLine.transform.localPosition.y, bestLine.transform.localPosition.z);

        // set recent text position
        recentText.transform.localPosition = new Vector3(recentText.transform.localPosition.x - xPos_recent, recentText.transform.localPosition.y, recentText.transform.localPosition.z);
        // set best text position
        bestText.transform.localPosition = new Vector3(bestText.transform.localPosition.x - xPos_best, bestText.transform.localPosition.y, bestText.transform.localPosition.z); 
    }   
}
