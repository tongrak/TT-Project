using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
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

    /*  Global variable */
    public Sprite[] puzzles;    //  puzzle image list

    public List<Sprite> gamePuzzles = new List<Sprite>();


    public List<Button> btns = new List<Button>();

    public GameOverScreen GameOverScreen;   //  get game over screen

    private bool isChoose;  //  check is guessing

    private int countGuesses;   //  guess already done
    private int countCorrectGuesses;    
    private int gameGuesses;

    private int currentPuzzIdx, currentAnsIdx;  //  current guess and answer index

    private string currentPuzz, currentAns; //  current guess and answer name



    /*  Method  */
    private void Awake()
    {
        //  Get asset image from Resources
        puzzles = Resources.LoadAll<Sprite>("Animal Basic Asset Pack/Free Sprites 1x");
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
     
        gameGuesses = gamePuzzles.Count / 2;
    }

    //  Create button
    void GetButton()
    {
        //  Puzzle btn
        GameObject[] Puzzle_objects = GameObject.FindGameObjectsWithTag("PuzzleBtn");

        for (int i=0; i<Puzzle_objects.Length; i++)
        {
            btns.Add(Puzzle_objects[i].GetComponent<Button>()); //  Add btn to btnList
            btns[i].image.sprite = Puzzle_bgImage;  // Change that btn bgImage
            btns[i].enabled = false;    // Set btn can't interactable   // fixed bug interact
            
        }
        currentPuzzIdx = Puzzle_objects.Length - 1;

        //  Ans btn
        GameObject[] Answer_objects = GameObject.FindGameObjectsWithTag("AnswerBtn");

        for (int i = 0; i < Answer_objects.Length; i++)
        {
            btns.Add(Answer_objects[i].GetComponent<Button>());
            btns[i].image.sprite = Puzzle_bgImage;

        }
    }

    //  Add puzzle image to gamePuzzle list
    void AddGamePuzzles()
    {
        int looper = btns.Count;    //  check button count
        int index = 0;

        for (int i=0; i<looper; i++)    // for repeat image
        {
            if(index == looper / 2)
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
        foreach(Button btn in btns)
        {
            btn.onClick.AddListener(() => PickPuzzle());    //  Use PickPuzzle when button has clicked
        }
    }

    //  Check picked answer
    public void PickPuzzle()
    {

        if (!isChoose)
        {
            isChoose = true;
            currentAnsIdx = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            currentAns = gamePuzzles[currentAnsIdx].name;
            btns[currentAnsIdx].image.sprite = gamePuzzles[currentAnsIdx];

            if(currentPuzz == currentAns)
            {
                print("Puzzle Match");
            }else
            {
                print("Puzzle don't Match");
                GameOverScreen.Setup();
            }

            StartCoroutine(checkThePuzzleMatch());

        }
    }

    //  Check selected is match
    IEnumerator checkThePuzzleMatch()
    {
        
        yield return new WaitForSeconds(0.5f);

        if(currentPuzz == currentAns)
        {
            //yield return new WaitForSeconds(0.5f);
            btns[currentPuzzIdx].interactable = false;
            //btns[currentAnsIdx].interactable = false;

            btns[currentPuzzIdx].image.color = new Color(0, 0, 0, 0);
            //btns[currentAnsIdx].image.color = new Color(0, 0, 0, 0);

            CheckTheGameFinished();
            currentPuzzIdx--;
            if (currentPuzzIdx > -1) { 
                ShowNextPuzzle();
            }
        }else
        {
            //
        }
        //yield return new WaitForSeconds(0.5f);

        isChoose = false;

        void CheckTheGameFinished()
        {
            countCorrectGuesses++;

            if (countCorrectGuesses == gameGuesses)
            {
                scoreSO.Value += 10;
                showScore.text = scoreSO.Value + "";
                print("game finished");
                print("it took you " + countGuesses + " ");
                GameOver(); // call game over
            }
        }

    }

    //  Show puzzle for remember
    IEnumerator RememPuzzleTime()
    {
        ShowAnsChoice(true);
        yield return new WaitForSeconds(0.5f);
        for (int i=0; i<btns.Count/2; i++)
        {
            yield return new WaitForSeconds(1f);
            btns[i].image.sprite = gamePuzzles[i];

        }

        yield return new WaitForSeconds(1f);
        for (int i=0; i<btns.Count/2; i++)
        {
            btns[i].image.sprite = Puzzle_bgImage;
        }
        btns[btns.Count / 2 - 1].image.sprite = CurrentGuess_bgImage;

        //  show choice 
        yield return new WaitForSeconds(0.5f);
        currentPuzz = gamePuzzles[currentPuzzIdx].name;
        ShowAnsChoice(false);
    }
    
    //  Shuffel puzzle
    void Shuffle(List<Sprite> list)
    {
        //  Puzzle Shuffle
        for (int i = 0; i < list.Count/2; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count/2);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        //  Answer Shuffle
        for (int i = list.Count / 2; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    //  Show aswer choice
    void ShowAnsChoice(bool isShowTime)
    {
        for (int i = btns.Count / 2; i < btns.Count; i++)
        {
            if (isShowTime) //  set can't interact btn in show puzzle time  // fixed btn bug 
            {
                btns[i].enabled = false;
            }
            else
            {
                btns[i].image.sprite = gamePuzzles[i];
                btns[i].enabled = true;
            }
        }
        
    }

    //  show next guess
    void ShowNextPuzzle()
    {
        btns[currentPuzzIdx].image.sprite = CurrentGuess_bgImage;
        currentPuzz = gamePuzzles[currentPuzzIdx].name;
    }

    //  show game over
    void GameOver()
    {
        GameOverScreen.Setup();
    }

    private void Update()
    {
        if(TimeSO.Value <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }
}
