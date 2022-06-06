using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_items : MonoBehaviour
{
    private AudioSource potAudioSource;
    private bool isDestroyed = false;

    public ParticleSystem potParticleSystem;
    public AudioClip potbreak;

    void Start()
    {
        potAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (!isDestroyed)
        {
            isDestroyed = true;

            ParticleSystem potBreak = Instantiate(potParticleSystem, transform.position, potParticleSystem.transform.rotation);

            potBreak.Play();

            potAudioSource.PlayOneShot(potbreak);

            StartCoroutine(DestroyEvent());
        }
    }

    private IEnumerator DestroyEvent()
    {
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);

        yield return new WaitForSeconds(3f);

        Destroy(gameObject);
    }
}
