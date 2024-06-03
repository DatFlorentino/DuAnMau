using System.IO;
using System.Collections.Generic;
using UnityEngine;
using SuDungChoSH;
using DefaultNameSpace;

namespace SuDungChoSM
{
    [System.Serializable]
    public class GameData
    {
        public int score;
        public string timePlayed;
    }

    [System.Serializable]
    public class GameDataPlayed
    {
        public List<GameData> plays;
    }

    public class StorageHelper
    {
        private readonly string filename = "game_data.txt";
        public GameDataPlayed played;

        public void LoadData()
        {
            played = new GameDataPlayed()
            {
                plays = new List<GameData>()
            };
            // doc chuoi tu file
            string dataAsJson = StorageManager.LoadFromFile(filename);
            if (!string.IsNullOrEmpty(dataAsJson))
            {
                try
                {
                    // chuyen chuoi json thanh object
                    played = JsonUtility.FromJson<GameDataPlayed>(dataAsJson);
                }
                catch (System.Exception e)
                {
                    Debug.Log("Error parsing JSON: " + e.Message);
                    // Xử lý lỗi và khởi tạo dữ liệu mặc định
                    played = new GameDataPlayed()
                    {
                        plays = new List<GameData>()
                    };
                }
            }
        }

        public void SaveData()
        {
            // chuyen object thanh chuoi json
            string dataAsJson = JsonUtility.ToJson(played);
            // luu chuoi json vao file
            StorageManager.SaveToFile(filename, dataAsJson);
        }
    }
}
