using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RDM_AddRememBtn : MonoBehaviour
{
    [SerializeField]
    private Transform puzzlefield;

    [SerializeField]
    private GameObject button;

    [SerializeField]
    private RDM_GameLevel RDM_GameLevels;

    [SerializeField]
    private RDM_LevelInfoSO RDM_LevelInfoSOs;

    private int puzzleSize;
    private int ansSize;

    private void Awake()
    {
        RDM_GameLevels.levelSet();
        puzzleSize = RDM_LevelInfoSOs.SlotTop;
        ansSize = RDM_LevelInfoSOs.SlotDown;


        if (button.tag == "PuzzleBtn")
        {
            for (int i = 0; i < puzzleSize; i++)
            {
                //  create puzzle btn
                GameObject _button = Instantiate(button);
                _button.name = "" + i;  //  Rename button
                _button.transform.SetParent(puzzlefield, false);
            }
        }else if (button.tag == "AnswerBtn")
        {
            for (int i = puzzleSize; i < puzzleSize+ansSize; i++)
            {
                //  create puzzle btn
                GameObject _button = Instantiate(button);
                _button.name = "" + i;
                _button.transform.SetParent(puzzlefield, false);
            }
        }
        else
        {
            print("Tag error");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
