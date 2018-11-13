using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Managers
{
    public static class GameManager
    {
        public static GameScript GameScript { get;set; }
        public static dj_hero.MatchOption Options { get; set; }
        public static dj_hero.Song Song { get; set; }
        public static int Score { get; set; }
    }
}
