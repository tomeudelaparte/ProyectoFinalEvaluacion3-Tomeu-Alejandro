using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprite : MonoBehaviour
{
    private GameObject theCam;
    // Start is called before the first frame update
    void Start()
    {
        theCam = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(theCam.transform);
        
    }
}
