using UnityEngine;

public class Breakable_items : MonoBehaviour
{
    public ParticleSystem potParticleSystem;
    public AudioClip potbreak;
    public AudioSource PotAudiosource;
    // Start is called before the first frame update
    void Start()
    {
        PotAudiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider otherCollider)
    {
      
        //Adios pot
        
        //explosionParticleSystem.Play();
        Vector3 offset = new Vector3(0, 0f, 0);
        var potBreak = Instantiate(potParticleSystem,
            transform.position + offset,
            potParticleSystem.transform.rotation);
        potBreak.Play();

        PotAudiosource.PlayOneShot(potbreak);

        Destroy(gameObject);
    }
}
