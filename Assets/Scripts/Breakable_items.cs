using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_items : MonoBehaviour
{
    public ParticleSystem potParticleSystem;
    public AudioClip potbreak;
    public AudioSource PotAudiosource;

    private bool isDestroyed = false;

    void Start()
    {
        PotAudiosource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider otherCollider)
    {

        //Adios pot

        if (!isDestroyed)
        {
            isDestroyed = true;

            Vector3 offset = new Vector3(0, 0f, 0);
            var potBreak = Instantiate(potParticleSystem,
                transform.position + offset,
                potParticleSystem.transform.rotation);

            potBreak.Play();

            PotAudiosource.PlayOneShot(potbreak);

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
