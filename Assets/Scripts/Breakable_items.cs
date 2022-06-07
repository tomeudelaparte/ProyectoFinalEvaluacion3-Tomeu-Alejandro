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
        // Obtiene la componente necesaria
        potAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        // Si NO esta roto
        if (!isDestroyed)
        {
            // Destroyed toma valor TRUE
            isDestroyed = true;

            // Instancia el sistema de particulas de destruccion
            Instantiate(potParticleSystem, transform.position, potParticleSystem.transform.rotation);

            // Reproduce un sonido predeterminado
            potAudioSource.PlayOneShot(potbreak);

            // Empieza una COROUTINE
            StartCoroutine(DestroyEvent());
        }
    }

    // Destruye el GameObject con retardo
    private IEnumerator DestroyEvent()
    {
        // Cambia la transpariencia del color de la componente SpriteRenderer
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);

        // Espera 3 segundos
        yield return new WaitForSeconds(3f);

        // Destruye el GameObject
        Destroy(gameObject);
    }
}
