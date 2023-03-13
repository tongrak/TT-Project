using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class howtoplay2 : MonoBehaviour
{
    [SerializeField]
    public GameObject nexthowto;

    [SerializeField]
    public GameObject prevhowto;
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

    public void Prev()
    {
        Setdown();
        prevhowto.SetActive(true);
    }
}
