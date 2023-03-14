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
    private int ex;

    [SerializeField]
    private int ey;

    [SerializeField]
    private int nx;

    [SerializeField]
    private int ny;

    [SerializeField]
    private int hx;

    [SerializeField]
    private int hy;

    //correct easy list
    [SerializeField]
    private List<Double> _easyCorrect = new List<Double>();

    //correct easy list
    [SerializeField]
    private List<Double> _easyInCorrect = new List<Double>();

    //correct normal list
    [SerializeField]
    private List<Double> _normalCorrect = new List<Double>();

    //correct normal list
    [SerializeField]
    private List<Double> _normalInCorrect = new List<Double>();

    //correct hard list
    [SerializeField]
    private List<Double> _hardCorrect = new List<Double>();

    //incorrect hard list
    [SerializeField]
    private List<Double> _hardInCorrect = new List<Double>();


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

    public int Ex
    {
        get { return ex; }
        set { ex = value; }
    }

    public int Ey
    {
        get { return ey; }
        set { ey = value; }
    }

    public int Nx
    {
        get { return nx; }
        set { nx = value; }
    }

    public int Ny
    {
        get { return ny; }
        set { ny = value; }
    }

    public int Hx
    {
        get { return hx; }
        set { hx = value; }
    }

    public int Hy
    {
        get { return hy; }
        set { hy = value; }
    }

    //add method
    //correct
    public void addEX(int value)
    {
        _easyCorrect.Add(value);
        _easyInCorrect.Add(0);
    }

    //Incorrect
    public void addEY(int value)
    {
        _easyInCorrect.Add(value);
        _easyCorrect.Add(0);
    }

    public void addNX(int value)
    {
        _normalCorrect.Add(value);
        _normalInCorrect.Add(0);
    }


    public void addNY(int value)
    {
        _normalInCorrect.Add(value);
        _normalCorrect.Add(0);
    }

    public void addHX(int value)
    {
        _hardCorrect.Add(value);
        _hardInCorrect.Add(0);
    }


    public void addHY(int value)
    {
        _hardInCorrect.Add(value);
        _hardCorrect.Add(0);
    }

    public void heuristic()
    {
        if (this.level == 1)
        {
            this._performance.Add((wxe1 * _easyCorrect[_easyCorrect.Count - 1] - wye2 * _easyInCorrect[_easyInCorrect.Count - 1]) + (accum_e* this._accum));
        }
        else if (this.level == 2)
        {
            this._performance.Add((wxn1 * _normalCorrect[_normalCorrect.Count - 1] - wyn2 * _normalInCorrect[_normalInCorrect.Count - 1]) + (accum_n * this._accum));
        }
        else
        {
            this._performance.Add((wxh1 * _hardCorrect[_hardCorrect.Count - 1] - wyh2 * _hardInCorrect[_hardInCorrect.Count - 1]) + (accum_h * this._accum));
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
        this.Ex = 0;
        this.Ey = 0;
        this.Nx = 0;
        this.Ny = 0;
        this.Hx = 0;
        this.Hy = 0;
        this._easyCorrect.Clear();
        this._easyInCorrect.Clear();
        this._normalCorrect.Clear();
        this._normalInCorrect.Clear();
        this._hardCorrect.Clear();
        this._hardInCorrect.Clear();
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
            if (this.level != 1)
            {
                this.level -= 1;
            }
        }
        else if (min() < 0.5 && mean() < 0.5 && max() > 0.5 && std() > 1)
        {
                
            if (this.level != 1)
            {
                this.level -= 1;
            }
        }
        else if (min() < 0.5 && mean() < 0.5 && max() > 0.5 && std() >= 0.5 && std() <= 1)
        {
            if (this.level != 3)
            {
                this.level += 1;
            }
        }
        else if (min() < 0.5 && mean() > 0.5 && max() > 0.5 && std() >= 0.25 && std() <= 1)
        {
            if (this.level != 3)
            {
                this.level += 1;
            }
        }
        else if (min() <= 0 && min() >= -0.5 && mean() > 0.5 && max() > 0.5 && std() >= 0.25 && std() <= 1)
        {
            
            if (this.level != 3)
            {
                this.level += 1;
            }
        }
        else if (min() <= 0 && min() >= -0.5 && mean() > 0.5 && max() > 0.5 && std() > 1)
        {
            
            if (this.level != 3)
            {
                this.level += 1;
            }
        }
        else if (min() > 0 && mean() > 0.5 && max() > 0.5 && std() > 1)
        {
            
            if (this.level != 3)
            {
                this.level += 1;
            }
        }
        else if (min() > 0 && mean() > 0.5 && max() > 0.5 && std() >= 0.25 && std() <= 1)
        {
            
            if (this.level != 3)
            {
                this.level += 1;
            }
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
            if (this.level == 1)
            {
                this.Ex += 1;
                this.addEX(this.Ex);
                this.Accum += 1;
                this.heuristic();
                this.reLevel3();
            } 
            else if (this.level == 2)
            {
                this.Nx += 1;
                this.addNX(this.Nx);
                this.Accum += 1;
                this.heuristic();
                this.reLevel3();
            }
            else if (this.level == 3)
            {
                this.Hx += 1;
                this.addHX(this.Hx);
                this.Accum += 1;
                this.heuristic();
                this.reLevel3();
            }
            
        }
        else
        {
            if (this.level == 1)
            {
                this.Ey += 1;
                this.Accum = 0;
                this.addEY(this.Ey);
                this.heuristic();
                this.reLevel3();
            }
            else if (this.level == 2)
            {
                this.Ny += 1;
                this.Accum = 0;
                this.addNY(this.Ny);
                this.heuristic();
                this.reLevel3();
            }
            else if (this.level == 3)
            {
                this.Hy += 1;
                this.Accum = 0;
                this.addHY(this.Hy);
                this.heuristic();
                this.reLevel3();
            }
            
        }
    }

}
