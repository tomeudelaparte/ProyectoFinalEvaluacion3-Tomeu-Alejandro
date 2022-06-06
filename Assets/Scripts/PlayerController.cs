using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("PLAYER")]
    private GameManager gameManager;

    private AudioSource playerAudioSource;
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    private PlayerSprite playerSprite;
    private RealTimeCam realTimeCam;
    private GameObject focalPoint;

    private float horizontalInput, verticalInput;

    [Header("GROUND DETECTION")]
    public LayerMask groundLayerMask;

    [Header("VALUES")]
    private float speed = 100f;
    private float impulse = 50f;
    private float groundDistance = 2f;
    private float velocityMax = 150f;

    [Header("PARTICLES")]
    public ParticleSystem spindashParticleSystem;
    public ParticleSystem waterParticleSystem;

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
        realTimeCam = FindObjectOfType<RealTimeCam>();
        playerAudioSource = GetComponent<AudioSource>();

        focalPoint = GameObject.Find("FocalPoint");

        playerAnimator.enabled = false;
        playerSprite.IdleSprite();
        realTimeCam.IdleSprite();
    }

    void Update()
    {
        PlayerActionPose();

        ActionJump();
        ActionStomp();
        ActionSpindash();
    }

    private void PlayerActionPose()
    {
        if (IsOnGround() && !isSpindashing)
        {

            if (Input.GetKey(KeyCode.W))
            {
                playerSprite.IdleSprite();
                realTimeCam.IdleSprite();
            }

            if (Input.GetKey(KeyCode.A))
            {
                playerSprite.LeftSprite();
                realTimeCam.LeftSprite();
            }

            if (Input.GetKey(KeyCode.S))
            {
                playerSprite.BackSprite();
            }

            if (Input.GetKey(KeyCode.D))
            {
                playerSprite.RightSprite();
                realTimeCam.RightSprite();
            }
        }
    }

    private void ActionJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround() && !isSpindashing)
        {
            playerRigidbody.AddForce(Vector3.up * impulse, ForceMode.Impulse);

            playerSprite.JumpSprite();
            realTimeCam.JumpSprite();

            int randomIndex = Random.Range(0, JumpVoices.Length);
            playerAudioSource.PlayOneShot(JumpVoices[randomIndex], 1);
            playerAudioSource.PlayOneShot(jumpsound, 0.7f);
        }
    }

    private void ActionStomp()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerRigidbody.AddForce(Vector3.down * 100f, ForceMode.Impulse);

            playerSprite.StompSprite();
            realTimeCam.StompSprite();
        }
    }

    private void ActionSpindash()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isSpindashing)
        {
            isSpindashing = true;

            playerRigidbody.velocity *= 0.9f;

            playerAnimator.enabled = true;
            playerAnimator.SetTrigger("spinAnimation");

            spindashCoroutine = StartCoroutine(SpindashCharging());

            if (!spindashParticleSystem.isPlaying)
            {
                playerSprite.SpindashSprite();
                realTimeCam.SpindashSprite();

                spindashParticleSystem.gameObject.SetActive(true);
                spindashParticleSystem.Play();

                int randomIndex = Random.Range(0, SpindashVoices.Length);
                playerAudioSource.PlayOneShot(SpindashVoices[randomIndex], 1);
                playerAudioSource.PlayOneShot(Spindashsound, 0.5f);
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse1) && isSpindashing)
        {
            isSpindashing = false;

            playerRigidbody.AddForce(focalPoint.transform.forward * spindashVelocity, ForceMode.VelocityChange);

            gameManager.ResetSpindash(0f);

            playerAnimator.enabled = false;
            playerAudioSource.PlayOneShot(boostsound, 0.5f);

            StopCoroutine(spindashCoroutine);

            if (spindashParticleSystem.isPlaying)
            {
                playerSprite.IdleSprite();
                realTimeCam.IdleSprite();

                spindashParticleSystem.Stop();
                spindashParticleSystem.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator SpindashCharging()
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
            realTimeCam.IdleSprite();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            playerAudioSource.PlayOneShot(ItemGetSound, 0.7f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            waterParticleSystem.gameObject.SetActive(true);
            waterParticleSystem.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            waterParticleSystem.Stop();
            waterParticleSystem.gameObject.SetActive(false);
        }
    }
    // DETECTA EL SUELO
    private bool IsOnGround()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitData, groundDistance, groundLayerMask);

        return hitData.collider != null;
    }
}
