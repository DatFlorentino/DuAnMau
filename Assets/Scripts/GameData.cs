using System;
using System.Collections.Generic;

namespace DefaultNameSpace
{
    //Luu tru thong tin game
    public class GameData
    {
        public int score = 0;
        public string timePlayed;
    }

    [Serializable]
    public class GameDataPlayed
    {
        public List<GameData> plays;
    }
}