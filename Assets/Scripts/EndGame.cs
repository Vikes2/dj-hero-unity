using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour {

    public TMP_Text scoreText;

    public void PlayAgain()
    {

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
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
