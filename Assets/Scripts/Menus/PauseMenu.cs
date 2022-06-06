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
