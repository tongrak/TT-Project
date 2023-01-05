using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{

    [SerializeField]
    private Sprite bgImage;

    public Sprite[] puzzles;

    public List<Sprite> gamePuzzle = new List<Sprite>();

    public List<Button> btns = new List<Button>();

    private bool firstGuess,secondGuess;

    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle,secondGuessPuzzle;

    

    private void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("Sprites/pngwing.com");
    }

    void Start()
    {
        GetButtons();
        AddListeners();
        AddGamePuzzle();
        Shuffle(gamePuzzle);
        gameGuesses = gamePuzzle.Count / 2;

    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for(int i = 0; i < objects.Length;i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }
        
    }

    void AddGamePuzzle()
    {
        int looper = btns.Count;
        int index = 0;

        for(int i = 0; i < looper; i++)
        {
            if(index == looper / 2)
            {
                index = 0;

            }

            gamePuzzle.Add(puzzles[index]);

            index++;
        }
    }

    void AddListeners()
    {
        foreach(Button button in btns)
        {
            button.onClick.AddListener( ()=> PickAPuzzle());
        }
    }

    public void PickAPuzzle()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        if (!firstGuess)
        {
            firstGuess = true;

            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            firstGuessPuzzle = gamePuzzle[firstGuessIndex].name;


            btns[firstGuessIndex].image.sprite = gamePuzzle[firstGuessIndex];


        }
        else if(!secondGuess)
        {
            secondGuess = true;

            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            secondGuessPuzzle = gamePuzzle[secondGuessIndex].name;

            btns[secondGuessIndex].image.sprite = gamePuzzle[secondGuessIndex];

            countCorrectGuesses++;

            StartCoroutine(CheckIfThePuzzleMatch());

        }
    }

    IEnumerator CheckIfThePuzzleMatch()
    {
        yield return new WaitForSeconds(1f) ;

        if (firstGuessPuzzle == secondGuessPuzzle)
        {
            yield return new WaitForSeconds(.5f);

            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            CheckIfTheGameIsFinished();

        }
        else
        {
            yield return new WaitForSeconds(.5f);

       
            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;
        }

        yield return new WaitForSeconds(.5f);

        firstGuess = secondGuess = false;

    }

    void CheckIfTheGameIsFinished()
    {
        countCorrectGuesses++;

        if(countCorrectGuesses == gameGuesses)
        {
            Debug.Log("Game Finished");
            Debug.Log("It took you " + countCorrectGuesses + " may guess(es) to finish the game");
        }
    }

    void Shuffle(List<Sprite> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int ramdomIndex = Random.Range(0, list.Count);
            list[i] = list[ramdomIndex];
            list[ramdomIndex] = temp;
        }
    }

}