using Assets.Managers;
using dj_hero;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {

    public Text timeText;
    public Text pointsText;
    private int characterIndex;
    private int charactersNo;
    public Game game;
    public string title;
    //public GameObject A;
    //public GameObject S;
    //public GameObject D;
    //public GameObject J;
    //public GameObject K;
    //public GameObject L;
    //public Dictionary<string, GameObject> dictionaryAlphabet;

    public GameObject playBoard;

    public GameObject characterPrefab;

    private int points = 0;
    private string sTime;

    private AppearingChar passingCharacter = null;
    private bool creationNeeded = false;


    public void DisplayTime(int time)
    {
        int minutes = time / 60;
        int seconds = time % 60;

        sTime = minutes < 10 ? "0" + minutes.ToString() : minutes.ToString();
        sTime += ":";
        sTime += seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();

    }

    public void DisplayProgressBar(int percent)
    {
        Debug.Log("Progres bar wynosi " + percent);
    }

    public void DisplayPoints(int _points)
    {
        points = _points;
    }


    public void Add(AppearingChar character)
    {
        passingCharacter = character;
        creationNeeded = true;
    }

    public void UpdateCharacter(AppearingChar character)
    {
        Debug.Log("no chance");
       
    }

    public string getCharacter()
    {
        return character;
    }
    public void clearCharacter()
    {
        character = "Bug";
        pressed = false;
    }

    public bool KeyPressed()
    {
        if(pressed == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool pressed = false;

    private string character;

    private void startGame()
    {
        game = new Game(GameManager.options, GameManager.song, this);

    }
    private GameObject mainElement;
    private Queue<GameObject> queue = new Queue<GameObject>();

    // Use this for initialization
    void Start()
    {
        Thread t = new Thread(startGame);
        t.Start();
        title = GameManager.song.GetTitle();
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = sTime;
        pointsText.text = points.ToString();


        if(pressed == false)
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(vKey))
                {
                    character = vKey.ToString();
                    Debug.Log("key -"+ vKey.ToString() +" is pressed in update opinion");
                    pressed = true;

                }
            }
        }

        if (creationNeeded)
        {
            GameObject appChar = Instantiate(characterPrefab);
            appChar.transform.SetParent(playBoard.transform, false);
            appChar.GetComponent<CharacterElement>().Character = passingCharacter.character.ToString();

            float x;
            float y;
            float z;
            Vector3 pos;

            x = Random.Range(-360, 350);
            y = Random.Range(-130, 90);
            z = 0;
            pos = new Vector3(x, y, z);

            appChar.transform.localPosition = pos;


            creationNeeded = false;


            if (queue.Count==2)
            {

                mainElement.SetActive(false);
                mainElement = appChar;
                queue.Enqueue(mainElement);

                mainElement = queue.Dequeue();
                return;


            }
            else
            {
                if(mainElement == null)
                {
                    mainElement = appChar;

                }
                else
                {
                    queue.Enqueue(mainElement);
                    mainElement = appChar;
                }
            }
        }

    }

}
