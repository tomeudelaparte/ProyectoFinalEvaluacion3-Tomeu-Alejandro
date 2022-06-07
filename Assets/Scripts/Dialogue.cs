using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialogue : MonoBehaviour
{
    // Obtenemos el canvas del dialogo
    public GameObject canvas;

    private void OnTriggerEnter(Collider other)
    {
        // Al entrar al trigger como PLAYER
        if (other.gameObject.CompareTag("Player"))
        {
            // Activa el canvas
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Al salir del trigger como PLAYER
        if (other.gameObject.CompareTag("Player"))
        {
            // Desactiva el canvas
            canvas.SetActive(false);
        }
    }
}
