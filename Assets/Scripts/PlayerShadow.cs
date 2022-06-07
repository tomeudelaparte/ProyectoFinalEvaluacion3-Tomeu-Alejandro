using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    public GameObject player;

    private Vector3 hitDataPoint;
    private float groundDistance;

    void Update()
    {
        // Raycast en direccion hacia abajo
        if (Physics.Raycast(player.transform.position, Vector3.down, out RaycastHit hitData, 50))
        {
            // Guarda el hit del RayCast
            hitDataPoint = hitData.point;

            // Modifica el axis vertical de este Vector3
            hitDataPoint.y += 0.1f;

            // Sigue la posicion del hit
            transform.position = hitDataPoint;

            // Toma la altura entre la sombra y el player
            groundDistance = transform.position.y - player.transform.position.y;

            // Escala la sombra segun la distancia con el suelo
            transform.localScale = Vector3.one * -groundDistance;
        }
    }
}
