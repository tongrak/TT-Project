using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Text;

public class Register : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField passwordField;
    [SerializeField] private TMP_InputField confirmPasswordField;
    [SerializeField] StringSO UsernameSO;
    [SerializeField] IntSO Seq_bestScoreSO;
    [SerializeField] IntSO MemRand_bestScoreSO;
    [SerializeField] IntSO Rev_bestScoreSO;
    [SerializeField] IntSO Mix_bestScoreSO;
    private SupabaseManager dbConnector;

    // Start is called before the first frame update
    void Start()
    {
        dbConnector = SupabaseManager.getInstance();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RegisterButton()
    {
        string username = usernameField.text;
        string password = passwordField.text;
        string confirmPass = confirmPasswordField.text;

        if (password == null || confirmPass == null || username == null)
        {
            Debug.Log("Please fill in all text field");
        }
        else if(password != confirmPass)
        {
            Debug.Log("Password and ConfimPassword is not same");
        }
        else
        {
            StartCoroutine(Register_Coroutine(username, password));
        }
    }

    IEnumerator Register_Coroutine(string username, string password)
    {
        // encoded password
        byte[] bytesToEncode = Encoding.UTF8.GetBytes(password);
        string encodedPassword = Convert.ToBase64String(bytesToEncode);
        
        // create data for insert to database.
        Dictionary<string, string> newUser_data = new Dictionary<string, string>();
        newUser_data.Add("username", username);
        newUser_data.Add("password", encodedPassword);
        
        // try to insert new username&password to database.
        yield return dbConnector.API_POST_Coroutine(newUser_data, "Player_account");
        // if new username have already exist in database.
        if (dbConnector.errData != null)
        {
            Debug.Log("err: " + dbConnector.errData);
            Debug.Log("Can't register");
        }
        // if new username have never exist in database.
        else
        {
            //insert new player to "Sequence score" table
            Dictionary<string, string> newUser_seq_score = new Dictionary<string, string>();
            newUser_seq_score.Add("username", username);
            yield return dbConnector.API_POST_Coroutine(newUser_seq_score, "SequenceMem_Score");

            //insert new player to "Reverse score" table
            Dictionary<string, string> newUser_rev_score = new Dictionary<string, string>();
            newUser_rev_score.Add("username", username);
            yield return dbConnector.API_POST_Coroutine(newUser_rev_score, "Reverse_Score");

            //insert new player to "MemoRandom score" table
            Dictionary<string, string> newUser_memRand_score = new Dictionary<string, string>();
            newUser_memRand_score.Add("username", username);
            yield return dbConnector.API_POST_Coroutine(newUser_memRand_score, "MemoRandom_Score");

            //insert new player to "Mix score" table
            Dictionary<string, string> newUser_mix_score = new Dictionary<string, string>();
            newUser_mix_score.Add("username", username);
            yield return dbConnector.API_POST_Coroutine(newUser_mix_score, "Mix_Score");

            newUser_data = null;
            newUser_seq_score = null;
            newUser_rev_score = null;
            newUser_memRand_score = null;
            newUser_mix_score = null;
            Debug.Log("register success!!!!!");

            // change scene
            ChangeSceneManager.changeScene("MainMenu");
        }
    }

    public void TestButton()
    {
        string username = "TestAPI";
        string password = "1234";
        StartCoroutine(Test_Coroutine(username, password));
    }

    IEnumerator Test_Coroutine(string username, string password)
    {
        Dictionary<string, string> newUser_data = new Dictionary<string, string>();
        newUser_data.Add("username", username);
        newUser_data.Add("password", password);
        yield return dbConnector.API_POST_Coroutine(newUser_data, "Player_account");
        if(dbConnector.errData != null)
        {
            Debug.Log("err: " + dbConnector.errData);
            Debug.Log("Can't register");
        }
        else
        {
            //insert new player to "Sequence score" table
            Dictionary<string, string> newUser_seq_score = new Dictionary<string, string>();
            newUser_seq_score.Add("username", username);
            yield return dbConnector.API_POST_Coroutine(newUser_seq_score, "SequenceMem_Score");
            //insert new player to "Reverse score" table
            Dictionary<string, string> newUser_rev_score = new Dictionary<string, string>();
            newUser_rev_score.Add("username", username);
            yield return dbConnector.API_POST_Coroutine(newUser_rev_score, "Reverse_Score");
            //insert new player to "MemoRandom score" table
            Dictionary<string, string> newUser_memRand_score = new Dictionary<string, string>();
            newUser_memRand_score.Add("username", username);
            yield return dbConnector.API_POST_Coroutine(newUser_memRand_score, "MemoRandom_Score");
            //insert new player to "Mix score" table
            Dictionary<string, string> newUser_mix_score = new Dictionary<string, string>();
            newUser_mix_score.Add("username", username);
            yield return dbConnector.API_POST_Coroutine(newUser_mix_score, "Mix_Score");

            newUser_data = null;
            newUser_seq_score = null;
            newUser_rev_score = null;
            newUser_memRand_score = null;
            newUser_mix_score = null;
            Debug.Log("register success!!!!!");
        }
    }
}
