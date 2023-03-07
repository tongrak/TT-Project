using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBar : MonoBehaviour
{
    [SerializeField] private Transform scoreBarContainerTransform = null;
    [SerializeField] private GameObject scoreBarEntryObject = null;
    [SerializeField] private IntSO rankFromBS_SO;
    [SerializeField] private IntSO rankFromRS_SO;
    [SerializeField] private IntSO numberOfPlayer_SO;
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
            GetComponent<ScoreBarEntryUI>().Initialise(rankFromRS_SO.Value, rankFromBS_SO.Value, numberOfPlayer_SO.Value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
