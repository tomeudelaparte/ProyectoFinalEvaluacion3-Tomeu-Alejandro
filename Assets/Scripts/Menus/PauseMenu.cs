using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private GameManager gameManagerScript;

    void Start()
    {
        // Obtiene la componente necesaria
        gameManagerScript = FindObjectOfType<GameManager>();
    }

    // Desactiva la pausa del juego
    public void Resume()
    {
        gameManagerScript.Pause();
    }


    public void ExitToMenu(string sceneName)
    {
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene(sceneName);
    }
}
