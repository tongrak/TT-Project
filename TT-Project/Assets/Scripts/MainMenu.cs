using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void HowToPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToHome()
    {
        SceneManager.LoadScene(0);
    }

    public void Leaderboard()
    {
        SceneManager.LoadScene(2);
    }

}
