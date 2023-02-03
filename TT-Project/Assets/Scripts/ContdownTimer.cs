using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContdownTimer : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 15f;
    float cutsceneTime = 6f;

    [SerializeField] 
    Text countdownText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        countdownText.text = startingTime.ToString("0");
        countdownText.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        if (cutsceneTime <= 0)
        {
            cutsceneTime = 0;

            currentTime -= Time.deltaTime;
            countdownText.text = currentTime.ToString("0");

            //countdownText.color = Color.green;

            if (currentTime <= 0)
            {
                currentTime = 0;
            }
        }else
        {
            cutsceneTime -= Time.deltaTime;
        }

    }
}
