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

        // �Ӣ������ jsonData ���ŧ�� class �ͧ C# �·������� class ��鹵�ͧ�ժ��ͷ��ç�Ѻ supabase Ẻ��� �
        playerData = JsonUtility.FromJson<PlayerList>(dbConnector.jsonData);

        // if username isn't in database
        if (playerData.players.Length == 0)
        {
            print("Create new player data");
            // �� username ������������ supabase
            yield return dbConnector.createNewPlayer(username);

            // �Ӣ����Ţͧ username ����� supabase �������� unity
            yield return dbConnector.GetPlayerData(username);
            playerData = JsonUtility.FromJson<PlayerList>(dbConnector.jsonData);


        }
        Debug.Log(dbConnector.jsonData);

        // change scene to scoreboard
        changeScene();
    }
}
