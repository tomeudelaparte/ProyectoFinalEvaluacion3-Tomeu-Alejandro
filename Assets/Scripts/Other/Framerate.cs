using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Framerate : MonoBehaviour
{
    private float deltaTime, fps;

    // Muestra el n�mero de frames en tiempo real
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        fps = 1.0f / deltaTime;

        // Mathf.Ceil - Devuelve el entero mayor o igual m�s pr�ximo a un n�mero dado.
        gameObject.GetComponent<TextMeshProUGUI>().text = Mathf.Clamp(Mathf.Ceil(fps), 0, 200).ToString();
    }
}
