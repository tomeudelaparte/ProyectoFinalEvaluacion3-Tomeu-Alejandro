using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Carga la escena del juego
    public void Play(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Quita la aplicacion
    public void Quit()
    {
        // Si estamos en el editor
        #if UNITY_EDITOR
                // Salimos del editor
                UnityEditor.EditorApplication.isPlaying = false;
        #endif

        // Cierra la aplicacion
        Application.Quit();
    }
}
