using SuDungChoSM;
using SuDungChoSH;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNameSpace;
using GameDataSM = SuDungChoSM.GameData;
using GameDataPlayedSM = SuDungChoSM.GameDataPlayed;

namespace Finish
{
    public class Finish : MonoBehaviour
    {
        [SerializeField] GameObject informationCanvas;
        [SerializeField] GameObject finishCanvas;

        private StorageHelper storageHelper;
        private GameDataPlayedSM played; // Sử dụng alias

        [SerializeField] GameObject row;

        private void Start()
        {
            storageHelper = new StorageHelper();
            storageHelper.LoadData();
            played = storageHelper.played;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                informationCanvas.SetActive(false);
                finishCanvas.SetActive(true);
                // lưu thành tích của người chơi
                var score = FindObjectOfType<GameController>().GetScore();
                var gameData = new GameDataSM() // Sử dụng alias
                {
                    score = score,
                    timePlayed = DateTime.Now.ToString("yyyy-MM-dd")
                };
                played.plays.Add(gameData);
                storageHelper.SaveData();
                // tải dữ liệu từ file hiển thị bảng thành tích
                storageHelper.LoadData();
                played = storageHelper.played;
                Debug.Log("Count: " + played.plays.Count);
                // Sắp xếp giảm dần theo điểm
                // lấy top 5
                played.plays.Sort((x, y) => y.score.CompareTo(x.score));
                var plays = played.plays.GetRange(0, Math.Min(5, played.plays.Count));
                // hiển thị lên giao diện
                foreach (Transform child in row.transform.parent)
                {
                    if (child != row.transform)
                    {
                        Destroy(child.gameObject);
                    }
                }
                for (int i = 0; i < plays.Count; i++)
                {
                    var rowInstance = Instantiate(row, row.transform.parent);
                    rowInstance.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = (i + 1).ToString();
                    rowInstance.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = plays[i].score.ToString();
                    rowInstance.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = plays[i].timePlayed;
                    rowInstance.SetActive(true);
                }
                // Hiển thị giao diện kết thúc 
                finishCanvas.SetActive(true);
            }
        }
    }
}
