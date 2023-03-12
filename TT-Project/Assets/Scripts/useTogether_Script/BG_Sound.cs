using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Sound : MonoBehaviour
{
    //Play Global
    private static BG_Sound instance = null;

    public static BG_Sound Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
    //Play Gobal End

}
