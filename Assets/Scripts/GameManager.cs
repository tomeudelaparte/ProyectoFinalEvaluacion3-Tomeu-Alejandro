using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("PLAYER")]
    public GameObject player;

    [Header("SPAWN POSITIONS")]
    public GameObject[] spawnPositions;

    [Header("USER INTERFACE")]
    public TextMeshProUGUI totalPilas;
    public TextMeshProUGUI collectedPilas;
    public TextMeshProUGUI timestamp;
    public Slider sliderUI;

    [Header("PANELS")]
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject userInterface;

    [Header("AUDIO SYSTEM")]
    public GameObject audioSystem;

    [Header("EXTRAS")]
    private AudioSource[] audioSource;
    private DataPersistence dataPersistence;

    private bool isGameOver = false;
    private bool isPaused = false;

    private int totalItems = 0;
    private int itemsCollected = 0;

    private float time, minutes, seconds, miliseconds;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Physics.gravity = new Vector3(0, -39.24f, 0);

        dataPersistence = FindObjectOfType<DataPersistence>();
        audioSource = audioSystem.GetComponents<AudioSource>();
        totalItems = GameObject.FindGameObjectsWithTag("Item").Length;

        totalPilas.text = totalItems.ToString();
        collectedPilas.text = itemsCollected.ToString();

        playerSpawn();
    }

    void Update()
    {
        UpdateTimer();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void playerSpawn()
    {
        bool skipTutorial = dataPersistence.GetBool("TUTORIAL SKIP");

        if (!skipTutorial)
        {
            player.gameObject.transform.rotation = spawnPositions[0].transform.rotation;
            player.gameObject.transform.position = spawnPositions[0].transform.position;
        }
        else
        {
            player.gameObject.transform.rotation = spawnPositions[1].transform.rotation;
            player.gameObject.transform.position = spawnPositions[1].transform.position;
        }
    }

    public void Pause()
    {
        if (!isPaused)
        {
            isPaused = true;

            Time.timeScale = 0;

            userInterface.SetActive(false);
            pauseMenu.SetActive(true);

            audioSource[0].Pause();
            audioSource[1].Play();

            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            isPaused = false;

            Time.timeScale = 1;

            audioSource[0].Play();
            audioSource[1].Pause();

            userInterface.SetActive(true);
            pauseMenu.SetActive(false);
            optionsMenu.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void GameOver()
    {
        isGameOver = true;

        string currentTime = timestamp.text;

        if (!dataPersistence.HasKey("Best Time"))
        {
            dataPersistence.SetString("Best Time", "99:99.09");
        }

        string bestTime = dataPersistence.GetString("Best Time");

        dataPersistence.SetString("Current Time", currentTime);

        if (checkBestTime(currentTime, bestTime))
        {
            dataPersistence.SetString("Best Time", currentTime);
        }

        SceneManager.LoadScene("GameOver");
    }

    public void UpdateScore()
    {
        itemsCollected++;

        collectedPilas.text = itemsCollected.ToString();

        if (itemsCollected >= totalItems)
        {
            GameOver();
        }
    }

    private void UpdateTimer()
    {
        if (!isGameOver)
        {
            time += Time.deltaTime;
            miliseconds = (int)((time - (int)time) * 100);
            seconds = (int)(time % 60);
            minutes = (int)(time / 60 % 60);

            timestamp.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, miliseconds);
        }
    }

    private bool checkBestTime(string currentTime, string bestTime)
    {
        string pattern = "([0-9][0-9]):([0-9][0-9]).([0-9][0-9])";

        string[] currentTimeSplit = Regex.Split(currentTime, pattern);
        string[] bestTimeSplit = Regex.Split(bestTime, pattern);

        float currentMinutes = float.Parse(currentTimeSplit[1]);
        float bestMinutes = float.Parse(bestTimeSplit[1]);

        float currentSeconds = float.Parse(currentTimeSplit[2]);
        float bestSeconds = float.Parse(bestTimeSplit[2]);

        float currentMilisec = float.Parse(currentTimeSplit[3]);
        float bestMilisec = float.Parse(bestTimeSplit[3]);

        if (currentMinutes < bestMinutes)
        {
            return true;
        }

        if (currentMinutes == bestMinutes)
        {
            if (currentSeconds < bestSeconds)
            {
                return true;
            }

            if (currentSeconds == bestSeconds && currentMilisec <= bestMilisec)
            {
                return true;
            }

            return false;
        }

        return false;
    }

    public void SetSpindash(float spin)
    {
        sliderUI.value = spin;
    }
    public void ResetSpindash(float spin)
    {
        sliderUI.value = 0;
    }
}
