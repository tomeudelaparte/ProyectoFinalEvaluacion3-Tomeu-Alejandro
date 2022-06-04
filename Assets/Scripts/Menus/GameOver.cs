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

        currentTime.text = dataPersistence.GetString("Current Time");
        bestTime.text = dataPersistence.GetString("Best Time");

        Cursor.lockState = CursorLockMode.None;
    }
}
