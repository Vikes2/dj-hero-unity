using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionSong : MonoBehaviour {

    public Text nicknameText;

	// Use this for initialization
	void Start () {
        string nickname = Scenes.getParam("nickname");
        nicknameText.text = nickname;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
