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
    private TMP_Text timeText;

    [SerializeField]
    private FloatSO timeCount;

    [SerializeField]
    private boolFortime pause;


    private void Start()
    {
        timeText.text = timeCount.Value.ToString("00");
    }

    void Update()
    {

        if (!pause.Value)
        {
            
            timeCount.Value -= 1 * Time.deltaTime;

            timeText.text = timeCount.Value.ToString("00");

            if (timeCount.Value <= 0)
            {
                timeCount.Value = 0;
            }
        }
        
    }
}
