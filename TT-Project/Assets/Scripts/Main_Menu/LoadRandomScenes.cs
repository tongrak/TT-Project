using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadRandomScenes : MonoBehaviour
{
    public void LoadRandomScene()
    {
        string[] cars = { "memory game", "ReverseRetention_1", "memory game", "memory game" };
        int index = Random.Range(0,4);
        SceneManager.LoadScene(cars[index]);
        Debug.Log("Scene " + cars[index] + " Loaded");
    }
}
