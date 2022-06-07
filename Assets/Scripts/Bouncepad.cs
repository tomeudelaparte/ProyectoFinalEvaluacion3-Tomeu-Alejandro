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
        // Obtiene las componentes necesarias
        animator = GetComponent<Animator>();
        bounceAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Activa la animacion del muelle
        animator.SetTrigger("Playertouch");

        // Obtiene el rigidbody del GameObject
        Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();

        // Aplica una fuerza vertical hacia arriba
        playerRigidbody.AddForce(Vector3.up * power, ForceMode.Impulse);

        // Reproduce un sonido predeterminado
        bounceAudioSource.PlayOneShot(bouncesound, 0.7f);
    }
}
