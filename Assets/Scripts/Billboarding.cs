using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    private GameObject focalPoint;

    void Start()
    {
        focalPoint = GameObject.Find("FocalPoint");
    }

    void Update()
    {
        transform.LookAt(focalPoint.transform);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}
