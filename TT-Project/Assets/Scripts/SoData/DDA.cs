using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]

public class DDA : ScriptableObject
{
    [SerializeField]
    private int level = 1;

    [SerializeField]
    private int wx;

    [SerializeField]
    private int wy;

    [SerializeField]
    private List<Double> _x = new List<Double>();

    [SerializeField]
    private List<Double> _y = new List<Double>();

    [SerializeField]
    private Double _accum;

    [SerializeField]
    private List<Double> _performance = new List<Double>();

    [SerializeField]
    private Double Max;

    [SerializeField]
    private Double Min;

    [SerializeField]
    private Double Mean;

    [SerializeField]
    private Double SD;

    //weight

    //easy
    private Double wxe1 = 0.5;
    private Double wye2 = 0.4;
    private Double accum_e = 0.1;
    //normal
    private Double wxn1 = 0.5;
    private Double wyn2 = 0.5;
    private Double accum_n = 0.09;
    //hard
    private Double wxh1 = 0.5;
    private Double wyh2 = 0.55;
    private Double accum_h = 0.08;


    public Double Accum
    {
        get { return _accum; }
        set { _accum = value; }
    }

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    public int Wx
    {
        get { return wx; }
        set { wx = value; }
    }

    public int Wy
    {
        get { return wy; }
        set { wy = value; }
    }

    public void addX(int value)
    {
        _x.Add(value);
        _y.Add(0);
    }


    public void addY(int value)
    {
        _y.Add(value);
        _x.Add(0);
    }

    public void heuristic()
    {
        if (this.level == 1)
        {
            this._performance.Add((wxe1 * _x[_x.Count - 1] - wye2 * _y[_y.Count - 1]) + (accum_e* this._accum));
        }
        else if (this.level == 2)
        {
            this._performance.Add((wxn1 * _x[_x.Count - 1] - wyn2 * _y[_y.Count - 1]) + (accum_n * this._accum));
        }
        else
        {
            this._performance.Add((wxh1 * _x[_x.Count - 1] - wyh2 * _y[_y.Count - 1]) + (accum_h * this._accum));
        }
        
    }

    public void Placeholder()
    {
        if (this.level == 1)
        {
            this._performance.Add((wxe1 * _x[_x.Count - 1] - wye2 * _y[_y.Count - 1]));
        }
        else if (this.level == 2)
        {
            this._performance.Add((wxn1 * _x[_x.Count - 1] - wyn2 * _y[_y.Count - 1]));
        }
        else
        {
            this._performance.Add((wxh1 * _x[_x.Count - 1 ] - wyh2 * _y[_y.Count - 1]));
        }
    }


    public Double min()
    {
        List<Double> temp = this._performance;
        temp.Sort();
        this.Min = temp[0];
        return temp[0];
    }

    public Double max()
    {
        List<Double> temp = this._performance;
        temp.Sort();
        this.Max = temp[temp.Count - 1];
        return temp[temp.Count-1];
    }

    public Double mean()
    {
        Double sum = 0;

        for(int i = 0; i < this._performance.Count; i++)
        {
            sum += _performance[i];
        }

        Double result = sum / (_performance.Count);
        this.Mean = result;
        return result;
    }

    public Double std()
    {
        Double sd;
        Double sum = 0;

        for (int i = 0; i < _performance.Count; i++)
        {
            sum += math.pow((_performance[i] - mean()), 2);
            Debug.Log(sum);
        }

        if(_performance.Count > 1)
        {
            Debug.Log("xxxxxx");
            sd = math.sqrt(sum / _performance.Count);
            Debug.Log(sd);
        }
        else
        {
            Debug.Log("yyyyyy");
            sd = math.sqrt(sum / _performance.Count);
        }

        this.SD = sd;
        return sd;
    }

    public void Reset()
    {
        this.level = 1;
        this.Wx = 0;
        this.Wy = 0;
        this._x.Clear();
        this._y.Clear();
        this._performance.Clear();
        this._accum = 0;
        this.Mean = 0;
        this.Min= 0;
        this.Max = 0;
    }

