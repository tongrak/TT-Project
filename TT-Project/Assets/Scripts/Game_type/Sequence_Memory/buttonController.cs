using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonController : MonoBehaviour
{

    private SpriteRenderer theSprite;

    public int thisButtonNumber;

    private GameController_SM theGM;

    // Start is called before the first frame update
    void Start()
    {
        theSprite = GetComponent<SpriteRenderer>();    
        theGM = FindObjectOfType<GameController_SM>();
        thisButtonNumber = Convert.ToInt32(this.name);
    }


    void OnMouseDown()
    {
        theSprite.color = new Color(theSprite.color.r, theSprite.color.g, theSprite.color.b,1f);
    }

    void OnMouseUp()
    {
        theSprite.color = new Color(theSprite.color.r, theSprite.color.g, theSprite.color.b, 0.5f);
        theGM.ColorPressed(thisButtonNumber);
    }

}
