using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncepad : MonoBehaviour
{
    public float power = 20f;
    private Animator animator;

    public AudioSource BounceAudioSource;
    public AudioClip bouncesound;

    private void Start()
    {
        animator = GetComponent<Animator>();

        BounceAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetTrigger("Playertouch");
        Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
        playerRigidbody.AddForce(Vector3.up * power, ForceMode.Impulse);

        BounceAudioSource.PlayOneShot(bouncesound, 0.7f);
    }
}
