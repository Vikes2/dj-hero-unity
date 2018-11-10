using dj_hero;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Rank : MonoBehaviour {

    private List<Song> songs;
    private List<GameObject> songElements = new List<GameObject>();
    private List<GameObject> songRankElements = new List<GameObject>();
    private GameObject rankView;
    private GameObject songRankView;

    public TMP_Text Header;
    public AudioSource rankAudioSource;

    public GameObject rankContainer;
    public GameObject songRankContainer;

    public GameObject songRankPrefab;
    public GameObject songRankTableRowPrefab;
    public Song activeSong;

    public void ToggleViews()
    {
        bool isRankViewActive = rankView.activeInHierarchy;

        rankView.SetActive(!isRankViewActive);
        songRankView.SetActive(isRankViewActive);

        if (isRankViewActive)
        {
            rankAudioSource.Stop();
        }
        else
        {
            rankAudioSource.Play();
        }
    }

    public void BackToMenu()
    {
        //foreach (var song in songElements)
        //{
        //    Destroy(song);
        //}

        //songElements = new List<GameObject>();

        SceneManager.LoadScene(0);
    }

    public void LoadRank()
    {
        ToggleViews();
        Header.text = "Ranking dla " + activeSong.GetTitle();

        dj_hero.Rank rank = new dj_hero.Rank(activeSong);
        foreach(var song in songRankElements)
        {
            Destroy(song);
        }

        songRankElements = new List<GameObject>();

        for(int i = 0; i < rank.Scores.Count; i++)
        {
            GameObject row = Instantiate(songRankTableRowPrefab);
            row.transform.SetParent(songRankContainer.transform, false);
            var textControls = row.GetComponentsInChildren<TMP_Text>();
            textControls[0].text = (i+1).ToString();
            textControls[1].text = rank.Scores[i].nickname;
            textControls[2].text = rank.Scores[i].points.ToString();
            songRankElements.Add(row);
        }

    }

	// Use this for initialization
	void Start () {
        rankView = GameObject.Find("RankView");
        songRankView = GameObject.Find("SongRankView");

        songRankView.SetActive(false);

        //RankManager.RankView = rankView;
        //RankManager.SongRankView = songRankView;

       
        if (rankView.activeInHierarchy)
        {
            songs = Audio.GetSongList();

            for (int i = 0; i < songs.Count; i++)
            {
                var rankContainer = GameObject.Find("RankContent");
                GameObject songElement = Instantiate(songRankPrefab); //Inicjalizuje nowy obiekt na liście
                songElement.transform.SetParent(rankContainer.transform, false); //Ustawia go na liście
                songElement.GetComponent<SongRankElement>().Song = songs[i]; //Nadaje mu obiekt piosenki
                songElement.GetComponent<SongRankElement>().rankContainer = rankContainer;
                songElements.Add(songElement); //Dodaje do listy prefabów
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
