using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using dj_hero;
using UnityEngine.SceneManagement;
using Assets.Managers;

public class SongElement : MonoBehaviour {

    public TMP_Text titleText;
    private string title;
    private Song song;
    public string nickname;
    
    public string Title
    {
        get
        {
            return title;
        }
        set
        {
            title = value;
            titleText.text = title;
        }
    }

    public Song Song
    {
        get
        {
            return song;
        }
        set
        {
            song = value;
            titleText.text = song.GetTitle();
        }
    }

    public void SongOnClick()
    {
        MatchOption matchOptions = new MatchOption(song);
        matchOptions.nickname = nickname;
        GameManager.options = matchOptions;
        GameManager.song = song;
        SceneManager.LoadScene(2);
        
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
