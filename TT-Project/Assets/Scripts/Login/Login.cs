using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public TMP_InputField usernameField;
    private SupabaseManager dbConnector;
    private PlayerList playerData;
    // Start is called before the first frame update
    void Start()
    {
        dbConnector = SupabaseManager.getInstance();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // for login button
    public void loginButton()
    {
        string username = usernameField.text;
        StartCoroutine(GetPlayer_Coroutine(username));
    }

    // use to change current scene to scoreboard scene
    private void changeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    IEnumerator GetPlayer_Coroutine(string username)
    {
        yield return dbConnector.GetPlayerData(username);

        // นำข้อมูลใน jsonData มาแปลงเป็น class ของ C# โดยที่ตัวแปรใน class นั้นต้องมีชื่อที่ตรงกับ supabase แบบเป๊ะ ๆ
        playerData = JsonUtility.FromJson<PlayerList>(dbConnector.jsonData);

        // if username isn't in database
        if (playerData.players.Length == 0)
        {
            print("Create new player data");
            // นำ username ที่ได้มาไปใส่ใน supabase
            yield return dbConnector.createNewPlayer(username);

            // นำข้อมูลของ username มาเก็บไว้ใน unity
            PlayerData.username = username;
            PlayerData.bestScore = 0;
            PlayerData.currentScore = 0;
        }

        // if username isn in database
        else
        {
            // นำข้อมูลของ username มาเก็บไว้ใน unity
            Player_Score currPlayer = playerData.players[0];
            PlayerData.username = currPlayer.Player_name;
            PlayerData.bestScore = currPlayer.Best_score;
            PlayerData.currentScore = currPlayer.Current_score;
        }

        Debug.Log("Player name: " + PlayerData.username + "\n" + "Best score: " + PlayerData.bestScore.ToString() + "\n" + "Current score: " + PlayerData.currentScore.ToString());

        // change scene to scoreboard
        //changeScene("Scoreboard");
        ChangeSceneManager.changeScene("MainMenu");
    }
}
