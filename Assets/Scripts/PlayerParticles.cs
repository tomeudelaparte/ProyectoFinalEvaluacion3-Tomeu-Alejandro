using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    public GameObject player;
    public GameObject focalPoint;

    void Update()
    {
        // Sigue la posicion del player
        gameObject.transform.position = player.transform.position;

        // Mira en direccion al player
        transform.LookAt(focalPoint.transform.position);
    }
}
