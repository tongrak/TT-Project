using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController_SM : MonoBehaviour
{

    public SpriteRenderer[] boxs;

    private int colorSelect;

    public float stayLit = 1;
    private float stayLitCounter;

    public float waitBetweenLights;
    private float waitBetweenCounter;

    private bool shouldBeLit;
    private bool shouldBeDark;

    public List<int> activeSequence;
    private int positionInSequence;

    private bool gameActive;
    private int inputInSequence;

    public void StartGame()
    {
        activeSequence.Clear();

        positionInSequence = 0;
        inputInSequence = 0;

        colorSelect = Random.Range(0, boxs.Length);

        activeSequence.Add(colorSelect);

        boxs[activeSequence[positionInSequence]].color = new Color(boxs[activeSequence[positionInSequence]].color.r, boxs[activeSequence[positionInSequence]].color.g, boxs[activeSequence[positionInSequence]].color.b, 1f);

        stayLitCounter = stayLit;
        shouldBeLit = true;
         

    }

    private void Update()
    {
        if(shouldBeLit)
        {
            stayLitCounter -= Time.deltaTime;
        
            if(stayLitCounter < 0 )
            {
                boxs[activeSequence[positionInSequence]].color = new Color(boxs[activeSequence[positionInSequence]].color.r, boxs[activeSequence[positionInSequence]].color.g, boxs[activeSequence[positionInSequence]].color.b, 0.5f);
                shouldBeLit = false;

                shouldBeDark = true;
                waitBetweenCounter = waitBetweenLights;

                positionInSequence++;
            }
            
        }

        if(shouldBeDark)
        {
            waitBetweenCounter -= Time.deltaTime;

            if(positionInSequence >= activeSequence.Count)
            {
                shouldBeDark = false;
                gameActive = true;
            }
            else
            {
                if(waitBetweenCounter < 0)
                {           

                    boxs[activeSequence[positionInSequence]].color = new Color(boxs[activeSequence[positionInSequence]].color.r, boxs[activeSequence[positionInSequence]].color.g, boxs[activeSequence[positionInSequence]].color.b, 1f);

                    stayLitCounter = stayLit;
                    shouldBeLit = true;
                    shouldBeDark = false;
                }
            }
        }
    }

    


    public void ColorPressed(int whichButton)
    {
        if(gameActive)
        {
            if (activeSequence[inputInSequence] == whichButton)
            {
                Debug.Log("Correct");

                inputInSequence++;

                if(inputInSequence >= activeSequence.Count)
                {
                    positionInSequence = 0;
                    inputInSequence = 0; 

                    colorSelect = Random.Range(0, boxs.Length);

                    activeSequence.Add(colorSelect);

                    boxs[activeSequence[positionInSequence]].color = new Color(boxs[activeSequence[positionInSequence]].color.r, boxs[activeSequence[positionInSequence]].color.g, boxs[activeSequence[positionInSequence]].color.b, 1f);

                    stayLitCounter = stayLit;
                    shouldBeLit = true;

                    gameActive = false;
                }
            }
            else
            {
                Debug.Log("Wrong");
                gameActive = false;
            }
        }

        
    }


}
