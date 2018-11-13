using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Managers;

public class EndGame : MonoBehaviour {

    public TMP_Text scoreText;

    public void PlayAgain()
    {
        Scenes.Load("GameScene");
    }

    public void BackToMenu()
    {
        Scenes.Load("MenuScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

	// Use this for initialization
	void Start () {
        scoreText.text = "Twój wynik: " + GameManager.Score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
