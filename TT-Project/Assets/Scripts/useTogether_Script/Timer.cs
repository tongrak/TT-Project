using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{


    //[SerializeField]
    //Text countdownText;

    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private FloatSO timeCount;

    
    void Update()
    {
        timeCount.Value -= 1 * Time.deltaTime;
        //countdownText.text = timeCount.Value.ToString("00");
        scoreText.text = timeCount.Value.ToString("00");

        if (timeCount.Value <= 0)
        {
            timeCount.Value = 0;
        }
    }
}
