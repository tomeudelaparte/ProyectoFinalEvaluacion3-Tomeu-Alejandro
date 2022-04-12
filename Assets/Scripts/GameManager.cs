using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int totalItems;
    private int itemsCollected;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        

        itemsCollected = 0;

        totalItems = GameObject.FindGameObjectsWithTag("Item").Length;
        Debug.Log($"Items en total: {totalItems} ya sabes que haceh");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateScore()
    {
        itemsCollected++;
        Debug.Log($"Tienes {itemsCollected} de {totalItems} guapeton");
    }
}
