using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]

public class DDA : ScriptableObject
{
    

    [SerializeField]
    private int _x;

    [SerializeField]
    private int _y;

    [SerializeField]
    private int _sum;


    //weight
    private int wxe1 = 2;
    private int wye2 = 0;
    private int wxn1 = 2;
    private int wyn2 = 1;
    private int wxh1 = 1;
    private int wyh2 = 3;

    public int Y
    {
        get { return _y; }
        set { _y = value; }
    }

    public int X
    {
        get { return _x; }
        set { _x = value; }
    }

    public int SUM
    {
        get { return _sum; }
        set { _sum = value; }
    }

    public void heuristic(int x,int y,string l)
    {
        if (l.Equals("e"))
        {
            this._sum = wxe1*x - wye2*y;
        }
        else if (l.Equals("n"))
        {
            this._sum = wxn1 * x - wyn2 * y;
        }
        else
        {
            this._sum = wxh1 * x - wyh2 * y;
        }
        
    }


    public void loadScenes()
    {
        SceneManager.LoadScene("sequence_memory_game");
    }
}
