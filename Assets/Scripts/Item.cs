using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        // Obtiene la componente necesaria
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        // Si al entrar como PLAYER
        if (otherCollider.CompareTag("Player"))
        {
            // Actualiza el contador de monedas
            gameManager.UpdateScore();

            // Destruye la moneda
            Destroy(gameObject);
        }
    }
}
