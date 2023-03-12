using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class howtoplay3 : MonoBehaviour
{
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

    public void Prev()
    {
        Setdown();
        prevhowto.SetActive(true);
    }
}
