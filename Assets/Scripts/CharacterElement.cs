using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterElement : MonoBehaviour {

    public TMP_Text character;
    private string chatecterString;

    public string Character
    {
        get
        {
            return chatecterString;
        }
        set
        {
            chatecterString = value;
            character.text = chatecterString;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
