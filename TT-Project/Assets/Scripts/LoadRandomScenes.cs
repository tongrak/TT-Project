using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadRandomScenes : MonoBehaviour
{
    public void LoadRandomScene()
    {
        int index = Random.Range(2,3);
        SceneManager.LoadScene(index);
        Debug.Log("Scene Loaded");
    }
}
