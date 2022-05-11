using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    private GameObject focalPoint;
    private bool useStaticBillboard;

    void Start()
    {
        focalPoint = GameObject.Find("FocalPoint");
    }

    void LateUpdate()
    {
        if (!useStaticBillboard)
        {
            transform.LookAt(focalPoint.transform);
        }
        else
        {
            transform.rotation = focalPoint.transform.rotation;
        }

        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}
