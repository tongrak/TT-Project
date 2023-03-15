using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// use easyConfig to get int[] that contain config for easy
// use normalConfig to get int[] that contain config for normal
// use hardConfig to get int[] that contain config for hard
public class RDM_Config : MonoBehaviour
{
    private static int[] result = new int[4];
    //  result[0] = NoOfCard;   //จำนวนบน
    //  result[1] = NoOfKind;   
    //  result[2] = NoOfSelectCard; //จำนวนการ์ดล่าง
    //  result[3] = NoOfFlipCard;   //จำนวนการ์ดหงาย

    private static int[] easyNum = { 1, 2 };
    private static int[] easykind = { 1, 2 };
    private static int[] easySelect = { 2, 3 };
    private static int[] easyFlip = { 1, 1 };

    private static int[] normalNum = { 2, 3 };
    private static int[] normalkind = { 2, 3 };
    private static int[] normalSelect = { 4, 4 };
    private static int[] normalFlip = { 2, 2 };

    private static int[] hardNum = { 4, 5 };
    private static int[] hardkind = { 2, 4 };
    private static int[] hardSelect = { 5, 6 };
    private static int[] hardFlip = { 3, 4 };

    public static int[] easyConfig()
    {
        string str = "easy";
        return random(str);
    }

    public static int[] normalConfig()
    {
        string str = "normal";
        return random(str);
    }

    public static int[] hardConfig()
    {
        string str = "hard";
        return random(str);
    }

    private static int[] random(string str)
    {
        int NoOfCard;
        int NoOfKind;
        int NoOfSelectCard;
        int NoOfFlipCard;
        if(str == "easy")
        {
            NoOfCard = UnityEngine.Random.Range(easyNum[0], easyNum[1]);
            NoOfKind = UnityEngine.Random.Range(easykind[0], easykind[1]);
            NoOfSelectCard = UnityEngine.Random.Range(easySelect[0], easySelect[1]);
            NoOfFlipCard = UnityEngine.Random.Range(easyFlip[0], easyFlip[1]);
        }else if(str == "normal")
        {
            NoOfCard = UnityEngine.Random.Range(normalNum[0], normalNum[1]);
            NoOfKind = UnityEngine.Random.Range(normalkind[0], normalkind[1]);
            NoOfSelectCard = UnityEngine.Random.Range(normalSelect[0], normalSelect[1]);
            NoOfFlipCard = UnityEngine.Random.Range(normalFlip[0], normalFlip[1]);
        }
        else
        {
            NoOfCard = UnityEngine.Random.Range(hardNum[0], hardNum[1]);
            NoOfKind = UnityEngine.Random.Range(hardkind[0], hardkind[1]);
            NoOfSelectCard = UnityEngine.Random.Range(hardSelect[0], hardSelect[1]);
            NoOfFlipCard = UnityEngine.Random.Range(hardFlip[0], hardFlip[1]);
        }
        result[0] = NoOfCard;
        result[1] = NoOfKind;
        result[2] = NoOfSelectCard;
        result[3] = NoOfFlipCard;
        return result;
    }

}
