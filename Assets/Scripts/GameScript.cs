using Assets.Managers;
using dj_hero;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public Slider progresbar;
    public GameObject progresbarObject;
    public GameObject progressbarFill;

    public GameObject playBoard;

    public AudioSource mainAudioSource;
    public AudioSource minorAudioSource;

    public GameObject characterPrefab;
    private List<AppearingChar> incomingCreation = new List<AppearingChar>();
    private int incomingCounter = 0;

    public int points = 0;
    private string sTime;

    private AppearingChar passingCharacter = null;
    private GameObject mainElement;
    private Queue<GameObject> queue = new Queue<GameObject>();
    private List<Vector3> busyPositions = new List<Vector3>();
    private bool creationNeeded = false;
    private bool updateNeeded = false;
    public bool missed = false;
    public string loadSongPath = "";
    private bool endGame = false;
    public bool pressed = false;
    private float progressbarValue = 1;
    private string character;
    private int doneCreation = 0;

    public void EndGame()
    {
        endGame = true;
    }
    
    public void DisplayTime(int time)
    {
        int minutes = time / 60;
        int seconds = time % 60;

        sTime = minutes < 10 ? "0" + minutes.ToString() : minutes.ToString();
        sTime += ":";
        sTime += seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();

    }

    public void LoadSong(string path)
    {
        mainAudioSource.clip = Resources.Load<AudioClip>(path);
    }

    public void DisplayProgressBar(int percent)
    {
        progressbarValue = ((float)percent / 100);


    }

    public void DisplayPoints(int _points)
    {
        points = _points;
    }
    
    public void Add(AppearingChar character)
    {
        incomingCounter++;
        incomingCreation.Add(character);
        //passingCharacter = character;
    }

    public void UpdateCharacter(AppearingChar character)
    {
        updateNeeded = true;
        //mainElement.GetComponent<CounterScript>().Counter = character.character;
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
    
    private void startGame()
    {
        game = new Game(GameManager.Options, GameManager.Song, this);

    }

    // Use this for initialization
    void Start()
    {
        Thread t = new Thread(startGame);
        t.Start();
        title = GameManager.Song.GetTitle();
    }


    // Update is called once per frame
    void Update()
    {
        if (endGame)
        {
            GameManager.Score = points;
            Scenes.Load("EndGameScene");
        }

        progressbarFill.GetComponent<Image>().color = Color.green;

        if(progressbarValue > 0.3f && progressbarValue < 0.7f)
        {
            progressbarFill.GetComponent<Image>().color = Color.yellow;
        }

        if(progressbarValue < 0.3f)
        {
            progressbarFill.GetComponent<Image>().color = Color.red;
        }

        if(loadSongPath != "")
        {
            LoadSong(loadSongPath);
        }

        if(mainAudioSource.clip != null && !mainAudioSource.isPlaying)
        {
            mainAudioSource.Play();
        }

        timeText.text = sTime;
        pointsText.text = points.ToString();
        if (progressbarValue < 0)
        {
            progresbarObject.SetActive(false);
        }
        progresbar.value = progressbarValue;

        if(pressed == false)
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(vKey))
                {
                    character = vKey.ToString();
                    pressed = true;

                }
            }
        }

        if (updateNeeded)
        {
            mainElement.GetComponent<CounterScript>().Counter--;
            updateNeeded = false;
        }

        if (mainElement != null)
        {
            mainElement.GetComponent<CharacterElement>().character.fontSize -= 0.05f;
            mainElement.GetComponent<CounterScript>().counter.fontSize -= 0.05f;
        }

        if (incomingCounter > doneCreation)
        {
            float x;
            float y;
            float z;
            Vector3 pos;

            doneCreation++;


            string character = incomingCreation[doneCreation -1].character.ToString();
            int counter = incomingCreation[doneCreation - 1].counter;
            GameObject appChar = Instantiate(characterPrefab);
            appChar.transform.SetParent(playBoard.transform, false);
            appChar.GetComponent<CharacterElement>().Character = character;
            appChar.GetComponent<CounterScript>().Counter = counter;

            // wih 55
            bool correctPos = true;
            do
            {

                x = Random.Range(-360, 350);
                y = Random.Range(-130, 90);
                z = 0;
                pos = new Vector3(x, y, z);

                foreach (Vector3 vector3 in busyPositions)
                {
                    Debug.Log("" + vector3.x);
                    if (pos.x > vector3.x && pos.x < vector3.x + 50 && pos.y > vector3.y && pos.y < vector3.y + 50 ||
                        pos.x > vector3.x && pos.x < vector3.x + 55 && pos.y < vector3.y && pos.y > vector3.y - 55 ||
                        pos.x < vector3.x && pos.x > vector3.x - 55 && pos.y < vector3.y && pos.y > vector3.y - 55 ||
                        pos.x < vector3.x && pos.x > vector3.x - 55 && pos.y > vector3.y && pos.y < vector3.y + 50
                        )
                    {
                        Debug.Log("zleeeeeeeeeeeee prosimy jeszcze raz");
                        correctPos = false;
                        break;
                    }
                    else
                    {
                        correctPos = true;
                    }
                }
            } while (!correctPos);


            appChar.transform.localPosition = pos;

            busyPositions.Add(pos);
            if(busyPositions.Count > 3)
            {
                busyPositions.RemoveAt(0);
                busyPositions.TrimExcess();
            }
            
            if (queue.Count == 2)
            {
                Destroy(mainElement);
                queue.Enqueue(appChar);
                mainElement = queue.Dequeue();
                mainElement.GetComponent<CharacterElement>().character.color = Color.green;
                mainElement.GetComponent<CounterScript>().counter.color = Color.green;
                mainElement.GetComponent<CharacterElement>().character.fontSize = 56;
                mainElement.GetComponent<CounterScript>().counter.fontSize = 28.1f;
                queue.Peek().GetComponent<CharacterElement>().character.color = Color.blue;
                queue.Peek().GetComponent<CounterScript>().counter.color = Color.blue;
            }
            else
            {
                if (mainElement == null)
                {
                    mainElement = appChar;
                    mainElement.GetComponent<CharacterElement>().character.color = Color.green;
                    mainElement.GetComponent<CounterScript>().counter.color = Color.green;
                    mainElement.GetComponent<CharacterElement>().character.fontSize = 56;
                    mainElement.GetComponent<CounterScript>().counter.fontSize = 28.1f;
                    Debug.Log("Ellllo ze scripta pierwsza litera " + character);
                }
                else
                {
                    queue.Enqueue(appChar);
                    queue.Peek().GetComponent<CharacterElement>().character.color = Color.blue;
                    queue.Peek().GetComponent<CounterScript>().counter.color = Color.blue;
                    Debug.Log("Siiemmmma ze scripta druga albo czecia litera " + character);
                }
            }
        }


        if (missed)
        {
            minorAudioSource.Play();
            missed = false;
        }

    }

}
