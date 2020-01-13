using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float TimeForEndGame = 300f;
    public Text TimeText;
    public GameObject EndGameMenu;
    public Image imageWinPlayer;

    public Sprite[] sprites;
    public PlayerController[] players;
    private bool isGameEnd;

    public void Start()
    {
        EndGameMenu.SetActive(false);
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (!isGameEnd)
        {
            TimeForEndGame -= Time.deltaTime;
            int minutes = (int)(TimeForEndGame / 60f);
            TimeText.text = minutes + ":" + (int)(TimeForEndGame % 60);
            isGameEnd = TimeForEndGame <= 0;
        }
        else
        {
            EndGame();
        }

    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void EndGame()
    {
        EndGameMenu.SetActive(true);
        Time.timeScale = 0.1f;
        for (int i = 0; i < players.Length; i++)
        {
            players[i].isDead = true;
        }
        int maxScore = -1000;
        int index = 0;
        for (int i = 0; i < players.Length; i++)
        {
            if(players[i].score > maxScore)
            {
                maxScore = players[i].score;
                index = i;
            }
        }
        imageWinPlayer.sprite = sprites[index];
    }
}
