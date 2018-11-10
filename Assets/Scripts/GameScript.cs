using Assets.Managers;
using dj_hero;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {

    public Text timeText;
    private int characterIndex;
    private int charactersNo;
    public Game game;
    public string title = GameManager.song.GetTitle();

    private string sTime;


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

    public void DisplayPoints(int points)
    {
        Debug.Log("Punkty wynosza " + points);

    }


    public void Add(AppearingChar character)
    {
        Debug.Log("Dodano literke " + character.character);
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
        character = null;
    }


    private string character;

    // Use this for initialization
    void Start()
    {
        game = new Game(GameManager.options, GameManager.song, this);
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = sTime;

        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(vKey))
            {
                character = vKey.ToString();

            }
        }
    }

}



//public class KeyTimer
//{

//    Game game;
//    public ConsoleKeyInfo pressedKey;

//    private System.Timers.Timer timer = new System.Timers.Timer(100);

//    public KeyTimer(Game _game)
//    {
//        pressedKey = new ConsoleKeyInfo();

//        game = _game;
//    }

//    public void RunTimer()
//    {
//        timer.Elapsed += Timer_Elapsed;
//        timer.Enabled = true;
//    }

//    public void StopTimer()
//    {
//        pressedKey = new ConsoleKeyInfo();
//        timer.Stop();
//        timer.Dispose();
//        game = null;
//        pressedKey = new ConsoleKeyInfo();

//    }
//    public string getCharacter()
//    {
//        while (pressedKey.Key == 0) ;

//        string character = pressedKey.Key.ToString();
//        if (pressedKey.Key == ConsoleKey.Escape)
//        {
//            character = "ESCAPE";
//        }
//        pressedKey = new ConsoleKeyInfo();

//        return character;
//    }

//    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
//    {
//        if (Console.KeyAvailable)
//        {
//            pressedKey = new ConsoleKeyInfo();
//            pressedKey = Console.ReadKey(true);
//        }
//    }
//}
