using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{

    [SerializeField]
    private FloatSO scoreSO;

    [SerializeField]
    private TMP_Text showScore;

    [SerializeField]
    private TMP_Text number;

    // Start is called before the first frame update
    void Start()
    {
        showScore.text = scoreSO.Value + "";
        number.text = "-";
    }

    // Update is called once per frame
    void Update()
    {
        showScore.text = scoreSO.Value + "";
        if(scoreSO.Value > 0)
        {
            number.text = "7";
        }
    }
}
