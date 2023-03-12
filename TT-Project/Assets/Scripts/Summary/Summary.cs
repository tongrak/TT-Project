using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Summary : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private FloatSO scoreSO;

    private void Start()
    {
        scoreText.text = scoreSO.Value.ToString();
    }
}
