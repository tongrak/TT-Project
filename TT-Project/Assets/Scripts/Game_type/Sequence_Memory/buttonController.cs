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
        theSprite.color = new Color((float)0.4509804, 1, (float)0.8666667, 1);
    }

    void OnMouseUp()
    {
        theSprite.color = new Color((float)0.1176471, (float)0.1176471, (float)0.1176471, 1);
        theGM.ColorPressed(thisButtonNumber);
    }

}
