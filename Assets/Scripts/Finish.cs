using SuDungChoSM;
using SuDungChoSH;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinishNamespace
{
    public class Finish : MonoBehaviour
    {
        [SerializeField] GameObject informationCanvas;
        [SerializeField] GameObject finishCanvas;

        private StorageHelper storageHelper;
        private GameDataPlayed played;

        [SerializeField] GameObject row;

        private void Awake()
        {
            // Đảm bảo rằng Finish không bị phá hủy khi tải lại cảnh
            DontDestroyOnLoad(gameObject);
            
        }

        private void Start()
        {
            storageHelper = new StorageHelper();
            storageHelper.LoadData();
            played = storageHelper.played;
            Debug.Log("Game started. Data loaded.");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player finished level.");
                informationCanvas.SetActive(false);
                finishCanvas.SetActive(true);

                var score = FindObjectOfType<GameController>().GetScore();
                Debug.Log("Player score: " + score);

                var gameData = new GameData()
                {
                    score = score,
                    timePlayed = DateTime.Now.ToString("yyyy-MM-dd")
                };
                played.plays.Add(gameData);
                storageHelper.SaveData();

                storageHelper.LoadData();
                played = storageHelper.played;
                Debug.Log("Data saved and reloaded. Total plays: " + played.plays.Count);

                played.plays.Sort((x, y) => y.score.CompareTo(x.score));
                var plays = played.plays.GetRange(0, Math.Min(5, played.plays.Count));

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

                finishCanvas.SetActive(true);
            }
        }
    }
}
