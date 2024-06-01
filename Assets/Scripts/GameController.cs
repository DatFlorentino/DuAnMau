using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] int score = 0;
    [SerializeField] int live = 3;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI liveText;

    [SerializeField] int coinsToCollect = 20;
    private int currentCoins = 0;
    [SerializeField] int rewardPoints = 100;
    [SerializeField] TextMeshProUGUI coinText; // UI Text hiển thị số lần nhặt coin

    private void Awake()
    {
        var numGameSessions = FindObjectsOfType<GameController>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        liveText.text = live.ToString();
        scoreText.text = score.ToString();
        UpdateCoinText();
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString();
    }

    private void DecreaseLive()
    {
        live--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        liveText.text = live.ToString();
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void ProcessPlayerDeath()
    {
        if (live > 1)
        {
            DecreaseLive();
        }
        else
        {
            ResetGame();
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void CollectCoin(int coinValue)
    {
        AddScore(coinValue); // Cộng điểm từ giá trị của coin
        currentCoins += 1; // Tăng số lần nhặt coin lên 1
        UpdateCoinText();

        if (currentCoins >= coinsToCollect)
        {
            RewardPlayer();
            currentCoins = 0; // Reset số lần nhặt coin
            UpdateCoinText(); // Cập nhật UI sau khi reset
        }
    }

    private void RewardPlayer()
    {
        AddScore(rewardPoints); // Thưởng điểm cho người chơi
        Debug.Log("Player rewarded with " + rewardPoints + " points for collecting " + coinsToCollect + " coins.");
    }

    private void UpdateCoinText()
    {
        if (coinText != null)  // Kiểm tra xem coinText có được gán chưa
        {
            coinText.text = "Nhiệm vụ: \n Thu Thập Coins: " + currentCoins + "/" + coinsToCollect;
        }
        else
        {
            Debug.LogError("Coin Text UI is not assigned in the GameController script.");
        }
    }
}
