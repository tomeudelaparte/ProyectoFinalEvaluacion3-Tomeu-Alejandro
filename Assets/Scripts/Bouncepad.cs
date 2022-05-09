using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncepad : MonoBehaviour
{
    public float power = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
        playerRigidbody.AddForce(Vector3.up * power, ForceMode.Impulse);
    }
}
