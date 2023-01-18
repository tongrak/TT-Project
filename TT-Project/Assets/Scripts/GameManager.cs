using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /*  SerialField variable */
    [SerializeField]    // Puzzle image bg
    private Sprite Puzzle_bgImage;

    [SerializeField]    // Ans image bg
    private Sprite Answer_bgImage;

    /*  Global variable */
    public Sprite[] puzzles;    //  puzzle image list

    public List<Sprite> gamePuzzles = new List<Sprite>();


    public List<Button> btns = new List<Button>();

    private bool firstGuess, secondGuess;

    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;

    /*  My variable */
    public int currentPuzzIdx;


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

        ShowPuzzle(gamePuzzles);
        ShowNextPuzzle(gamePuzzles);

        gameGuesses = gamePuzzles.Count / 2;
    }

    void GetButton()
    {
        //  Change bgImage
        //  Puzzle btn
        GameObject[] Puzzle_objects = GameObject.FindGameObjectsWithTag("PuzzleBtn");


        for (int i=0; i<Puzzle_objects.Length; i++)
        {
            btns.Add(Puzzle_objects[i].GetComponent<Button>()); //  Add btn to btnList
            btns[i].image.sprite = Puzzle_bgImage;  // Change that btn bgImage
            
        }

        /*  wah */
        currentPuzzIdx = Puzzle_objects.Length - 1;
        //firstGuessPuzzle = gamePuzzles[currentPuzzIdx].name;
        //btns[currentPuzzIdx].image.sprite = gamePuzzles[currentPuzzIdx];

        //  Ans btn
        GameObject[] Answer_objects = GameObject.FindGameObjectsWithTag("AnswerBtn");

        for (int i = 0; i < Answer_objects.Length; i++)
        {
            btns.Add(Answer_objects[i].GetComponent<Button>());
            btns[i].image.sprite = Puzzle_bgImage;

        }
    }

    void AddGamePuzzles()
    {
        //  Add puzzle image to gamePuzzle list
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
    void AddListeners()
    {
        foreach(Button btn in btns)
        {
            btn.onClick.AddListener(() => PickPuzzle());    //  Use PickPuzzle when button has clicked
        }
    }

    public void PickPuzzle()
    {

        /*
        //  Check first pick is equal to second pick
        if (!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;

            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
        }
        else if (!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;

            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            
            if(firstGuessPuzzle == secondGuessPuzzle)
            {
                print("Puzzle Match");
            }
            else
            {
                print("Puzzle don't Match");
            }

            StartCoroutine(checkThePuzzleMatch());
        }
        */
        /*  Wah */
        if (!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];

            if(firstGuessPuzzle == secondGuessPuzzle)
            {
                print("Puzzle Match");
            }else
            {
                print("Puzzle don't Match");
            }

            StartCoroutine(checkThePuzzleMatch());

        }
    }

    //  Check selected is match
    IEnumerator checkThePuzzleMatch()
    {
        /*
        yield return new WaitForSeconds(0.5f);

        if (firstGuessPuzzle == secondGuessPuzzle)
        {
            //  if match set invissible
            yield return new WaitForSeconds(0.5f);
            btns[firstGuessIndex].interactable = false; //  set can not  interaction
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            CheckTheGameFinished();

        }else
        {
            //  if not match return bgImage
            btns[firstGuessIndex].image.sprite = Answer_bgImage;
            btns[secondGuessIndex].image.sprite = Answer_bgImage;
        }
        yield return new WaitForSeconds(0.5f);

        firstGuess = secondGuess = false;

        void CheckTheGameFinished() {
            countCorrectGuesses++;

            if(countCorrectGuesses == gameGuesses)
            {
                print("game finished");
                print("it took you " + countGuesses + " ");
            }
        }
        */
        /*  Wah */
        yield return new WaitForSeconds(0.5f);

        if(firstGuessPuzzle == secondGuessPuzzle)
        {
            yield return new WaitForSeconds(0.5f);
            btns[currentPuzzIdx].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[currentPuzzIdx].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            CheckTheGameFinished();
            if (currentPuzzIdx > -1) { 
                currentPuzzIdx--;
                ShowNextPuzzle(gamePuzzles);
            }
        }else
        {
            //
        }
        yield return new WaitForSeconds(0.5f);

        secondGuess = false;

        void CheckTheGameFinished()
        {
            countCorrectGuesses++;

            if (countCorrectGuesses == gameGuesses)
            {
                print("game finished");
                print("it took you " + countGuesses + " ");
            }
        }

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

    /*  My method   */
    void ShowPuzzle(List<Sprite> list)
    {
        //firstGuessPuzzle = gamePuzzles[currentPuzzIdx].name;
        //btns[currentPuzzIdx].image.sprite = gamePuzzles[currentPuzzIdx];
        /*
        for (int i = 0; i<list.Count/2; i++)
        {
            btns[i].image.sprite = gamePuzzles[i];
        }
        */
        for (int i = list.Count / 2; i < list.Count; i++)
        {
            btns[i].image.sprite = gamePuzzles[i];
        }
        
    }
    void ShowNextPuzzle(List<Sprite> list)
    {
        btns[currentPuzzIdx].image.sprite = gamePuzzles[currentPuzzIdx];
        firstGuessPuzzle = gamePuzzles[currentPuzzIdx].name;
    }
}
