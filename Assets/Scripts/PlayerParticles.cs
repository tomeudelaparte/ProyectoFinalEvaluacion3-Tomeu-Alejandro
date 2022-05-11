using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    public GameObject player;
    public GameObject focalPoint;

    void Update()
    {
        gameObject.transform.position = player.transform.position;

        transform.LookAt(focalPoint.transform.position);
    }
}
