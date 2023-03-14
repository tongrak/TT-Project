using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RDM_GameManager : MonoBehaviour
{
    /*  SerialField variable */
    [SerializeField]    // Puzzle image bg
    private Sprite Puzzle_bgImage;

    [SerializeField]    // Ans image bg
    private Sprite Answer_bgImage;

    [SerializeField]
    private Sprite CurrentGuess_bgImage;

    //Score
    [SerializeField]
    private FloatSO scoreSO;

    //Show Score
    [SerializeField]
    private TMP_Text showScore;

    //Time
    [SerializeField]
    private FloatSO TimeSO;

    //DDA
    [SerializeField]
    private DDA DDA;

    /*  Global variable */
    public Sprite[] puzzles;    //  puzzle image list

    public List<Sprite> gamePuzzles = new List<Sprite>();


    public List<GameObject> btns = new List<GameObject>();

    public RDM_GameOverScreen GameOverScreen;   //  get game over screen

    private bool isChoose;  //  check is guessing

    private int countGuesses;   //  guess already done
    private int countCorrectGuesses;    
    private int gameGuesses;

    private int currentAnsIdx, currentGuessesIdx;  //  current guess and answer index

    private string currentAns; //  current answer name

    private bool isRememTime = false;
    private float rememTimeStamp;

    private int puzzleSize;
    private int ansSize;
    public List<string> guessesList;

    [SerializeField]
    private RDM_GameLevel RDM_GameLevels;

    [SerializeField]
    private RDM_LevelInfoSO RDM_LevelInfoSOs;

    [SerializeField]
    private AudioSource clickSE;

    [SerializeField]
    private AudioSource correctSE;

    [SerializeField]
    private AudioSource wrongSE;

    [SerializeField]
    private boolFortime boolMixs;

    [SerializeField]
    private GameObject placeHold;

    [SerializeField]
    private Transform backGround;

    [SerializeField]
    private TMP_Text gameLevelText;

    [SerializeField]
    private TMP_Text easyPass;

    [SerializeField]
    private TMP_Text normalPass;

    [SerializeField]
    private TMP_Text hardPass;

    /*  Method  */
    private void Awake()
    {
        if (DDA.Level == 1)
        {
            backGround.GetComponent<Image>().color = new Color((float)0.6039216, (float)0.9764706, (float)0.4705882, 1);
            gameLevelText.text = "EASY";
        }
        else if(DDA.Level == 2)
        {
            backGround.GetComponent<Image>().color = new Color((float)0.9764706, (float)0.8941177, (float)0.4705882, 1);
            gameLevelText.text = "NORMAL";
            gameLevelText.fontSize = 64;
        }else
        {
            backGround.GetComponent<Image>().color = new Color((float)0.9372549, (float)0.3490196, (float)0.2666667, 1);
            gameLevelText.text = "HARD";
        }
        easyPass.text = DDA.Ex.ToString();
        normalPass.text = DDA.Nx.ToString();
        hardPass.text = DDA.Hx.ToString();

        //  Get asset image from Resources
        puzzles = Resources.LoadAll<Sprite>("Sprites_Memo_Random/Animal Basic Asset Pack/Free Sprites 1x");
        RDM_GameLevels.setEnd();

        //  Shuffle image
        Sprite tmpShuffle;
        for (int i = 0; i < puzzles.Length; i++)
        {
            int rnd = Random.Range(0, puzzles.Length);
            tmpShuffle = puzzles[rnd];
            puzzles[rnd] = puzzles[i];
            puzzles[i] = tmpShuffle;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GetButton();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);


        showScore.text = scoreSO.Value + "";
        StartCoroutine(RememPuzzleTime());
     
    }

    //  Create button
    void GetButton()
    {
        //  Puzzle btn
        GameObject[] Puzzle_objects = GameObject.FindGameObjectsWithTag("PuzzleBtn");
        puzzleSize = Puzzle_objects.Length;

        for (int i=0; i< puzzleSize; i++)
        {
            btns.Add(Puzzle_objects[i]); //  Add btn to btnList
            btns[i].transform.GetComponent<Image>().sprite = Puzzle_bgImage;  // Change that btn bgImage
            btns[i].transform.GetComponent<Button>().enabled = false;    // Set btn can't interactable   // fixed bug interact
            print("x" + btns[i].transform.gameObject.transform);
            print("y" + btns[i].transform.localScale.y);
            print("z" + btns[i].transform.localScale.z);

        }

        //  Ans btn
        GameObject[] Answer_objects = GameObject.FindGameObjectsWithTag("AnswerBtn");
        ansSize = Answer_objects.Length;

        for (int i = 0; i < ansSize; i++)
        {
            btns.Add(Answer_objects[i]);
            btns[puzzleSize+i].GetComponent<Image>().sprite = Answer_bgImage;

        }
    }

    //  Add puzzle image to gamePuzzle list
    void AddGamePuzzles()
    {
        int index = 0;

        for (int i=0; i<puzzleSize+ansSize; i++)
        {
            if (i == puzzleSize)
            {
                index = 0;
            }
            gamePuzzles.Add(puzzles[index]);
            index++;
        }
    }

    //  Check button interaction
    void AddListeners()
    {
        foreach(GameObject btn in btns)
        {
            btn.transform.GetComponent<Button>().onClick.AddListener(() => PickPuzzle());    //  Use PickPuzzle when button has clicked
        }
    }

    //  Check picked answer
    public void PickPuzzle()
    {
        clickSE.Play();
        if (!isChoose)
        {
            isChoose = true;
            currentAnsIdx = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name); // get select ans name
            
            currentAns = gamePuzzles[currentAnsIdx].name;
            //btns[currentAnsIdx].image.sprite = gamePuzzles[currentAnsIdx];

            if(guessesList.Contains(currentAns))
            {
                print("Puzzle Match");
                for(int i=0; i<puzzleSize; i++)
                {
                    if(gamePuzzles[i].name == currentAns)
                    {
                        currentGuessesIdx = i;
                    }
                }
            }else
            {
                print("Puzzle don't Match");
                wrongSE.Play();
                DDA.check(false);

                StartCoroutine(runOver());
            }

            StartCoroutine(checkThePuzzleMatch());

        }
    }

    IEnumerator runOver()
    {
        yield return new WaitForSeconds(1f);
        GameOver();
    }

    //  Random guesses
    void RandomGuess(int guessSize, int randCount)
    {
        for(int i=0; i<randCount; i++)
        {
            int rnd = Random.Range(0, guessSize);
            gameGuesses = randCount;
            if (guessesList.Count == 0)
            {
                guessesList.Add(gamePuzzles[rnd].name);
            }
            else if (guessesList.Contains(gamePuzzles[rnd].name))
            {
                i--;
            }
            else
            {
                guessesList.Add(gamePuzzles[rnd].name);
            }

        }
    }

    //  Check selected is match
    IEnumerator checkThePuzzleMatch()
    {
        
        yield return new WaitForSeconds(0.5f);

        if(guessesList.Contains(currentAns))
        {
            btns[currentGuessesIdx].transform.GetComponent<Image>().sprite = gamePuzzles[currentAnsIdx];

            if (CheckTheGameFinished())
            {
                correctSE.Play();
                StartCoroutine(runOver());
            }
        }else
        {
            //
        }
        //yield return new WaitForSeconds(0.5f);

        isChoose = false;

        bool CheckTheGameFinished()
        {
            countCorrectGuesses++;

            if (countCorrectGuesses == gameGuesses)
            {
                correctSE.Play();
                scoreSO.Value += 10;
                showScore.text = scoreSO.Value + "";

                //DDA
                DDA.check(true);

                print("game finished");
                print("it took you " + countGuesses + " ");
                return true;
            }
            return false;
        }

    }

    //  Show puzzle for remember
    IEnumerator RememPuzzleTime()
    {
        rememTimeStamp = TimeSO.Value;
        isRememTime = true;

        ShowAnsChoice(true);
        yield return new WaitForSeconds(0.5f);
        for (int i=0; i<puzzleSize; i++)
        {
            btns[i].transform.GetComponent<Image>().sprite = gamePuzzles[i];

        }

        RandomGuess(puzzleSize, RDM_LevelInfoSOs.SlotOpened);
        yield return new WaitForSeconds(3f);
        for (int i=0; i<puzzleSize; i++)
        {
            if (guessesList.Contains(gamePuzzles[i].name))    //  if puzzle in guesslist change it bg
            {
                btns[i].transform.GetComponent<Image>().sprite = Puzzle_bgImage;
            }
        }

        //  show choice 
        yield return new WaitForSeconds(0.5f);
        isRememTime = false;
        ShowAnsChoice(false);
    }
    
    //  Shuffel puzzle
    void Shuffle(List<Sprite> list)
    {
        //  Puzzle Shuffle
        for (int i = 0; i < puzzleSize; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, puzzleSize);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        //  Answer Shuffle
        for (int i = puzzleSize; i < puzzleSize+ansSize; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, puzzleSize+ansSize);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    //  Show aswer choice
    void ShowAnsChoice(bool isShowTime)
    {
        for (int i = puzzleSize; i < puzzleSize+ansSize; i++)
        {
            if (isShowTime) //  set can't interact btn in show puzzle time  // fixed btn bug 
            {
                btns[i].transform.GetComponent<Button>().enabled = false;
                placeHold.SetActive(true);
            }
            else
            {
                btns[i].transform.GetComponent<Image>().sprite = gamePuzzles[i];
                btns[i].transform.GetComponent<Button>().enabled = true;
                placeHold.SetActive(false);
            }
        }
        
    }

    //  show game over
    void GameOver()
    {
        GameOverScreen.Setup(boolMixs.Value);
    }

    private void Update()
    {
        if (isRememTime)
        {
            TimeSO.Value = rememTimeStamp;
        }
        if(TimeSO.Value <= 0)
        {
            SceneManager.LoadScene("Summary");
        }
    }
}
