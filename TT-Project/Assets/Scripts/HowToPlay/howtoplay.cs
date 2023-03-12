using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class howtoplay : MonoBehaviour
{

    [SerializeField]
    public GameObject nexthowto;
    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void Setdown()
    {
        gameObject.SetActive(false);
    }

    public void Next()
    {
        Setdown();
        nexthowto.SetActive(true);
    }
}
