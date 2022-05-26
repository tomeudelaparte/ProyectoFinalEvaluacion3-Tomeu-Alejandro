using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncepad : MonoBehaviour
{
    public float power = 20f;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        
    }

    
    private void OnTriggerEnter(Collider other)
    {
        animator.SetTrigger("Playertouch");
        Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
        playerRigidbody.AddForce(Vector3.up * power, ForceMode.Impulse);

        
    }
}
