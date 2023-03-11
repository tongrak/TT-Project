using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text;

public class Register : MonoBehaviour
{
    [Header("Game objects")]
    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField passwordField;
    [SerializeField] private TMP_InputField confirmPasswordField;
    [SerializeField] private GameObject popup;
    [Header("Disable objects")]
    [SerializeField] private Transform buttons;
    [SerializeField] private Transform inputTextField;
    [Header("SO file")] 
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
        popup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            usernameField.text = usernameField.text.Replace(" ", "");
            passwordField.text = passwordField.text.Replace(" ", "");
            confirmPasswordField.text = confirmPasswordField.text.Replace(" ", "");
        }
    }

    // disable สิ่งต่าง ๆ เมื่อทำการ register
    public void DisableObjects()
    {
        foreach (Transform child in buttons)
        {
            child.GetComponent<Button>().interactable = false;
        }

        foreach (Transform child in inputTextField)
        {
            child.GetComponent<TMP_InputField>().interactable = false;
        }
    }

    // enable ปุ่มต่าง ๆ หลังจากกด close popup
    public void EnableObjects()
    {
        foreach (Transform child in buttons)
        {
            child.GetComponent<Button>().interactable = true;
        }

        foreach (Transform child in inputTextField)
        {
            child.GetComponent<TMP_InputField>().interactable = true;
        }
    }

    // pop the popup window when error accur
    private void popWarning(string message)
    {
        // write warning message in popup
        TextMeshProUGUI warningText = popup.transform.Find("warningText").GetComponent<TextMeshProUGUI>();
        warningText.text = "" + message;
        // pop warning
        popup.SetActive(true);
    }

    public void RegisterButton()
    {
        string username = usernameField.text;
        string password = passwordField.text;
        string confirmPass = confirmPasswordField.text;

        if (password == "" || confirmPass == "" || username == "")
        {
            Debug.Log("Please fill in all text field");
            popWarning("Please fill in all text field");
        }
        else if(password != confirmPass)
        {
            Debug.Log("Password and ConfimPassword is not same");
            popWarning("Password and ConfimPassword is not same");
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
            Debug.Log("This username have already exist");
            popWarning("This username have already exist");
        }
        // if new username have never exist in database.
        else
        {
            newUser_data.Remove("password");
            //insert new player to "Sequence score" table
            yield return dbConnector.API_POST_Coroutine(newUser_data, "SequenceMem_Score");

            //insert new player to "Reverse score" table
            yield return dbConnector.API_POST_Coroutine(newUser_data, "Reverse_Score");

            //insert new player to "MemoRandom score" table
            yield return dbConnector.API_POST_Coroutine(newUser_data, "MemoRandom_Score");

            //insert new player to "Mix score" table
            yield return dbConnector.API_POST_Coroutine(newUser_data, "Mix_Score");

            newUser_data = null;

            // save data to unity
            UsernameSO.Value = username;
            Seq_bestScoreSO.Value = 0;
            MemRand_bestScoreSO.Value = 0;
            Rev_bestScoreSO.Value = 0;
            Mix_bestScoreSO.Value = 0;

            Debug.Log("register success!!!!!");

            // change scene
            ChangeSceneManager.changeScene("MainMenu");
        }
    }

    //public void TestButton()
    //{
    //    string username = "TestAPI";
    //    string password = "1234";
    //    StartCoroutine(Test_Coroutine(username, password));
    //}

    //IEnumerator Test_Coroutine(string username, string password)
    //{
    //    Dictionary<string, string> newUser_data = new Dictionary<string, string>();
    //    newUser_data.Add("username", username);
    //    newUser_data.Add("password", password);
    //    yield return dbConnector.API_POST_Coroutine(newUser_data, "Player_account");
    //    if(dbConnector.errData != null)
    //    {
    //        Debug.Log("err: " + dbConnector.errData);
    //        Debug.Log("This username have already exist");
    //    }
    //    else
    //    {
    //        //insert new player to "Sequence score" table
    //        Dictionary<string, string> newUser_seq_score = new Dictionary<string, string>();
    //        newUser_seq_score.Add("username", username);
    //        yield return dbConnector.API_POST_Coroutine(newUser_seq_score, "SequenceMem_Score");
    //        //insert new player to "Reverse score" table
    //        Dictionary<string, string> newUser_rev_score = new Dictionary<string, string>();
    //        newUser_rev_score.Add("username", username);
    //        yield return dbConnector.API_POST_Coroutine(newUser_rev_score, "Reverse_Score");
    //        //insert new player to "MemoRandom score" table
    //        Dictionary<string, string> newUser_memRand_score = new Dictionary<string, string>();
    //        newUser_memRand_score.Add("username", username);
    //        yield return dbConnector.API_POST_Coroutine(newUser_memRand_score, "MemoRandom_Score");
    //        //insert new player to "Mix score" table
    //        Dictionary<string, string> newUser_mix_score = new Dictionary<string, string>();
    //        newUser_mix_score.Add("username", username);
    //        yield return dbConnector.API_POST_Coroutine(newUser_mix_score, "Mix_Score");

    //        newUser_data = null;
    //        newUser_seq_score = null;
    //        newUser_rev_score = null;
    //        newUser_memRand_score = null;
    //        newUser_mix_score = null;
    //        Debug.Log("register success!!!!!");
    //    }
    //}
}
