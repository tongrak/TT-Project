using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class howtoplay3 : MonoBehaviour
{
    [SerializeField]
    public GameObject prevhowto;

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

    public void Prev()
    {
        Setdown();
        prevhowto.SetActive(true);
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
