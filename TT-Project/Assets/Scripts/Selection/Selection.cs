using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Selection : MonoBehaviour
{
    public void type1()
    {
        SceneManager.LoadScene("memory game");
    }

    public void type2()
    {
        SceneManager.LoadScene("ReverseRetention_1");
    }

    public void type3()
    {
        Debug.Log("Secene 3 loaded");
        //SceneManager.LoadScene("memory game");
    }

    public void type4()
    {
        Debug.Log("Loading random scenes");
        //SceneManager.LoadScene("memory game");
    }
}
