using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Selection : MonoBehaviour
{

    [SerializeField]
    private boolFortime random;
    public void type1()
    {
        Debug.Log("Secene memory game loaded");
        SceneManager.LoadScene("memory game");
    }

    public void type2()
    {
        Debug.Log("Secene sequence_memory_game loaded");
        SceneManager.LoadScene("sequence_memory_game");
    }

    public void type3()
    {
        Debug.Log("Secene ReverseRetention loaded");
        SceneManager.LoadScene("ReverseRetention_1");
    }

    public void type4()
    {
        Debug.Log("Loading Random-memo scenes");
        SceneManager.LoadScene("MemoRandom");
    }

    public void type5()
    {
        Debug.Log("Loading Random scenes");
        int random = Random.Range(0,4);
        if (random == 0)
        {
            type1();
        }else if (random == 1)
        {
            type2();
        }else if(random == 2)
        {
            type3();
        }else if(random == 3)
        {
            type4();
        }
        //SceneManager.LoadScene("Random-memo");
    }

    public void Mix()
    {
        random.Value = true;
    }
}
