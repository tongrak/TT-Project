using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBar : MonoBehaviour
{
    [Header("Game objects")]
    [SerializeField] private Transform scoreBarContainerTransform = null;
    [SerializeField] private GameObject scoreBarEntryObject = null;
    [SerializeField] private TextMeshProUGUI recentText = null;
    [SerializeField] private TextMeshProUGUI bestText = null;
    [Header("SO_Data")]
    [SerializeField] private IntSO rankFromBS_SO;
    [SerializeField] private IntSO rankFromRS_SO;
    [SerializeField] private IntSO numberOfPlayer_SO;
    [SerializeField] private IntSO recentScore_SO;
    [SerializeField] private IntSO bestScore_SO;
    [SerializeField] private IntSO highestScore_SO;
    [SerializeField] private IntSO lowest_SO;
    // Start is called before the first frame update
    void Start()
    {
        // delete old score bar
        foreach (Transform child in scoreBarContainerTransform)
        {
            Destroy(child.gameObject);
        }
        // create new score bar
        Instantiate(scoreBarEntryObject, scoreBarContainerTransform).
            GetComponent<ScoreBarEntryUI>().Initialise(rankFromRS_SO.Value, rankFromBS_SO.Value, numberOfPlayer_SO.Value, highestScore_SO.Value, lowest_SO.Value);

        //show score
        recentText.text = "Recent score: " + recentScore_SO.Value.ToString();
        bestText.text = "Best score: " + bestScore_SO.Value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
