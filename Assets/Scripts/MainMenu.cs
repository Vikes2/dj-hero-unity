using dj_hero;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Rank()
    {
        Scenes.Load("RankScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

	// Use this for initialization
	void Start () {
        Audio.PrepareSongs();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
