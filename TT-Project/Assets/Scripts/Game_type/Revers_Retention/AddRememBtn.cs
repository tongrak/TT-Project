using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRememBtn : MonoBehaviour
{
    [SerializeField]
    private Transform puzzlefield;

    [SerializeField]
    private GameObject button;

    [SerializeField]
    private GameLevel GameLevels;

    [SerializeField]
    private LevelInfoSO LevelInfoSOs;

    private void Awake()
    {
        GameLevels.levelSet();
        if (button.tag == "PuzzleBtn")
        {
            for (int i = 0; i < LevelInfoSOs.Slot; i++)
            {
                //  create puzzle btn
                GameObject _button = Instantiate(button);
                _button.name = "" + i;  //  Rename button
                _button.transform.SetParent(puzzlefield, false);
            }
        }else if (button.tag == "AnswerBtn")
        {
            for (int i = LevelInfoSOs.Slot; i < LevelInfoSOs.Slot*2; i++)
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
