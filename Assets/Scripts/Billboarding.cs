using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    private GameObject focalPoint;

    void Start()
    {
        // Obtiene el FocalPoint(Camara)
        focalPoint = GameObject.Find("FocalPoint");
    }

    void Update()
    {
        // Rota en direccion al FocalPoint(Camara) en Y
        transform.LookAt(focalPoint.transform);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}
