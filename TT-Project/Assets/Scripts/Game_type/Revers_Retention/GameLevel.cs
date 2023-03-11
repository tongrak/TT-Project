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
            print("WAH");
            isSet = true;
            if(DDAs.Level == 1)
            {
                LevelInfoSOs.Slot = Random.Range(3, 5);
                int temp = Random.Range(0, 1);
                LevelInfoSOs.Img = imageLevel[temp];
            }
            else if(DDAs.Level == 2)
            {
                LevelInfoSOs.Slot = Random.Range(4, 6);
                int temp = Random.Range(0, 1);
                LevelInfoSOs.Img = imageLevel[temp];
            }
            else if (DDAs.Level == 3)
            {
                LevelInfoSOs.Slot = Random.Range(5, 7);
                int temp = Random.Range(0, 1);
                LevelInfoSOs.Img = imageLevel[temp];
            }
        }
    }
}
