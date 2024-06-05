using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip CoinPickup;
    [SerializeField] int CoinValue = 50; // Giá trị điểm của mỗi coin

    private bool isCollected = false;
    private string coinID;

    private void Start()
    {
        coinID = gameObject.name + transform.position.ToString(); // Tạo ID duy nhất cho coin dựa trên tên và vị trí
        GameController gameController = FindObjectOfType<GameController>();
        if (gameController.IsCoinCollected(coinID))
        {
            Destroy(gameObject); // Hủy đối tượng nếu nó đã bị thu thập
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            // Cộng điểm và tính số lần nhặt coin
            GameController gameController = FindObjectOfType<GameController>();
            gameController.CollectCoin(CoinValue, coinID);
            AudioSource.PlayClipAtPoint(CoinPickup, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
