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
        return temp[0];
    }

    public Double max()
    {
        List<Double> temp = this._performance;
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

        return result;
    }

    public Double std()
    {
        Double sd;
        Double sum = 0;

        for (int i = 0; i < _performance.Count; i++)
        {
            sum += math.pow((_performance[i] - mean()), 2);
        }

        sd = math.sqrt(sum / _performance.Count - 1);

        return sd;
    }

    public void reLevel()
    {
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
        else if (this.level == 2)
        {
            if (min() < 0.5 && min() > 0)
            {
                if (mean() < 0.5)
                {
                    if (max() < 0)
                    {
                        if (this.level != 1)
                        {
                            this.level -= 1;
                        }

                    }
                    else if (max() > 0.5)
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
        else
        {
            if (min() < 0.5 && min() > 0)
            {
                if (mean() < 0.5)
                {
                    if (max() < 0)
                    {
                        if (this.level != 1)
                        {
                            this.level -= 1;
                        }

                    }
                    else if (max() > 0.5)
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


    public void loadScenes()
    {
        SceneManager.LoadScene("sequence_memory_game");
    }
}
