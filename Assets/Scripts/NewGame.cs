using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGame : MonoBehaviour {

    public InputField name;
    public string nameString = "Grzesiek";

    public void GetString()
    {
        nameString = name.text;
    }

    public void SelectSong()
    {
        GameObject.Find("NickView").SetActive(false);
    }

	// Use this for initialization
	void Start () {
        //GameObject.Find("SelectionSongView").SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
