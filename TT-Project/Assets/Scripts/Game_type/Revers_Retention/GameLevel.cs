using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField]
    DDA DDAs;

    [SerializeField]
    LevelInfoSO LevelInfoSOs;

    private bool isSet;
    private string[] imageLevel = { "Difficult", "Normal" };

    public void levelSet() {
        if (!isSet)
        {
            isSet = true;
            if(DDAs.Level == 1)
            {
                int temp = Random.Range(3, 5);
                LevelInfoSOs.Slot = temp;
                if(temp == 3)
                {
                    LevelInfoSOs.Img = imageLevel[0];
                }
                else
                {
                    LevelInfoSOs.Img = imageLevel[1];
                }
            }
            else if(DDAs.Level == 2)
            {
                int temp = Random.Range(4, 6);
                LevelInfoSOs.Slot = temp;
                if (temp == 4)
                {
                    LevelInfoSOs.Img = imageLevel[0];
                }
                else
                {
                    LevelInfoSOs.Img = imageLevel[1];
                }
            }
            else if (DDAs.Level == 3)
            {
                int temp = Random.Range(5, 7);
                LevelInfoSOs.Slot = temp;
                if (temp == 5)
                {
                    LevelInfoSOs.Img = imageLevel[0];
                }
                else
                {
                    LevelInfoSOs.Img = imageLevel[1];
                }
            }
        }
    }

    public void setEnd()
    {
        isSet = false;
    }
}
