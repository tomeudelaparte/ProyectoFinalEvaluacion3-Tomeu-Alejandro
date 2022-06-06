using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        // Si estamos haciendo pruebas en el editor
        #if UNITY_EDITOR
                // Salimos del editor
                UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }
}
