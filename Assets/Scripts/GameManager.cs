using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timestamp, collectedPilas, totalPilas;

    private int totalItems;
    private int itemsCollected;

    private float startTime;
    private float time, minutes, seconds, miliseconds;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        itemsCollected = 0;

        totalItems = GameObject.FindGameObjectsWithTag("Item").Length;
        Debug.Log($"Items en total: {totalItems} ya sabes que haceh");

        totalPilas.text = totalItems.ToString();
        collectedPilas.text = itemsCollected.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    public void UpdateScore()
    {
        itemsCollected++;
        Debug.Log($"Tienes {itemsCollected} de {totalItems} guapeton");

        collectedPilas.text = itemsCollected.ToString();
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
