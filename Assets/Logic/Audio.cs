using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace dj_hero
{
    public static class Audio
    {
        private static bool prepared = false;
        private static List<Song> songs = new List<Song>();
        public static AudioSource Player { get; set; }
        //public static readonly string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/DJH_MusicFiles";
        public static readonly string libraryPath = Path.Combine(Application.dataPath, "Songs");

        public static void StartSong(Song song)
        {
            Player.clip = Resources.Load<AudioClip>(song.GetPath());
            Player.Play();
            //source.Play();
        }

        public static void StopSong()
        {
            Player.Stop();
        }

        public static void AddSongToList(Song s)
        {
            songs.Add(s);
        }

        public static int SetDurationSong(Song song)
        {
            //WindowsMediaPlayer wmp = new WindowsMediaPlayer();
            //string path = song.GetPath();
            //IWMPMedia mediaInformation = wmp.newMedia(path);

            return Player != null ? (int)Player.clip.length : -1;
        }

        public static void PrepareSongs()
        {
            string primaryPath = Path.Combine(Application.dataPath, "Resources/Songs");

            Debug.Log(Application.dataPath);

            if (!Directory.Exists(primaryPath))
            {
                Debug.Log("Brak plikow");
            }
            else
            {
                if (!prepared)
                {
                    DirectoryInfo primaryDirectory = new DirectoryInfo(primaryPath);
                    if (!Directory.Exists(libraryPath))
                    {
                        Directory.CreateDirectory(libraryPath);
                    }
                    string difficultyLevel;
                    foreach (FileInfo fi in primaryDirectory.GetFiles())
                    {
                        if (!File.Exists(Path.Combine(libraryPath, fi.Name)))
                            fi.CopyTo(Path.Combine(libraryPath, fi.Name), true);

                        difficultyLevel = fi.Name.Substring(fi.Name.Length - 5, 1);
                        int result;
                        if(int.TryParse(difficultyLevel,out result))
                        {
                            Song s = new Song(fi.Name);
                            s.duration = SetDurationSong(s);
                            AddSongToList(s);
                        }

                    }
                }
                prepared = true;
            }
        }




        public static List<Song> GetSongList()
        {
            return songs.OrderBy(order => order.getDifficulty().GetOrder()).ToList();
        }
    }
}