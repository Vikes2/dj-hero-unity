using dj_hero;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGame : MonoBehaviour {

    public InputField name;
    public Text nickText;
    public string nameString = "Grzesiek";

    private Song testSong;

    public void GetString()
    {
        nameString = name.text;
    }

    public void SelectSong()
    {


        //GameObject.Find("NickView").SetActive(false);
    }

    public void PlayOnClick()
    {
        nameString = name.text;
        LoadSongs();
    }


    private void LoadSongs()
    {
        nickText.text = nameString;

    }


    public void SongClick()
    {
        Debug.Log("song click");
    }

    // Use this for initialization
    void Start () {
        //GameObject.Find("SelectionSongView").SetActive(false);
        testSong = new Song("Imagine Dragons - Natural1.mp3");

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
