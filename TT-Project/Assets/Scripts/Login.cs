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

    public void loginButton()
    {
        string username = usernameField.text;
        StartCoroutine(GetPlayer_Coroutine(username));
    }

    private void changeScene()
    {
        SceneManager.LoadScene("Scoreboard");
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

            // นำข้อมูลของ username ใหม่ใน supabase มาเก็บไว้ใน unity
            yield return dbConnector.GetPlayerData(username);
            playerData = JsonUtility.FromJson<PlayerList>(dbConnector.jsonData);


        }
        Debug.Log(dbConnector.jsonData);

        // change scene to scoreboard
        changeScene();
    }
}
