using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField]
    Text countdownText;

    float currentTime = 0f;
    float startingTime = 180f;

    void Start()
    {
        currentTime = startingTime;
    }

    
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("00");

        if(currentTime <= 0)
        {
            currentTime = 0;
        }
    }
}
