using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;

    private AudioSource playerAudioSource;
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    private PlayerSprite playerSprite;
    private GameObject focalPoint;

    private float horizontalInput, verticalInput;

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

    [Header("AUDIO CLIPS")]
    public AudioClip[] JumpVoices;
    public AudioClip[] SpindashVoices;
    public AudioClip Spindashsound;
    public AudioClip jumpsound;
    public AudioClip boostsound;
    public AudioClip ItemGetSound;

    [Header("SPINDASH")]
    private float spindashVelocity = 0;
    private bool isSpindashing = false;
    private Coroutine spindashCoroutine = null;


    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerSprite = FindObjectOfType<PlayerSprite>();
        playerAudioSource = GetComponent<AudioSource>();

        focalPoint = GameObject.Find("FocalPoint");

        spindashParticleSystem.Stop();

        playerAnimator.enabled = false;

        playerSprite.IdleSprite();
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

            if (Input.GetKey(KeyCode.S))
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
            playerAudioSource.PlayOneShot(JumpVoices[randomIndex], 0.5f);
            playerAudioSource.PlayOneShot(jumpsound, 0.7f);
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

            playerAnimator.enabled = true;
            playerAnimator.SetTrigger("spinAnimation");

            spindashCoroutine = StartCoroutine(SpindashCooldown());

            if (!spindashParticleSystem.isPlaying)
            {
                spindashParticleSystem.gameObject.SetActive(true);

                spindashParticleSystem.Play();
                playerSprite.SpindashSprite();
                int randomIndex = Random.Range(0, SpindashVoices.Length);
                playerAudioSource.PlayOneShot(SpindashVoices[randomIndex], 1);

            }
            playerAudioSource.PlayOneShot(Spindashsound, 0.5f);
        }

        if (Input.GetKeyUp(KeyCode.Mouse1) && isSpindashing)
        {
            isSpindashing = false;

            playerRigidbody.AddForce(focalPoint.transform.forward * spindashVelocity, ForceMode.VelocityChange);

            StopCoroutine(spindashCoroutine);
            gameManager.ResetSpindash(0f);

            if (spindashParticleSystem.isPlaying)
            {
                spindashParticleSystem.Stop();
                playerSprite.IdleSprite();
                spindashParticleSystem.gameObject.SetActive(false);
            }
            playerAudioSource.Stop();
            playerAnimator.SetTrigger("spinStop");
            playerAnimator.enabled = false;
            playerAudioSource.PlayOneShot(boostsound, 0.5f);
        }
    }

    private IEnumerator SpindashCooldown()
    {
        spindashVelocity = 0;
        gameManager.SetSpindash(spindashVelocity);

        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.4f);

            spindashVelocity += 40;
            gameManager.SetSpindash(spindashVelocity);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            playerAudioSource.PlayOneShot(ItemGetSound, 0.7f);
        }
    }
    // DETECTA EL SUELO
    private bool IsOnGround()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitData, groundDistance, groundLayerMask);

        return hitData.collider != null;
    }
}
