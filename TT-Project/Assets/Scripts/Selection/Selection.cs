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
        Debug.Log("Scene memory game loaded");
        SceneManager.LoadScene("memory game");
    }

    public void type2()
    {
        Debug.Log("Scene sequence_memory_game loaded");
        SceneManager.LoadScene("sequence_memory_game");
    }

    public void type3()
    {
        Debug.Log("Scene ReverseRetention loaded");
        SceneManager.LoadScene("ReverseRetention_1");
    }

    public void type4()
    {
        Debug.Log("Scene Random-memo loaded");
        SceneManager.LoadScene("MemoRandom");
    }

    public void type5()
    {
        Debug.Log("Loading Random scene");
        Mix();
        int random = Random.Range(0,3);
        if (random == 0)
        {
            type2();
        }else if(random == 1)
        {
            type3();
        }else if(random == 2)
        {
            type4();
        }
    }

    public void Mix()
    {
        random.Value = true;
    }
}
