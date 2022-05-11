using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncepad : MonoBehaviour
{
    public float power = 20f;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
        playerRigidbody.AddForce(Vector3.up * power, ForceMode.Impulse);
    }
}
