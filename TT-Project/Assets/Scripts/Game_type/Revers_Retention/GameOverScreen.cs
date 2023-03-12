using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText;

    private string[] sceneList = { "MemoRandom", "ReverseRetention_1" };
    private int randomIndex;

    public void Setup()
    {
        //gameObject.SetActive(true);
        //pointsText.text = score.ToString() + " POINTS"; //show point 
        NextPuzzle();
    }

    public void NextPuzzle() {
        randomIndex = Random.Range(0, sceneList.Length);
        SceneManager.LoadScene(sceneList[randomIndex]);
    }

    public void MainMenu() {
        SceneManager.LoadScene("ReverseRetention_1");
    
    }
}
