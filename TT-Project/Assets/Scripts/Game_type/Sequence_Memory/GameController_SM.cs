using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController_SM : MonoBehaviour
{
    //Score
    [SerializeField]
    private FloatSO scoreSO;

    [SerializeField]
    private TMP_Text scoreText;

    //Time
    [SerializeField]
    private FloatSO TimeSO;

    //DDA
    [SerializeField]
    private DDA DDA;

    //game managment
    //public SpriteRenderer[] boxs;

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


    //new 

    public List<SpriteRenderer> btns = new List<SpriteRenderer>();

    private void Start()
    {

        TimeSO.Value = 180;

        //Main Score++
        scoreText.text = scoreSO.Value + "";


        GetButtons();
        activeSequence.Clear();

        positionInSequence = 0;
        inputInSequence = 0;

        for (int i = 0; i < btns.Count; i++)
        {
            colorSelect = Random.Range(0, btns.Count);
            activeSequence.Add(colorSelect);
            Debug.Log(activeSequence.Count);
            Debug.Log("btns = " + btns.Count);
        }

        btns[activeSequence[positionInSequence]].color = new Color(btns[activeSequence[positionInSequence]].color.r, btns[activeSequence[positionInSequence]].color.g, btns[activeSequence[positionInSequence]].color.b, 1f);

        stayLitCounter = stayLit;
        shouldBeLit = true;
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("btnSeq");

        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<SpriteRenderer>());
            Debug.Log(objects[i].name);
        }

        Debug.Log("len = " + objects.Length);
    }


    public void StartGame()
    {

        //Main Score++
        scoreSO.Value = 0;
        scoreText.text = scoreSO.Value + "";


        GetButtons();
        activeSequence.Clear();
        
        positionInSequence = 0;
        inputInSequence = 0;

        for(int i = 0;i < btns.Count;i++)
        {
            colorSelect = Random.Range(0, btns.Count);
            activeSequence.Add(colorSelect);
            Debug.Log(activeSequence.Count);
            Debug.Log("btns = " + btns.Count);
        }
        
        btns[activeSequence[positionInSequence]].color = new Color(btns[activeSequence[positionInSequence]].color.r, btns[activeSequence[positionInSequence]].color.g, btns[activeSequence[positionInSequence]].color.b, 1f);

        stayLitCounter = stayLit;
        shouldBeLit = true;

    }


    private void Update()
    {
        if (TimeSO.Value <= 0)
        {
            SceneManager.LoadScene("Summary");
        }

        if (shouldBeLit)
        {
            
            stayLitCounter -= Time.deltaTime;
        
            if(stayLitCounter < 0 )
            {
                btns[activeSequence[positionInSequence]].color = new Color(btns[activeSequence[positionInSequence]].color.r, btns[activeSequence[positionInSequence]].color.g, btns[activeSequence[positionInSequence]].color.b, 0.5f);

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

                    btns[activeSequence[positionInSequence]].color = new Color(btns[activeSequence[positionInSequence]].color.r, btns[activeSequence[positionInSequence]].color.g, btns[activeSequence[positionInSequence]].color.b, 1f);
                    
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
                    Debug.Log("Win and new game.");

                    DDA.X += 1;
                    //Main Score++
                    scoreSO.Value += 10;
                    scoreText.text = scoreSO.Value + "";

                    SceneManager.LoadScene("sequence_memory_game");

                    
                }
            }
            else
            {
                DDA.Y += 1;
                Debug.Log("Wrong End game");
                gameActive = false;
                SceneManager.LoadScene("sequence_memory_game");
            }
        }

        
    }


}
