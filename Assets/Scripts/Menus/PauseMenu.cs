using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private GameManager gameManagerScript;

    void Start()
    {
        gameManagerScript = FindObjectOfType<GameManager>();
    }

    // Desactiva la pausa del juego
    public void Resume()
    {
        gameManagerScript.Pause();
    }


    // Carga la escena Main menu
    public void ExitToMenu(string sceneName)
    {
        // Reanuda el tiempo
        Time.timeScale = 1;

        // Desbloquea el raton
        Cursor.lockState = CursorLockMode.None;

        // Carga la escena
        SceneManager.LoadScene(sceneName);
    }
}
