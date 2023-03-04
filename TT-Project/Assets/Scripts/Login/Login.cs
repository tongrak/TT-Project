using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField usernameField;
    [SerializeField]
    private TMP_InputField passwordField;
    [SerializeField]
    private IntSO bestScoreSO;
    [SerializeField]
    private StringSO UsernameSO;
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
        string password = passwordField.text;
        StartCoroutine(GetPlayer_Coroutine(username, password));
    }

    // use to change current scene to scoreboard scene
    public void changeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    IEnumerator GetPlayer_Coroutine(string username, string password)
    {
        yield return dbConnector.API_GET_Coroutine("Player_Score?Player_name=eq."+username, "players");

        // นำข้อมูลใน jsonData มาแปลงเป็น class ของ C# โดยที่ตัวแปรใน class นั้นต้องมีชื่อที่ตรงกับ supabase แบบเป๊ะ ๆ
        playerData = JsonUtility.FromJson<PlayerList>(dbConnector.jsonData);

        // if username isn't in database
        if (playerData.players.Length == 0)
        {
            print("Create new player data");
            // นำ username ที่ได้มาไปใส่ใน supabase
            Dictionary<string, string> newUser_data = new Dictionary<string, string>();
            newUser_data.Add("Player_name", username);
            yield return dbConnector.API_POST_Coroutine(newUser_data, "Player_Score", "players");

            // นำข้อมูลของ username มาเก็บไว้ใน unity
            PlayerData.username = username;
            PlayerData.bestScore = 0;
            PlayerData.currentScore = 0;
            UsernameSO.Value = username;
            bestScoreSO.Value = 0;
        }

        // if username is in database
        else
        {
            // นำข้อมูลของ username มาเก็บไว้ใน unity
            Player_Score currPlayer = playerData.players[0];
            PlayerData.username = currPlayer.Player_name;
            PlayerData.bestScore = currPlayer.Best_score;
            PlayerData.currentScore = currPlayer.Current_score;
            UsernameSO.Value = username;
            bestScoreSO.Value = currPlayer.Best_score;
        }

        Debug.Log("Player name: " + PlayerData.username + "\n" + "Best score: " + PlayerData.bestScore.ToString() + "\n" + "Current score: " + PlayerData.currentScore.ToString());

        // change scene to scoreboard
        //changeScene("Scoreboard");
        ChangeSceneManager.changeScene("MainMenu");
    }
}
