using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    private GameObject theCam;
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        theCam = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(theCam.transform);

        transform.position = (Player.transform.position + Vector3.up * 2.5f);
    }
}
