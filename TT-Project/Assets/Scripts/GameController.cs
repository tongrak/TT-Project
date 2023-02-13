using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{

    //Image
    [SerializeField]
    private Sprite bgImage;

    //Score
    [SerializeField]
    private FloatSO scoreSO;

    //Time
    [SerializeField]
    private FloatSO TimeSO;

    [SerializeField]
    private TMP_Text scoreText;

    //Puzzles
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
        countGuesses = 0;
    }

    void Start()
    {
        scoreText.text = scoreSO.Value + "";
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

            countGuesses += 1;
            StartCoroutine(CheckIfThePuzzleMatch());

        }
    }

    IEnumerator CheckIfThePuzzleMatch()
    {
        yield return new WaitForSeconds(.5f) ;

        if (firstGuessPuzzle == secondGuessPuzzle)
        {
            yield return new WaitForSeconds(.5f);

            //Main Score++
            scoreSO.Value += 10;
            scoreText.text = scoreSO.Value + "";

            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            Debug.Log(countCorrectGuesses);
            Debug.Log(gameGuesses);

            CheckIfTheGameIsFinished();

        }
        else
        {
            yield return new WaitForSeconds(.5f);

            //Main Score
            /*if(scoreSO.Value > 0)
            {
                scoreSO.Value -= 2;
                scoreText.text = scoreSO.Value + "";
            }*/
            
            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;
        }

        yield return new WaitForSeconds(.5f);

        firstGuess = secondGuess = false;

        Debug.Log("Score = " + scoreSO.Value);
    }

    void CheckIfTheGameIsFinished()
    {
        countCorrectGuesses++;

        if(countCorrectGuesses == gameGuesses)
        {
            Debug.Log("Game Finished");
            Debug.Log("It took you " + countGuesses + " may guess(es) to finish the game");
            int index = Random.Range(5, 7);
            SceneManager.LoadScene(index);
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

    private void Update()
    {
        if(TimeSO.Value <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }

}