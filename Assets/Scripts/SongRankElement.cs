using dj_hero;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SongRankElement : MonoBehaviour, IPointerClickHandler {

    private Song song;

    public Song Song
    {
        get
        {
            return song;
        }
        set
        {
            song = value;
            TitleText.text = song.GetTitle();
        }
    } 
    public GameObject rankContainer;
    public TMP_Text TitleText;

    public void ShowRank()
    {
        Debug.Log("click kurde");
        if(Song != null)
        {
            RankManager.ActiveSong = Song;
            RankManager.LoadRank(GameObject.Find("Canvas"));
        }
    }
    
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        ShowRank();
    }
}
