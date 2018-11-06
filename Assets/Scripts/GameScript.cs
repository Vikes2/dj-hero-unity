using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {

    // Use this for initialization
    private ViewElement timer;
    private ViewElement progressBar;
    private ViewElement[] characters;
    private ViewElement[] counters;

    private bool[,] vacancy;

    private int characterIndex;
    private int charactersNo;

    protected ViewElement points;


    public static readonly object locker = new object();
    public static bool isWriting = false;


    public GameView()
    {
        characterIndex = 0;
        charactersNo = 3;

        characters = new ViewElement[charactersNo];
        counters = new ViewElement[charactersNo];
        vacancy = new bool[Console.WindowHeight, Console.WindowWidth];

        timer = new ViewElement(Console.WindowWidth - 8, 1, 5, 3,
            new List<string>()
            {
                    "TIMER",
                    "",
                    "00:00"
            });
        progressBar = new ViewElement(3, 1, 27, 5, new List<string>() { "" });
        points = new ViewElement((Console.WindowWidth - 2) / 2, 1, 5, 1, new List<string>() { "0" });


        Elements.Add("ProgressBar", progressBar);
        Elements.Add("Points", points);
        Elements.Add("Timer", timer);

        progressBar.ForegroundColor = ConsoleColor.Green;
        DisplayProgressBar(100);



        InitCharacters();
    }






    public void DisplayTime(int time)
    {
        int minutes = time / 60;
        int seconds = time % 60;

        string sTime = minutes < 10 ? "0" + minutes.ToString() : minutes.ToString();
        sTime += ":";
        sTime += seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();

        Elements["Timer"].Lines[2] = sTime;
        Elements["Timer"].Update();
    }

    public void DisplayProgressBar(int percent)
    {
        string ret = "[";
        for (int i = 0; i < 25; i++)
        {
            if (percent / 4 > i)
                ret = ret + "|";
            else
                ret = ret + " ";
        }
        ret = ret + "]";

        Elements["ProgressBar"].Lines[0] = ret;

        if (percent <= 30)
        {
            Elements["ProgressBar"].ForegroundColor = ConsoleColor.Red;
        }
        else if (percent <= 70)
            Elements["ProgressBar"].ForegroundColor = ConsoleColor.Yellow;
        else
            Elements["ProgressBar"].ForegroundColor = ConsoleColor.Green;

        Elements["ProgressBar"].Update();
    }

    public void DisplayPoints(int points)
    {
        Elements["Points"].Lines[0] = points.ToString();
        Elements["Points"].Update();
    }


    public void Add(AppearingChar character)
    {
       
    }

    public void UpdateCharacter(AppearingChar character)
    {
       
    }

}

public class KeyTimer
{

    Game game;
    public ConsoleKeyInfo pressedKey;

    private System.Timers.Timer timer = new System.Timers.Timer(100);

    public KeyTimer(Game _game)
    {
        pressedKey = new ConsoleKeyInfo();

        game = _game;
    }

    public void RunTimer()
    {
        timer.Elapsed += Timer_Elapsed;
        timer.Enabled = true;
    }

    public void StopTimer()
    {
        pressedKey = new ConsoleKeyInfo();
        timer.Stop();
        timer.Dispose();
        game = null;
        pressedKey = new ConsoleKeyInfo();

    }
    public string getCharacter()
    {
        while (pressedKey.Key == 0) ;

        string character = pressedKey.Key.ToString();
        if (pressedKey.Key == ConsoleKey.Escape)
        {
            character = "ESCAPE";
        }
        pressedKey = new ConsoleKeyInfo();

        return character;
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        if (Console.KeyAvailable)
        {
            pressedKey = new ConsoleKeyInfo();
            pressedKey = Console.ReadKey(true);
        }
    }
}
