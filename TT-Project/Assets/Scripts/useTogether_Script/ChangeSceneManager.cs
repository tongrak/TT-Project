using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // use to change current scene to scoreboard scene
    public static void changeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
