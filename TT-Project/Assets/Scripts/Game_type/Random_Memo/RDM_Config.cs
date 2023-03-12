using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// use easyConfig to get int[] that contain config for easy
// use normalConfig to get int[] that contain config for normal
// use hardConfig to get int[] that contain config for hard
public class RDM_Config : MonoBehaviour
{
    private static string easy = "Assets\\Resources\\Puzzle\\Memo_random\\easy_level\\easy.txt";
    private static string normal = "Assets\\Resources\\Puzzle\\Memo_random\\normal_level\\normal.txt";
    private static string hard = "Assets\\Resources\\Puzzle\\Memo_random\\hard_level\\hard.txt";
    private static int[] result = new int[4];
    //  result[0] = NoOfCard;   //จำนวนบน
    //  result[1] = NoOfKind;   
    //  result[2] = NoOfSelectCard; //จำนวนการ์ดล่าง
    //  result[3] = NoOfFlipCard;   //จำนวนการ์ดหงาย
    public static int[] easyConfig()
    {
        string[] str = System.IO.File.ReadAllLines(@easy);
        return random(str);
    }

    public static int[] normalConfig()
    {
        string[] str = System.IO.File.ReadAllLines(@normal);
        return random(str);
    }

    public static int[] hardConfig()
    {
        string[] str = System.IO.File.ReadAllLines(@hard);
        return random(str);
    }

    private static int[] random(string[] str)
    {
        int NoOfCard = UnityEngine.Random.Range(Int16.Parse(str[1]),Int16.Parse(str[2])+1);
        int NoOfKind = UnityEngine.Random.Range(Int16.Parse(str[4]),Int16.Parse(str[5])+1);
        int NoOfSelectCard = UnityEngine.Random.Range(Int16.Parse(str[7]),Int16.Parse(str[8])+1);
        int NoOfFlipCard = UnityEngine.Random.Range(Int16.Parse(str[10]),Int16.Parse(str[11])+1);
        result[0] = NoOfCard;
        result[1] = NoOfKind;
        result[2] = NoOfSelectCard;
        result[3] = NoOfFlipCard;
        return result;
    }
}
