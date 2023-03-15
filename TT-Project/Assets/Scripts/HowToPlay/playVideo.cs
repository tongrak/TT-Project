using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playVideo : MonoBehaviour
{
    [SerializeField]
    private string link;
    public void OpenURL()
    {
        if(link == "reverse") Application.OpenURL("https://youtu.be/AxYJkFmB-UA");
        else if(link == "sequence") Application.OpenURL("https://youtu.be/03D4ksjj20c");
        else Application.OpenURL("https://youtu.be/5MyQFkwhFzQ");
        Debug.Log("is t$$anonymous$$s working?");
    }
}
