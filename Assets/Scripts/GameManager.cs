using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timestamp, collectedPilas, totalPilas;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;


    private bool isGameOver = false;
    private bool isPaused = false;

    private int totalItems;
    private int itemsCollected;

    private float time, minutes, seconds, miliseconds;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        itemsCollected = 0;

        totalItems = GameObject.FindGameObjectsWithTag("Item").Length;
        Debug.Log($"ITEMS EN TOTAL: {totalItems}");

        totalPilas.text = totalItems.ToString();
        collectedPilas.text = itemsCollected.ToString();

        Physics.gravity = new Vector3(0, -39.24f, 0);
    }

    void Update()
    {
        UpdateTimer();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (!isPaused && !isGameOver)
        {
            isPaused = true;

            pauseMenu.SetActive(true);

            Cursor.lockState = CursorLockMode.None;

            Time.timeScale = 0;

        }
        else if(!isGameOver)
        {
            isPaused = false;

            pauseMenu.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;

            Time.timeScale = 1;
        }
    }

    private void GameOver()
    {
        isGameOver = true;

        gameOverMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0;
    }

    public void UpdateScore()
    {
        itemsCollected++;
        Debug.Log($"Tienes {itemsCollected} de {totalItems}");

        collectedPilas.text = itemsCollected.ToString();

        if (itemsCollected >= totalItems)
        {
            GameOver();
        }
    }

    private void UpdateTimer()
    {
        time += Time.deltaTime;
        miliseconds = (int)((time - (int)time) * 100);
        seconds = (int)(time % 60);
        minutes = (int)(time / 60 % 60);

        timestamp.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, miliseconds);
    }
}