    public void reLevel()
    {
        min();
        max();
        mean();
        std();
        if (this.level == 1)
        {
            if(min() < 0.5 && min() > 0)
            {
                if(mean() < 0.5)
                {
                    if(max() < 0)
                    {
                        if(this.level != 1)
                        {
                            this.level -= 1;
                        }
                        
                    }
                    else if(max() > 0.5)
                    {
                        if (std() > 1)
                        {
                            if (this.level != 1)
                            {
                                this.level -= 1;
                            }
                        }
                        else
                        {
                            if (this.level != 3)
                            {
                                this.level += 1;
                            }
                        }
                    }
                    
                }
                else if (mean() > 0.5)
                {
                    if (this.level != 3)
                    {
                        this.level += 1;
                    }
                }
                
                
            }
            else if (min() <= 0 && min() >= -0.5)
            {
                if (mean() > 0.5)
                {
                    if (this.level != 3)
                    {
                        this.level += 1;
                    }
                }

            }
            else
            {
                if (this.level != 3)
                {
                    this.level += 1;
                }
            }
        }
    }

    public void reLevel2()
    {
        if(min() < 0.5 && mean() < 0.5 && max() < 0 && std() >= 0.25 && std() <= 1)
        {
            Debug.Log("1111111111111111111111111111");
            if (this.level != 1)
            {
                this.level -= 1;
            }
        }
        else if (min() < 0.5 && mean() < 0.5 && max() > 0.5 && std() > 1)
        {
            Debug.Log("2222222222222222222222222222");
            if (this.level != 1)
            {
                this.level -= 1;
            }
        }
        else if (min() < 0.5 && mean() < 0.5 && max() > 0.5 && std() >= 0.5 && std() <= 1)
        {
            Debug.Log("3333333333333333333333333");
            if (this.level != 3)
            {
                this.level += 1;
            }
        }
        else if (min() < 0.5 && mean() > 0.5 && max() > 0.5 && std() >= 0.25 && std() <= 1)
        {
            Debug.Log("44444444444444444444444");
            if (this.level != 3)
            {
                this.level += 1;
            }
        }
        else if (min() <= 0 && min() >= -0.5 && mean() > 0.5 && max() > 0.5 && std() >= 0.25 && std() <= 1)
        {
            Debug.Log("5555555555555555555555");
            if (this.level != 3)
            {
                this.level += 1;
            }
        }
        else if (min() <= 0 && min() >= -0.5 && mean() > 0.5 && max() > 0.5 && std() > 1)
        {
            Debug.Log("6666666666666666666666");
            if (this.level != 3)
            {
                this.level += 1;
            }
        }
        else if (min() > 0 && mean() > 0.5 && max() > 0.5 && std() > 1)
        {
            Debug.Log("7777777777777777777777777");
            if (this.level != 3)
            {
                this.level += 1;
            }
        }
        else if (min() > 0 && mean() > 0.5 && max() > 0.5 && std() >= 0.25 && std() <= 1)
        {
            Debug.Log("888888888888888888888");
            if (this.level != 3)
            {
                this.level += 1;
            }
        }
        else
        {
            Debug.Log("Fineeeeeeeee!?");
        }
    }

    public void reLevel3()
    {
        if (this._performance[_performance.Count-1] < 0 && this.Accum == 0)
        {
            if (this.level != 1)
            {
                this.level -= 1;
            }
        }
        else if (this._performance[_performance.Count - 1] < 1)
        {

        }
        else
        {
            if (this.level != 3)
            {
                this.level += 1;
            }
        }
    }

    public void check(bool cor)
    {
        if (cor)
        {
            this.Wx += 1;
            this.addX(this.Wx);
            this.Accum += 1;
            this.heuristic();
            this.reLevel3();
        }
        else
        {
            this.Wy += 1;
            this.Accum = 0;
            this.addY(this.Wy);
            this.heuristic();
            this.reLevel3();
        }
    }

}
