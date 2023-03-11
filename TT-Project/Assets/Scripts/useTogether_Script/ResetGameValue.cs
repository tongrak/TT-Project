using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGameValue : MonoBehaviour
{

    [SerializeField]
    private FloatSO scroeSO;

    [SerializeField]
    private FloatSO TimeSO;

    [SerializeField]
    private boolFortime random;

    public void reset()
    {
        scroeSO.Value = 0;
        TimeSO.Value = 180f;
        random.Value = false;
    }

}
