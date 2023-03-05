using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Text;

public class Login : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField passwordField;
    [SerializeField] StringSO UsernameSO;
    [SerializeField] IntSO Seq_bestScoreSO;
    [SerializeField] IntSO MemRand_bestScoreSO;
    [SerializeField] IntSO Rev_bestScoreSO;
    [SerializeField] IntSO Mix_bestScoreSO;
    private SupabaseManager dbConnector;
    private Player_DataList playerData;
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
        StartCoroutine(Login_Coroutine(username, password));
    }

    public void TestButton()
    {
        StartCoroutine(Test_coroutine());
    }

    // use to change current scene to scoreboard scene
    public void changeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    IEnumerator Test_coroutine()
    {
        string username = "TestP2";
        yield return StartCoroutine(dbConnector.API_GET_Coroutine("Player_account?select=username,MemoRandom_Score!inner(best_score),SequenceMem_Score!inner(best_score),Reverse_Score!inner(best_score),Mix_Score!inner(best_score)&username=eq." + username + "&password=eq.NDU2Nwo="));
        print(dbConnector.jsonData);
    }

    IEnumerator Login_Coroutine(string username, string password)
    {
        // encoded password
        byte[] bytesToEncode = Encoding.UTF8.GetBytes(password);
        string encodedPassword = Convert.ToBase64String(bytesToEncode);
        // check if username and password is correct
        yield return dbConnector.API_GET_Coroutine("Player_account?select=username,MemoRandom_Score!inner(best_score),SequenceMem_Score!inner(best_score),Reverse_Score!inner(best_score),Mix_Score!inner(best_score)&username=eq." + username +"&password=eq." + encodedPassword);

        // นำข้อมูลใน jsonData มาแปลงเป็น class ของ C# โดยที่ตัวแปรใน class นั้นต้องมีชื่อที่ตรงกับ supabase แบบเป๊ะ ๆ
        playerData = JsonUtility.FromJson<Player_DataList>(dbConnector.jsonData);

        // if username isn't in database, then register
        if (playerData.jsonData.Length == 0)
        {
            print("Create new player data");
            // นำ username ที่ได้มาไปใส่ใน supabase
            Dictionary<string, string> newUser_data = new Dictionary<string, string>();
            newUser_data.Add("Player_name", username);
            yield return dbConnector.API_POST_Coroutine(newUser_data, "Player_Score");

            // นำข้อมูลของ username มาเก็บไว้ใน unity
            UsernameSO.Value = username;
            Seq_bestScoreSO.Value = 0;
            Rev_bestScoreSO.Value = 0;
            MemRand_bestScoreSO.Value = 0;
            Mix_bestScoreSO.Value = 0;
        }

        // if username is in database, then login
        else
        {
            Debug.Log("Login success!!!!!");
            // นำข้อมูลของ username มาเก็บไว้ใน unity
            Player_Data currPlayer = playerData.jsonData[0];
            UsernameSO.Value = currPlayer.username;
            Seq_bestScoreSO.Value = currPlayer.SequenceMem_Score.best_score;
            Rev_bestScoreSO.Value = currPlayer.Reverse_Score.best_score;
            MemRand_bestScoreSO.Value = currPlayer.MemoRandom_Score.best_score;
            Mix_bestScoreSO.Value = currPlayer.Mix_Score.best_score;
            ChangeSceneManager.changeScene("MainMenu");
        }


    }
}
