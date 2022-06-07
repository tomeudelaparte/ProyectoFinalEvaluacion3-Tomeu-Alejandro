using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    [Header("GAME COMPLETE")]
    public TextMeshProUGUI currentTime;
    public TextMeshProUGUI bestTime;

    [Header("EXTRAS")]
    private DataPersistence dataPersistence;

    void Start()
    {
        dataPersistence = FindObjectOfType<DataPersistence>();

        // Obtiene el mejor tiempo y el actual
        currentTime.text = dataPersistence.GetString("Current Time");
        bestTime.text = dataPersistence.GetString("Best Time");

        // Desbloquea el raton
        Cursor.lockState = CursorLockMode.None;
    }
}
