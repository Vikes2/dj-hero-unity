using dj_hero;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewGame : MonoBehaviour {

    private GameObject newGameView;
    private GameObject songSelectionView;

    private List<GameObject> songItems = new List<GameObject>();

    public TMP_InputField nicknameInput;
    public TMP_Text nicknameErrorInput;
    public TMP_Text welcomeText;
    public AudioSource songSelectionAudio;
    public AudioSource newGameAudioSource;

    public GameObject songContainer;
    public GameObject songPrefab;

    private List<Song> songs;
    private Song testSong;

    public void BackToMenu()
    {
        newGameAudioSource.Stop();
        Scenes.Load("MenuScene");
    }

    public void ToggleView()
    {
        bool isNewGameActive = newGameView.activeInHierarchy;

        if(isNewGameActive && nicknameInput.text == string.Empty)
        {
            nicknameErrorInput.text = "Nieprawidlowy nick";
            newGameAudioSource.Play();
            return;
        }

        newGameAudioSource.Stop();

        nicknameErrorInput.text = "";

        newGameView.SetActive(!isNewGameActive);
        songSelectionView.SetActive(isNewGameActive);

        if (isNewGameActive)
        {
            welcomeText.text = "Witaj " + nicknameInput.text;

            songSelectionAudio.Play();

            for(int i = 0; i < songs.Count; i++)
            {
                GameObject songElement = Instantiate(songPrefab); //Inicjalizuje nowy obiekt na liście
                songElement.transform.SetParent(songContainer.transform, false); //Ustawia go na liście
                songElement.GetComponent<SongElement>().Song = songs[i]; //Nadaje mu obiekt piosenki
                songElement.GetComponent<SongElement>().nickname = nicknameInput.text;

                songItems.Add(songElement); //Dodaje do listy prefabów
            }
        }
        else
        {
            songSelectionAudio.Stop();
        }
    }


    public void SongClick()
    {
        Debug.Log("song click");
    }

    // Use this for initialization
    void Start () {
        newGameView = GameObject.Find("NewGameView");
        songSelectionView = GameObject.Find("SongSelectionView");

        songSelectionView.SetActive(false);

        songs = Audio.GetSongList();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
