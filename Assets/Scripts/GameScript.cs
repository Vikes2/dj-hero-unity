using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {

    private int characterIndex;
    private int charactersNo;


    public void DisplayTime(int time)
    {
        int minutes = time / 60;
        int seconds = time % 60;

        string sTime = minutes < 10 ? "0" + minutes.ToString() : minutes.ToString();
        sTime += ":";
        sTime += seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();
    }

    public void DisplayProgressBar(int percent)
    {

    }

    public void DisplayPoints(int points)
    {
    }


    public void Add()
    {
       
    }

    public void UpdateCharacter()
    {
       
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
