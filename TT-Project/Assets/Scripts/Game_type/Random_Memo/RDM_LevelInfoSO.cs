using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class RDM_LevelInfoSO : ScriptableObject
{
    [SerializeField]
    private int slot;

    [SerializeField]
    private string img;

    public int Slot
    {
        get { return slot; }
        set { slot = value; }
    }

    public string Img
    {
        get { return img; }
        set { img = value; }
    }
}
