using dj_hero;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RankManager{

    public static Song ActiveSong { get; set; }
    public static GameObject RankView { get; set; }
    public static GameObject SongRankView { get; set; }


    public static void LoadRank(GameObject container)
    {
        var rank = container.GetComponent<Rank>();

        rank.activeSong = ActiveSong;
        rank.LoadRank();
    }
}
