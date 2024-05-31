
namespace DefaultNamespace
{
    using DefaultNameSpace;
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;
    using UnityEngine.Playables;

    public class StorageHelper
    {
        private readonly string filename = "game_data.txt";
        public GameDataPlayed played;
        public void LoadData()
        {
            played = new GameDataPlayed();
            {
                plays = new List<GameData>();
            };
            string dataAsJson = StorageManager.LoadFromFile(filename);
            if (dataAsJson != null)
            {
                // chuyen chuoi json thanh object
                played = JsonUtility.FromJson<GameDataPlayed>(dataAsJson);
            }
        }
        public void SaveData()
        {
            //chuyen object thanh chuoi json
            string dataAsJson = JsonUtility.ToJson(played);
            //LUU chuoi json vao file
            StorageManager.SaveToFile(filename, dataAsJson);
        }
    }
}
