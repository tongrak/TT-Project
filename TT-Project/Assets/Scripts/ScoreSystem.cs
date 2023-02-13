using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    string scoreKey = "Score";

    public int CurrentScore { get; set; }


    private void Awake()
    {
         CurrentScore = PlayerPrefs.GetInt(scoreKey);
    }

    public void SetScore(int score)
    {
        PlayerPrefs.SetInt(scoreKey, score);
    }
}
