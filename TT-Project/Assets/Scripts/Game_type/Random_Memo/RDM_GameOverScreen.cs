using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RDM_GameOverScreen : MonoBehaviour
{
    public Text pointsText;

    private string[] sceneList = { "ReverseRetention_1", "MemoRandom", "sequence_memory_game" };
    private int randomIndex;

    public void Setup(bool isMix)
    {
        //gameObject.SetActive(true);
        //pointsText.text = score.ToString() + " POINTS"; //show point 
        if (isMix)
        {
            NextPuzzle();
        }
        else
        {
            RePuzzle();
        }
    }

    public void NextPuzzle() {
        randomIndex = Random.Range(0, sceneList.Length);
        SceneManager.LoadScene(sceneList[randomIndex]);
    }

    public void RePuzzle()
    {
        SceneManager.LoadScene("MemoRandom");
    }

    public void MainMenu() {
        SceneManager.LoadScene("MemoRandom");
    
    }
}
