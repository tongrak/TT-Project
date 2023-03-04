using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RDM_AddRememBtn : MonoBehaviour
{
    [SerializeField]
    private Transform puzzlefield;

    [SerializeField]
    private GameObject button;

    private void Awake()
    {
        if (button.tag == "PuzzleBtn")
        {
            for (int i = 0; i < 4; i++)
            {
                //  create puzzle btn
                GameObject _button = Instantiate(button);
                _button.name = "" + i;  //  Rename button
                _button.transform.SetParent(puzzlefield, false);
            }
        }else if (button.tag == "AnswerBtn")
        {
            for (int i = 4; i < 8; i++)
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
