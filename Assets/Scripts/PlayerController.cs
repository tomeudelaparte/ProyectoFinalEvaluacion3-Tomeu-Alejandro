using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("GROUND DETECTION")]
    public LayerMask groundLayerMask;

    [Header("VALUES")]
    public float speed = 10f;
    public float impulse = 5F;
    public float groundDistance = 2f;
    public float speedRotation = 10f;
    public float velocityMax = 100f;

    [Header("PARTICLES")]
    public ParticleSystem spindashParticleSystem;
    
    private float horizontalInput, verticalInput;
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    private PlayerSprite playerSprite;
    private GameObject focalPoint;

    [Header("AUDIO")]
    public AudioSource playerAudioSource;
    public AudioClip[] JumpVoices;
    public AudioClip[] SpindashVoices;

    [Header("SPINDASH")]
    private float spindashVelocity = 0;
    private bool isSpindashing = false;
    private Coroutine spindashCoroutine = null;
    private GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerSprite = FindObjectOfType<PlayerSprite>();

        spindashParticleSystem.Stop();

        focalPoint = GameObject.Find("FocalPoint");

        playerSprite.IdleSprite();
        playerAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (IsOnGround() && !isSpindashing)
        {
            
            if (Input.GetKey(KeyCode.W))
            {
                playerSprite.IdleSprite();
            }

            if (Input.GetKey(KeyCode.A))
            {
                playerSprite.LeftSprite();
            }

            if (Input.GetKey(KeyCode.S) )
            {
                playerSprite.BackSprite();
            }

            if (Input.GetKey(KeyCode.D))
            {
                playerSprite.RightSprite();
            }
        }
        //Jump sprite aparece


        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround() && !isSpindashing)

        {
            
            playerRigidbody.AddForce(Vector3.up * impulse, ForceMode.Impulse);
            playerSprite.JumpSprite();
            int randomIndex = Random.Range(0, JumpVoices.Length);
            playerAudioSource.PlayOneShot(JumpVoices[randomIndex], 1);
        }
        
        //Stomp
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerRigidbody.AddForce(Vector3.down * 100f, ForceMode.Impulse);

            playerSprite.StompSprite();
        }

        //Spindash
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isSpindashing)
        {
            isSpindashing = true;

            playerRigidbody.velocity *= 0.9f;

            Debug.Log("RECARGANDO SPINDASH");

            spindashCoroutine = StartCoroutine(SpindashCooldown());

            if (!spindashParticleSystem.isPlaying)
            {
                spindashParticleSystem.gameObject.SetActive(true);

                spindashParticleSystem.Play();
                playerSprite.SpindashSprite();
                int randomIndex = Random.Range(0, SpindashVoices.Length);
                playerAudioSource.PlayOneShot(SpindashVoices[randomIndex], 1);
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse1) && isSpindashing)
        {
            isSpindashing = false;

            playerRigidbody.AddForce(focalPoint.transform.forward * spindashVelocity, ForceMode.VelocityChange);

            StopCoroutine(spindashCoroutine);
            gameManager.ResetSpindash(0f);
            Debug.Log("SPINDASH INICIADO");

            if (spindashParticleSystem.isPlaying)
            {
                spindashParticleSystem.Stop();
                playerSprite.IdleSprite();
                spindashParticleSystem.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator SpindashCooldown()
    {
        spindashVelocity = 0;
        gameManager.SetSpindash(spindashVelocity);
        Debug.Log("SPINDASH: " + spindashVelocity + " / 200");

        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.4f);

            spindashVelocity += 40;
            gameManager.SetSpindash(spindashVelocity);
            Debug.Log("SPINDASH: " + spindashVelocity + " / 200");
        }
    }

    private void FixedUpdate()
    {
        if (!isSpindashing)
        {
            horizontalInput = Input.GetAxis("Horizontal");

            verticalInput = Input.GetAxis("Vertical");

            playerRigidbody.AddForce(focalPoint.transform.forward * speed * verticalInput, ForceMode.Force);

            playerRigidbody.AddForce(focalPoint.transform.right * horizontalInput * speed);
        }

        if (playerRigidbody.velocity.magnitude > velocityMax)
        {
            playerRigidbody.velocity = playerRigidbody.velocity.normalized * velocityMax;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            playerSprite.IdleSprite();
        }
    }

    // DETECTA EL SUELO
    private bool IsOnGround()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitData, groundDistance, groundLayerMask);

        return hitData.collider != null;
    }
}
