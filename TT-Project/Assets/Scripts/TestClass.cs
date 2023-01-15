using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestClass : MonoBehaviour
{
    public TMP_Text test_text;
    private StudentList stList;
    private Database databaseConnector;
    // Start is called before the first frame update

    void Start()
    {
        databaseConnector = new Database();
        test_text.text = "Loading....";
        StartCoroutine(GetTopTenStudent());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator GetTopTenStudent()
    {
        yield return databaseConnector.GetTopTenStudentData();
        stList = databaseConnector.studentList;
        test_text.text = stList.students[0].id.ToString();
        yield break;
    }
}
