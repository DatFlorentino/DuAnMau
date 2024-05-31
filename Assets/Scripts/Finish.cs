using DefaultNameSpace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] GameObject informationCanvas;
    [SerializeField] GameObject finishCanvas;
    

    private StorageHelper1 storageHelper;
    private GameDataPlayed played;

    [SerializeField] GameObject row;

    private void Start()
    {
        storageHelper = new StorageHelper1();
        storageHelper.LoadData();
        played = storageHelper.played;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            informationCanvas.SetActive(false);
            finishCanvas.SetActive(true);
            // luu thanh tich cua nguoi choi
            var score = FindObjectOfType<GameController>().GetScore();
            var gameData = new GameData()
            {
                score = score,
                timePlayed = DateTime.Now.ToString("yyyy-MM-dd")
            };
            played.plays.Add(gameData);
            storageHelper.SaveData();
            // tai du lieu trong file hien thi bang thanh tich
            storageHelper.LoadData();
            played = storageHelper.played;
            Debug.Log("Count: " + played.plays.Count);
            //Sap xep giam dan theo diem
            //lay top 5
            played.plays.Sort((x, y) => y.score.CompareTo(x.score));
            var plays = played.plays.GetRange(0, Math.Min(5, played.plays.Count));
            //hien thi len giao dien
            for (int i = 0; i < played.plays.Count; i++)
            {
                var rowInstance = Instantiate(row, row.transform.parent);
                rowInstance.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = (i + 1).ToString();
                rowInstance.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = played.plays[i].score.ToString();
                rowInstance.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = played.plays[i].timePlayed;
                rowInstance.SetActive(true);
            }
            //Hien thi giao dien ket thuc 
            finishCanvas.SetActive(true);
        }
    }
}
