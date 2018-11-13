using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using dj_hero;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Assets.Managers;

public class SongElement : MonoBehaviour, IPointerClickHandler {

    public TMP_Text titleText;
    public TMP_Text difficultyText;
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
            difficultyText.text = "Poziom trudnosci: " + song.getDifficultyName();
        }
    }

    public void SongOnClick(PointerEventData eventData)
    {
        MatchOption matchOptions = new MatchOption(song);
        matchOptions.nickname = nickname;
        GameManager.Options = matchOptions;
        GameManager.Song = song;
        SceneManager.LoadScene(2);
        
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        MatchOption matchOptions = new MatchOption(song);
        matchOptions.nickname = nickname;
        GameManager.Options = matchOptions;
        GameManager.Song = song;
        SceneManager.LoadScene(2);
    }
}
