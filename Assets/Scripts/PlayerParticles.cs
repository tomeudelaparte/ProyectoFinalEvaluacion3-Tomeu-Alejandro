using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    private GameObject player;
    private GameObject camera;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.Find("FocalPoint");
    }

    void Update()
    {
        gameObject.transform.position = player.transform.position;

        transform.LookAt(camera.transform.position);
        //transform.localEulerAngles = new Vector3(transform.transform.localEulerAngles.x, transform.transform.localEulerAngles.y, 0);
    }
}
