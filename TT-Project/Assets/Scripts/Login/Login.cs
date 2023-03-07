using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Text;

public class Login : MonoBehaviour
{
    [Header("Game objects")]
    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField passwordField;
    [SerializeField] private GameObject popup;
    [Header("SO file")]
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
        popup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // pop the popup window when error accur
    private void popWarning(string message)
    {
        // write warning message in popup
        TextMeshProUGUI warningText =  popup.transform.Find("warningText").GetComponent<TextMeshProUGUI>();
        warningText.text = "" + message;
        // pop warning
        popup.SetActive(true);
    }

    // use to change current scene to scoreboard scene
    public void changeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    // for login button
    public void loginButton()
    {
        string username = usernameField.text;
        string password = passwordField.text;
        if(username == "" || password == "")
        {
            Debug.Log("Please fill in all text field");
            popWarning("Please fill in all text field");
        }
        else
        {
            StartCoroutine(Login_Coroutine(username, password));
        }
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
            Debug.Log("username or passsword is not correct");
            popWarning("username or passsword is not correct");
        }

        // if username is in database, then login
        else
        {
            // นำข้อมูลของ username มาเก็บไว้ใน unity
            Player_Data currPlayer = playerData.jsonData[0];
            UsernameSO.Value = currPlayer.username;
            Seq_bestScoreSO.Value = currPlayer.SequenceMem_Score.best_score;
            Rev_bestScoreSO.Value = currPlayer.Reverse_Score.best_score;
            MemRand_bestScoreSO.Value = currPlayer.MemoRandom_Score.best_score;
            Mix_bestScoreSO.Value = currPlayer.Mix_Score.best_score;

            Debug.Log("Login success!!!!!");
            //change scene
            ChangeSceneManager.changeScene("MainMenu");
        }
    }
}
