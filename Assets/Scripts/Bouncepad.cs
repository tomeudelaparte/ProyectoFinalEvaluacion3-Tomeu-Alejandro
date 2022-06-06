using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncepad : MonoBehaviour
{
    private Animator animator;
    private AudioSource bounceAudioSource;

    public float power = 20f;
    public AudioClip bouncesound;

    private void Start()
    {
        animator = GetComponent<Animator>();

        bounceAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetTrigger("Playertouch");

        Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
        playerRigidbody.AddForce(Vector3.up * power, ForceMode.Impulse);

        bounceAudioSource.PlayOneShot(bouncesound, 0.7f);
    }
}
