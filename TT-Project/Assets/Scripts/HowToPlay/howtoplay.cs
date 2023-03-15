using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class howtoplay : MonoBehaviour
{

    [SerializeField]
    public GameObject nexthowto;

    [SerializeField]
    public GameObject playClip;

    [SerializeField]
    public GameObject oldPage;

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

    public void PlayClip()
    {
        playClip.SetActive(true);
        oldPage.SetActive(false);
    }

    public void CloseClip()
    {
        playClip.SetActive(false);
        oldPage.SetActive(true);
    }
}
