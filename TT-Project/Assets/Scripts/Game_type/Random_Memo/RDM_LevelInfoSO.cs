using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class RDM_LevelInfoSO : ScriptableObject
{
    [SerializeField]
    private int slotTop;

    [SerializeField]
    private int slotDown;

    [SerializeField]
    private int slotOpened;

    [SerializeField]
    private string img;

    public int SlotTop
    {
        get { return slotTop; }
        set { slotTop = value; }
    }

    public int SlotDown
    {
        get { return slotDown; }
        set { slotDown = value; }
    }

    public int SlotOpened
    {
        get { return slotOpened; }
        set { slotOpened = value; }
    }

    public string Img
    {
        get { return img; }
        set { img = value; }
    }
}
