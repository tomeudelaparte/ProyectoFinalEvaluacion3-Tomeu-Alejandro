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
    private float speedForce = 100f;
    private float impulseForce = 50f;
    private float stompForce = 100f;
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

    // Actualiza a cada frame
    void Update()
    {
        // Cambia la pose del personaje
        PlayerActionPose();

        // Accion SALTAR
        ActionJump();

        // Accion PISOTON
        ActionStomp();

        // Accion IMPULSO HACIA DELANTE
        ActionSpindash();
    }

    // MOVIMIENTO HORIZONTAL Y VERTICAL
    // Actualiza independientemente del framerate para el calculo de fisicas
    private void FixedUpdate()
    {
        // Si NO esta haciendo el SPINDASH
        if (!isSpindashing)
        {
            // Guarda los input axis
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            // Aplica una fuerza horizontal
            playerRigidbody.AddForce(focalPoint.transform.forward * speedForce * verticalInput, ForceMode.Force);

            // Aplica una fuerza vertical
            playerRigidbody.AddForce(focalPoint.transform.right * horizontalInput * speedForce);
        }

        // Velocidad maxima del rigidbody del player
        if (playerRigidbody.velocity.magnitude > velocityMax)
        {
            // Mantiene a la velocidad maxima
            playerRigidbody.velocity = playerRigidbody.velocity.normalized * velocityMax;
        }
    }

    // ACCIONES
    // Funcion que cambia la pose del personaje y en la interfaz de usuario.
    private void PlayerActionPose()
    {
        // Si esta tocando LAYER GROUND y NO está haciendo SPINDASH
        if (IsOnGround() && !isSpindashing)
        {
            // Si presionas la tecla W
            if (Input.GetKey(KeyCode.W))
            {
                // Cambia la pose del personaje
                playerSprite.IdleSprite();

                // Cambia la pose en la interfaz
                realTimeCam.IdleSprite();
            }

            // Si presionas la tecla A
            if (Input.GetKey(KeyCode.A))
            {
                // Cambia la pose del personaje
                playerSprite.LeftSprite();

                // Cambia la pose en la interfaz
                realTimeCam.LeftSprite();
            }

            // Si presionas la tecla S
            if (Input.GetKey(KeyCode.S))
            {
                // Cambia la pose del personaje
                playerSprite.BackSprite();
            }

            // Si presionas la tecla D
            if (Input.GetKey(KeyCode.D))
            {
                // Cambia la pose del personaje
                playerSprite.RightSprite();

                // Cambia la pose en la interfaz
                realTimeCam.RightSprite();
            }
        }
    }

    // Accion de SALTAR
    private void ActionJump()
    {
        // Si presiona la tecla SPACE y esta tocando el GROUND y NO esta haciendo SPINDASH
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround() && !isSpindashing)
        {
            // Aplica una fuerza, un impulso vertical hacia arriba
            playerRigidbody.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);

            // Cambia la pose del personaje y la pose de la interfaz
            playerSprite.JumpSprite();
            realTimeCam.JumpSprite();

            // Reproduce una frase aleatoria y un sonido predeterminado
            int randomIndex = Random.Range(0, JumpVoices.Length);
            playerAudioSource.PlayOneShot(JumpVoices[randomIndex], 1);
            playerAudioSource.PlayOneShot(jumpsound, 0.7f);
        }
    }

    // Accion de PISOTON
    private void ActionStomp()
    {
        // Si presionas el click izquierdo del raton
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Aplica una fuerza, un impulso vertical hacia abajo
            playerRigidbody.AddForce(Vector3.down * stompForce, ForceMode.Impulse);

            // Cambia la pose del personaje y la pose de la interfaz
            playerSprite.StompSprite();
            realTimeCam.StompSprite();
        }
    }

    // Accion de IMPULSO HACIA DELANTE
    private void ActionSpindash()
    {
        // Si presionas el click derecho del raton y NO esta haciendo SPINDASH
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isSpindashing)
        {
            // SPINDASH toma valor TRUE
            isSpindashing = true;

            // Disminuye la velocidad del rigidbody del player
            playerRigidbody.velocity *= 0.9f;

            // Activa la componente animator del player y acciona la animacion de SPINDASH
            playerAnimator.enabled = true;
            playerAnimator.SetTrigger("spinAnimation");

            // Empieza una COROUTINE de SPINDASH y la guarda para mas tarde
            spindashCoroutine = StartCoroutine(SpindashCharging());

            // Si el sistema de particulas de SPINDASH no estan reproduciendose
            if (!spindashParticleSystem.isPlaying)
            {
                // Cambia la pose del personaje y la pose de la interfaz
                playerSprite.SpindashSprite();
                realTimeCam.SpindashSprite();

                // Activa el sistema de particulas de SPINDASH y las reproduce
                spindashParticleSystem.gameObject.SetActive(true);
                spindashParticleSystem.Play();

                // Reproduce una frase aleatoria y un sonido predeterminado
                int randomIndex = Random.Range(0, SpindashVoices.Length);
                playerAudioSource.PlayOneShot(SpindashVoices[randomIndex], 1);
                playerAudioSource.PlayOneShot(Spindashsound, 0.5f);
            }
        }

        // Si presionas el click derecho del raton y esta haciendo SPINDASH
        if (Input.GetKeyUp(KeyCode.Mouse1) && isSpindashing)
        {
            // SPINDASH toma valor TRUE
            isSpindashing = false;

            // Aplica una fuerza, una cambio de velocidad hacia adelante
            playerRigidbody.AddForce(focalPoint.transform.forward * spindashVelocity, ForceMode.VelocityChange);

            // Resetea la barra de SPINDASH
            gameManager.ResetSpindash(0f);

            // Desactiva la componente Animator
            playerAnimator.enabled = false;

            // Reproduce un sonido predeterminado
            playerAudioSource.PlayOneShot(boostsound, 0.5f);

            // Para la COROUTINE de SPINDASH
            StopCoroutine(spindashCoroutine);

            // Si el sistema de particulas se esta reproduciendo
            if (spindashParticleSystem.isPlaying)
            {
                // Cambia la pose del personaje y la pose de la interfaz
                playerSprite.IdleSprite();
                realTimeCam.IdleSprite();

                // Para y desactiva el sistema de particulas de SPINDASH
                spindashParticleSystem.Stop();
                spindashParticleSystem.gameObject.SetActive(false);
            }
        }
    }

    // COROUTINE que carga la potencia del SPINDASHS
    private IEnumerator SpindashCharging()
    {
        // Resetea la velocidad del SPINDAHS
        spindashVelocity = 0;

        // Actualiza la barra de carga de SPINDASH
        gameManager.SetSpindash(spindashVelocity);

        // CARGA SPINDASH
        for (int i = 0; i < 5; i++)
        {
            // Cada 0.4 segundos
            yield return new WaitForSeconds(0.4f);

            // Suma +40 al total cargado
            spindashVelocity += 40;

            // Actualiza la barra de carga de SPINDASH
            gameManager.SetSpindash(spindashVelocity);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Si esta colisionando con GROUND
        if (other.gameObject.CompareTag("Ground"))
        {
            // Cambia la pose del personaje y la pose de la interfaz
            playerSprite.IdleSprite();
            realTimeCam.IdleSprite();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si esta triggeando con ITEM
        if (other.gameObject.CompareTag("Item"))
        {
            // Reproduce un sonido predeterminado
            playerAudioSource.PlayOneShot(ItemGetSound, 0.7f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Al entrar al trigger de WATER
        if (other.gameObject.CompareTag("Water"))
        {
            // Activa y reproduce el sistema de particulas
            waterParticleSystem.gameObject.SetActive(true);
            waterParticleSystem.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Al salir del trigger de WATER
        if (other.gameObject.CompareTag("Water"))
        {
            // Para y desactiva el sistema de particulas
            waterParticleSystem.Stop();
            waterParticleSystem.gameObject.SetActive(false);
        }
    }

    // DETECTA EL SUELO con RAYCAST
    private bool IsOnGround()
    {
        // Raycast hacia abajo con una distancia determinada
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitData, groundDistance, groundLayerMask);

        // Devuelve cualquier bool diferente a NULL
        return hitData.collider != null;
    }
}
