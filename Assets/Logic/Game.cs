﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using UnityEngine;

namespace dj_hero
{

    public class GameTimer
    {
        private int time;
        Game game;

        private System.Timers.Timer timer = new System.Timers.Timer(1000);

        public GameTimer(int _time, Game _game)
        {
            time = _time;
            game = _game;
        }

        public void RunTimer()
        {
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
        }

        public void StopTimer()
        {
            timer.Stop();
            timer.Dispose();
            game = null;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            time--;

            game.view.DisplayTime(time);


            if (time <= 0)
            {
                // go end game
                game.EndGame();
                timer.Stop();
                timer.Dispose();
            }
            else
            {
                //game.DecreaseProgresBarPerSec();
                game.TimeControler();
            }
        }
    }

    public sealed class Game
    {
        private GameTimer timer;
        //public GameView view;
        private int points;
        private string playerName;
        public Song song;
        private int progresBarValue;
        MatchOption matchOpttions;


        private bool gameOverProcesDone;
        private bool gameOverByUserInterrupt;

        public Game(MatchOption _matchOption, Song _song)
        {
            matchOpttions = _matchOption;
            song = _song;
            playerName = matchOpttions.nickname;
            points = 0;
            progresBarValue = matchOpttions.progresBarValue;
            timer = new GameTimer(song.duration, this);
            //keyTimer = new KeyTimer(this);
            //view = new GameView();
            gameOverByUserInterrupt = false;
            gameOverProcesDone = false;
            play();

        }
        private Thread t;
        public Thread readThread;
        //public KeyTimer keyTimer;
        public void play()
        {
            //view.Render();

            //Audio.StartSong(song);
            timer.RunTimer();
            //keyTimer.RunTimer();

            LoadSegment();
            string currentCharacter;

            t = new Thread(delegate ()
            {

                while (true)
                {
                    currentCharacter = keyTimer.getCharacter();
                    if (currentCharacter == mainElement.character.ToString().ToUpper())
                    {
                        SuccesedClick();
                    }
                    else
                    {
                        if (currentCharacter == "ESCAPE")
                        {
                            gameOverByUserInterrupt = true;
                            EndGame();
                        }
                        else
                        {
                            MissClick();
                        }
                    }
                    
                }

            });
            t.Start();
            t.Join();

            Debug.Log("koniec gry");

            //if (gameOverByUserInterrupt == true)
            //{
                
            //    MenuView menuView = new MenuView();
            //    menuView.Init();
            //}
            //else
            //{
            //    Ranking ranking = new Ranking(song);
            //    ranking.AddRecord(playerName, points);

            //    EndGameView endGameView = new EndGameView(points, song, matchOpttions);
            //}
        }


        private void SuccesedClick()
        {
            // ++ points
            points += 10;
            view.DisplayPoints(points);
            // progres bar ++
            IncreaseProgresBar();
            //load next segment
            mainElement.counter--;
            LoadSegment();
        }

        private void MissClick()
        {
            if (gameOverProcesDone)
                return;
            Audio.StartServiceTrack("beep");

            // progresbar -- or nothing
            DecreaseProgresBarPerMiss();
            // load next segment
            mainElement.counter = 0;
            LoadSegment();
        }

        private AppearingChar mainElement;
        private Queue<AppearingChar> queue = new Queue<AppearingChar>();
        private void LoadSegment()
        {
            if(gameOverProcesDone)
            {
                return;
            }
            RefreshTimeToAnswer();
            //========================
            //3 posibility
            //first load
            if (mainElement == null)
            {

                for (int i = 1; i <= matchOpttions.amountElementsSameTime; i++)
                {
                    mainElement = new AppearingChar(matchOpttions);
                    queue.Enqueue(mainElement);
                    view.Add(mainElement);
                }
                mainElement = queue.Dequeue();

                return;
            }
            //refresh
            if (mainElement.counter > 0)
            {
                view.UpdateCharacter(mainElement);
            }
            //hit
            if (mainElement.counter == 0)
            {
                mainElement = new AppearingChar(matchOpttions);
                queue.Enqueue(mainElement);
                view.Add(mainElement);

                mainElement = queue.Dequeue();

            }
        }
        private Thread endGameThread;
        public void EndGame()
        {
            if (timer != null)
            {
                timer.StopTimer();
                keyTimer.StopTimer();
            }
            timer = null;
            try
            {
                Audio.StopSong();
            }
            catch { };

            endGameThread = new Thread(delegate ()
            {

                t.Abort();

            });
            endGameThread.Start();
            gameOverProcesDone = true;
            t.Abort();

        }

        // -- operation on progresbarr

        public void DecreaseProgresBarPerSec()
        {
            if (progresBarValue < 1)
                return;
            progresBarValue -= matchOpttions.progresBarLosePerSec;
            view.DisplayProgressBar(progresBarValue);
            if (progresBarValue < 1)
            {
                EndGame();
            }
        }

        public void DecreaseProgresBarPerMiss()
        {
            if (progresBarValue < 1)
                return;
            progresBarValue -= matchOpttions.decPointsPerMiss;
            view.DisplayProgressBar(progresBarValue);
            if (progresBarValue < 1)
            {
                EndGame();
            }
        }

        public void IncreaseProgresBar()
        {
            progresBarValue += matchOpttions.incPointsPerSucceed;
            view.DisplayProgressBar(progresBarValue);
            if (progresBarValue > 100)
                progresBarValue = 100;
        }
        // --------------------------------------------------------------

        //clock stuff
        public void TimeControler()
        {
            timeToAnswer--;

            if (timeToAnswer == 0)
            {
                MissClick(); //miss answer function
                timeToAnswer = matchOpttions.answerTime;
            }
        }
        private int timeToAnswer;

        private void RefreshTimeToAnswer()
        {
            timeToAnswer = matchOpttions.answerTime;
        }
    }
}
