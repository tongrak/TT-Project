using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    IEnumerator GetPlayer_Coroutine(string username)
    {
        yield return dbConnector.GetPlayerData(username);
        Debug.Log(dbConnector.jsonData);

        // นำข้อมูลใน jsonData มาแปลงเป็น class ของ C# โดยที่ตัวแปรใน class นั้นต้องมีชื่อที่ตรงกับ database แบบเป๊ะ ๆ
        playerData = JsonUtility.FromJson<PlayerList>(dbConnector.jsonData);

        // if username isn't in database
        if (playerData.players.Length == 0)
        {
            print("Not login yet");
            // นำ username ที่ได้มาไปใส่ใน database
            yield return dbConnector.createNewPlayer(username);
            Debug.Log(dbConnector.jsonData);

            // นำข้อมูลของ username ใหม่ใน database มาเก็บไว้ใน unity
        }
        // if username is in database
        else
        {
            print("Welcome back");
        }
    }
}
