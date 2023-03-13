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

    [SerializeField]
    private DDA DDA;

    public void reset()
    {
        scroeSO.Value = 0;
        TimeSO.Value = 60f;
        random.Value = false;
        DDA.Reset();
    }

}
