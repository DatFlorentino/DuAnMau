using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip CoinPickup;
    [SerializeField] int CoinValue = 50; // Giá trị điểm của mỗi coin

    private bool isCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            // Cộng điểm và tính số lần nhặt coin
            GameController gameController = FindObjectOfType<GameController>();
            gameController.CollectCoin(CoinValue);
            AudioSource.PlayClipAtPoint(CoinPickup, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
