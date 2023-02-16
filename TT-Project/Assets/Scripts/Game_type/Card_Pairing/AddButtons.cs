using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AddButtons : MonoBehaviour
{
    [SerializeField]
    private Transform puzzleField;

    [SerializeField] 
    private GridLayoutGroup grid;

    [SerializeField]
    private GameObject btn;

    private int m;
    private int allCards;

    void Start()
    {
        allCards = FindObjectOfType<RamdomPuzzle>().allCards;
        Debug.Log("All Cards for addButton have " + allCards.ToString());
        
        for(int i = 0;i < allCards; i++)
        {
            GameObject button = Instantiate(btn);
            button.name = "" + i;
            button.transform.SetParent(puzzleField,false);
        }
        
    }
}//AddButtons
