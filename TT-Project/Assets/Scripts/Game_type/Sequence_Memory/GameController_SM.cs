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

    [SerializeField]
    private boolFortime pause;

    //DDA
    [SerializeField]
    private DDA DDA;

    //Mix mod
    [SerializeField]
    private boolFortime Mix;

    //Sound
    [SerializeField]
    private AudioSource Correct;

    [SerializeField]
    private AudioSource inCorrect;

    [SerializeField]
    private AudioSource soundBTN;

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

        
        //TimeSO.Value = 180;

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
            //Debug.Log(activeSequence.Count);
            //Debug.Log("btns = " + btns.Count);
        }

        btns[activeSequence[positionInSequence]].color = new Color((float)0.4509804, 1, (float)0.8666667, 1); ;

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

        //Debug.Log("len = " + objects.Length);
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
            //Debug.Log(activeSequence.Count);
            //Debug.Log("btns = " + btns.Count);
        }
        
        btns[activeSequence[positionInSequence]].color = new Color((float)0.4509804, 1, (float)0.8666667, 1); ;

        stayLitCounter = stayLit;
        shouldBeLit = true;

    }


    private void Update()
    {
        if (TimeSO.Value <= 0)
        {
            DDA.Reset();
            SceneManager.LoadScene("Summary");
        }

        if (shouldBeLit)
        {
            //Pause
            pause.Value = true;
            //Debug.Log("bool = " + pause.Value);

            stayLitCounter -= Time.deltaTime;
        
            if(stayLitCounter < 0 )
            {
                //btns[activeSequence[positionInSequence]].color = new Color(btns[activeSequence[positionInSequence]].color.r, btns[activeSequence[positionInSequence]].color.g, btns[activeSequence[positionInSequence]].color.b, 0.5f);
                btns[activeSequence[positionInSequence]].color = new Color((float)0.1176471, (float)0.1176471, (float)0.1176471, 1);
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
                //Unpause
                pause.Value = false;
                //Debug.Log("bool = " + pause.Value);
                shouldBeDark = false;
                gameActive = true;
            }
            else
            {
                if(waitBetweenCounter < 0)
                {
                    

                    btns[activeSequence[positionInSequence]].color = new Color((float)0.4509804, 1, (float)0.8666667, 1); 
                    
                    stayLitCounter = stayLit;
                    shouldBeLit = true;
                    shouldBeDark = false;
                }
            }
            
        }
    }

    


    public void ColorPressed(int whichButton)
    {
        soundBTN.Play();
        if (gameActive)
        {
            if (activeSequence[inputInSequence] == whichButton)
            {
                Debug.Log("Correct");

                inputInSequence++;

                if(inputInSequence >= activeSequence.Count)
                {
                    Debug.Log("Win and new game.");

                    Correct.Play();
                    
                    DDA.check(true);
                     

                    //Main Score++
                    if (DDA.Level == 1)
                    {
                        scoreSO.Value += 1;
                        scoreText.text = scoreSO.Value + "";
                    }
                    else if (DDA.Level == 2)
                    {
                        scoreSO.Value += 3;
                        scoreText.text = scoreSO.Value + "";
                    }
                    else if(DDA.Level == 3)
                    {
                        scoreSO.Value += 7;
                        scoreText.text = scoreSO.Value + "";
                    }

                    if (Mix.Value)
                    {
                        string[] scenes = { "sequence_memory_game", "ReverseRetention_1", "MemoRandom" };
                        int random = Random.Range(0, scenes.Length);
                        SceneManager.LoadScene(scenes[random]);
                    }
                    else
                    {
                        SceneManager.LoadScene("sequence_memory_game");
                    }

                }
            }
            else
            {
                Debug.Log("Wrong End game");
                inCorrect.Play();
                gameActive = false;
                DDA.check(false);

                if (Mix.Value)
                {
                    string[] scenes = { "sequence_memory_game", "ReverseRetention_1", "MemoRandom" };
                    int random = Random.Range(0, scenes.Length);
                    SceneManager.LoadScene(scenes[random]);
                }
                else
                {
                    SceneManager.LoadScene("sequence_memory_game");
                }
                
            }
        }

        
    }


}
