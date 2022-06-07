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
    public GameObject map;

    [Header("AUDIO SYSTEM")]
    public GameObject audioSystem;

    [Header("EXTRAS")]
    private AudioSource[] audioSource;
    private DataPersistence dataPersistence;

    [Header("GAME MANAGER")]
    private int totalItems = 0;
    private int itemsCollected = 0;

    private bool isGameOver = false;
    private bool isPaused = false;

    private float time, minutes, seconds, miliseconds;

    void Start()
    {
        // Bloquea el uso del raton
        Cursor.lockState = CursorLockMode.Locked;

        // Cambia la gravedad a una determinada
        Physics.gravity = new Vector3(0, -39.24f, 0);

        // Componentes necesarias
        dataPersistence = FindObjectOfType<DataPersistence>();
        audioSource = audioSystem.GetComponents<AudioSource>();
        totalItems = GameObject.FindGameObjectsWithTag("Item").Length;

        // Actualizamos el total de monedas y las monedas recogidas
        totalPilas.text = totalItems.ToString();
        collectedPilas.text = itemsCollected.ToString();

        // Activa la interfaz de usuario y desactiva el menu de pausa
        userInterface.SetActive(true);
        pauseMenu.SetActive(false);

        // Selecciona el lugar de spawn del player
        PlayerSpawn();
    }

    void Update()
    {
        // Actualiza el cronometro
        UpdateTimer();

        // Si presionas la tecla ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Pausa el juego
            Pause();
        }
    }

    private void PlayerSpawn()
    {
        // Obtiene el spawn del player guardado seleccionado
        bool skipTutorial = dataPersistence.GetBool("TUTORIAL SKIP");

        // Si NO se salta el tutorial
        if (!skipTutorial)
        {
            // Aparece al principio del tutorial
            player.gameObject.transform.rotation = spawnPositions[0].transform.rotation;
            player.gameObject.transform.position = spawnPositions[0].transform.position;
        }
        else
        {
            // Aparece al final de todo del tutorial
            player.gameObject.transform.rotation = spawnPositions[1].transform.rotation;
            player.gameObject.transform.position = spawnPositions[1].transform.position;
        }
    }

    // Pausa el juego
    public void Pause()
    {
        // Si NO esta pausado
        if (!isPaused)
        {
            // PAUSED toma valor TRUE
            isPaused = true;

            // Pausa el tiempo
            Time.timeScale = 0;

            // Desactiva la interfaz de usuario
            userInterface.SetActive(false);

            // Activa el menu de pausa
            pauseMenu.SetActive(true);

            // Para la musica del juego y reproduce la musica de pausa
            audioSource[0].Pause();
            audioSource[1].Play();

            // Desbloquea el uso del raton
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            // PAUSED toma valor FALSE
            isPaused = false;

            // Reanuda el tiempo
            Time.timeScale = 1;

            // Activa la interfaz de usuario
            userInterface.SetActive(true);

            // Desactiva el menu de pausa
            pauseMenu.SetActive(false);

            // Desactiva el menu de opciones
            optionsMenu.SetActive(false);

            // Desactiva el menu del mapa
            map.SetActive(false);

            // Reproduce la musica del juego y para la musica de pausa
            audioSource[0].Play();
            audioSource[1].Pause();

            // Bloquea el uso del raton
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Termina el juego
    private void GameOver()
    {
        // GameOver toma valor TRUE
        isGameOver = true;

        // Guarda el tiempo actual
        string currentTime = timestamp.text;

        // Si NO existe l mejor tiempo guardado
        if (!dataPersistence.HasKey("Best Time"))
        {
            // Guarda un dato predeterminado
            dataPersistence.SetString("Best Time", "99:99.09");
        }

        // Obtiene el mejor tiempo guardado
        string bestTime = dataPersistence.GetString("Best Time");

        // Guarda el tiempo actual
        dataPersistence.SetString("Current Time", currentTime);

        // Si el tiempo actual es mejor que el anterior mejor tiempo
        if (checkBestTime(currentTime, bestTime))
        {
            // Guarda el tiempo actual como mejor tiempo
            dataPersistence.SetString("Best Time", currentTime);
        }

        // Carga la escena
        SceneManager.LoadScene("GameOver");
    }

    // Actualiza el total de monedas recogidas
    public void UpdateScore()
    {
        // Suma +1 al total de monedas recogidas
        itemsCollected++;

        // Actualiza el texto de las monedas recogidas
        collectedPilas.text = itemsCollected.ToString();

        // Si las monedas recogidas es mayor o igual al total de monedas
        if (itemsCollected >= totalItems)
        {
            // Termina el juego
            GameOver();
        }
    }

    private void UpdateTimer()
    {
        // Si GameOver es FALSE
        if (!isGameOver)
        {
            // Guarda y suma deltatime
            time += Time.deltaTime;

            // Convierte time en minutos, segundos y milisegundos
            miliseconds = (int)((time - (int)time) * 100);
            seconds = (int)(time % 60);
            minutes = (int)(time / 60 % 60);

            // Cambia el texto con los valores formateados a un solo String
            timestamp.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, miliseconds);
        }
    }

    private bool checkBestTime(string currentTime, string bestTime)
    {
        // Patron del Regex
        string pattern = "([0-9][0-9]):([0-9][0-9]).([0-9][0-9])";

        // Guarda y divide el string en grupos con el pattern
        string[] currentTimeSplit = Regex.Split(currentTime, pattern);
        string[] bestTimeSplit = Regex.Split(bestTime, pattern);

        // Parsea el Grupo 01 de String a Float
        float currentMinutes = float.Parse(currentTimeSplit[1]);
        float bestMinutes = float.Parse(bestTimeSplit[1]);

        // Parsea el Grupo 02 de String a Float
        float currentSeconds = float.Parse(currentTimeSplit[2]);
        float bestSeconds = float.Parse(bestTimeSplit[2]);

        // Parsea el Grupo 03 de String a Float
        float currentMilisec = float.Parse(currentTimeSplit[3]);
        float bestMilisec = float.Parse(bestTimeSplit[3]);

        // Si currentMinutes es menor a bestMinutes
        if (currentMinutes < bestMinutes)
        {
            return true;
        }

        // Si currentMinutes es igual a bestMinutes
        if (currentMinutes == bestMinutes)
        {
            // Si currentSeconds es menor a bestSeconds
            if (currentSeconds < bestSeconds)
            {
                return true;
            }

            // Si currentSeconds es igual a bestSeconds y currentMilisec menor o igual a bestMilisec
            if (currentSeconds == bestSeconds && currentMilisec <= bestMilisec)
            {
                return true;
            }

            return false;
        }

        return false;
    }

    // Actualiza la barra de Spindash
    public void SetSpindash(float spin)
    {
        sliderUI.value = spin;
    }

    // Resetea a 0 la barra de Spindash
    public void ResetSpindash(float spin)
    {
        sliderUI.value = 0;
    }
}
