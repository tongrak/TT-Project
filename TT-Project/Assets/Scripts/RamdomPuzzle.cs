using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RamdomPuzzle : MonoBehaviour
{
    GridLayoutGroup glg;
    public int allCards;

    void Awake()
    {
        
        glg = gameObject.GetComponent<GridLayoutGroup>();
        Debug.Log(glg);
        Debug.Log(glg.cellSize);
        Debug.Log(glg.childAlignment);
        
        glg.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        allCards = Random.Range(4, 21);


        while (allCards % 2 != 0)
        {
            allCards = Random.Range(4, 21);
            
        }
        
        if (allCards == 18)
        {
            glg.constraintCount = allCards / 3;
            
        }else if(allCards == 20)
        {
            glg.constraintCount = allCards / 4;
        }
        else
        {
            glg.constraintCount = allCards / 2;
            //glg.childAlignment = TextAnchor.MiddleCenter;
        }
        
        Debug.Log("Cards have " + allCards);
        Debug.Log("Colum is " + glg.constraintCount);


        //set position
        if (allCards > 10 && allCards < 18)
        {
            glg.cellSize = new Vector2(200, 300);
            glg.spacing = new Vector2(10,10);
            Debug.Log(glg.cellSize);
            Debug.Log(glg.spacing);
        }
        else if(allCards >= 18 && allCards < 20)
        {
            glg.cellSize = new Vector2(150, 220);
            glg.spacing = new Vector2(7, 7);
            Debug.Log(glg.cellSize);
            Debug.Log(glg.spacing);
        }else if (allCards == 20)
        {
            glg.cellSize = new Vector2(150, 225);
            glg.spacing = new Vector2(10, 5);
            Debug.Log(glg.cellSize);
            Debug.Log(glg.spacing);
        }
    }


}
