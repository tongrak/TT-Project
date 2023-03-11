using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBarEntryUI : MonoBehaviour
{
    [SerializeField] private GameObject bar;
    [Header("Recent")]
    [SerializeField] private GameObject recentLine;
    [SerializeField] private GameObject recentTri;
    [Header("Best")]
    [SerializeField] private GameObject bestLine;
    [SerializeField] private GameObject bestTri;

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

        // set recent triangle positions
        recentTri.transform.localPosition = new Vector3(recentTri.transform.localPosition.x - xPos_recent , recentTri.transform.localPosition.y, recentTri.transform.localPosition.z);
        // set best triangle positions
        bestTri.transform.localPosition = new Vector3(bestTri.transform.localPosition.x - xPos_best , bestTri.transform.localPosition.y, bestTri.transform.localPosition.z);
    }   
}
