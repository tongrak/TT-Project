using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RDM_GameLevel : MonoBehaviour
{
    [SerializeField]
    DDA DDAs;

    [SerializeField]
    RDM_LevelInfoSO RDM_LevelInfoSOs;

    [SerializeField]
    RDM_Config RDM_Configs;


    private bool isSet;
    private string[] imageLevel = { "Difficult", "Normal" };

    public void levelSet() {
        if (!isSet)
        {
            isSet = true;
            int[] temp; 
            if(DDAs.Level == 1)
            {
                temp = RDM_Config.easyConfig();
            }
            else if(DDAs.Level == 2)
            {
                temp = RDM_Config.normalConfig();
            }
            else 
            {
                temp = RDM_Config.hardConfig();
            }
            while (temp[0] > temp[2])
            {
                temp[2]++;
            }
            RDM_LevelInfoSOs.SlotTop = temp[0];
            RDM_LevelInfoSOs.SlotDown = temp[2];
            RDM_LevelInfoSOs.SlotOpened = temp[3];
        }
    }

    public void setEnd()
    {
        isSet = false;
    }
}
