using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class boolFortime : ScriptableObject
{
    [SerializeField]
    private bool _value = false;

    public bool Value
    {
        get { return _value; }
        set { _value = value; }
    }
}
