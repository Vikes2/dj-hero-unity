using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace dj_hero
{
    public class Rank
    {
        private static readonly string rankingPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/DJH_MusicFiles/Ranking/";
        private ListSerializer<Score> XmlList;
        private readonly Song song;
        private List<Score> scores = new List<Score>();

        public List<Score> Scores
        {
            get
            {
                return scores;
            }
        }

        public struct Score
        {
            public string nickname;
            public int points;

            public Score(string _nickname, int _points)
            {
                nickname = _nickname;
                points = _points;
            }
        }

        public Rank(Song _song)
        {
            song = _song;
            InitSerialize();
        }

        public void InitSerialize()
        {
            if (!Directory.Exists(rankingPath))
            {
                Directory.CreateDirectory(rankingPath);
            }
            XmlList = new ListSerializer<Score>(rankingPath + song.GetTitle(), song.GetTitle() + ".xml", scores);
            scores = XmlList.PullData();
        }


        public void AddRecord(string playerName, int playerScore)
        {
            Score s = new Score(playerName, playerScore);
            scores.Add(s);
            scores = scores.OrderByDescending(o => o.points).ToList();

            if (scores.Count() > 10)
            {
                scores.RemoveAt(scores.Count - 1);
            }
            XmlList = new ListSerializer<Score>(rankingPath + song.GetTitle(), song.GetTitle() + ".xml", scores);
            XmlList.PushData();
            return;
        }

    }
}
