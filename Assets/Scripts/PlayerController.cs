using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float impulse = 5F;
    public float groundDistance = 2f;
    public float speedRotation = 10f;
    public float velocityMax = 100f;
    public float lastPosition;
    public GameObject shadowPlayer;
    public ParticleSystem spindashParticleSystem;
    public float mouseSensitivity = 100f;

    private PlayerSprite playerSprite;

    private GameObject focalPoint;
    private float horizontalInput, verticalInput;
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;

    // SPINDASH VARIABLES
    private float spindashVelocity = 0;
    private bool isSpindashing = false;
    private Coroutine spindashCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerSprite = FindObjectOfType<PlayerSprite>();

        spindashParticleSystem.Stop();

        focalPoint = GameObject.Find("FocalPoint");

        Physics.gravity *= 4;

        playerSprite.IdleSprite();
    }

    // Update is called once per frame
    void Update()
    {
        shadowPositionRaycast();

        if(IsOnGround())
        {
            //playerSprite.IdleSprite();
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround() && !isSpindashing)

        {
            // JUMP 
            playerRigidbody.AddForce(Vector3.up * impulse, ForceMode.Impulse);

            playerSprite.JumpSprite();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !IsOnGround() && !isSpindashing)
        {
            // GROUND POUND
            playerRigidbody.AddForce(Vector3.down * 100f, ForceMode.Impulse);

            playerSprite.StompSprite();
            
        }

        // SPINDASH
        // ! Si al mantener click derecho y no est? recargando el spindash 
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isSpindashing)
        {
            // Spindash pasa a ser TRUE
            isSpindashing = true;

            // Reduce la velocidad del jugador a 1
            playerRigidbody.velocity -= Vector3.one * 1f;

            Debug.Log("RECARGANDO SPINDASH");

            // Empieza y guarda la coroutine en una variable global
            spindashCoroutine = StartCoroutine(SpindashCooldown());

            //Sistema de particulas
           
            if(!spindashParticleSystem.isPlaying)
            {
                spindashParticleSystem.gameObject.SetActive(true);

                spindashParticleSystem.Play();
                playerSprite.SpindashSprite();
            }
            
        }

        // SPINDASH PARTE 2
        // ! Si al soltar el click derecho y estaba recargando el spindash 
        if (Input.GetKeyUp(KeyCode.Mouse1) && isSpindashing)
        {
            // Spindash pasa a ser FALSE
            isSpindashing = false;

            // Aplica una fuerza forwards respecto a la direcci?n de la c?mar? con la velocidad recargada
            playerRigidbody.AddForce(focalPoint.transform.forward * spindashVelocity, ForceMode.VelocityChange);

            // Para la coroutine guardada en la variable anterior
            StopCoroutine(spindashCoroutine);

            Debug.Log("SPINDASH INICIADO");
          

            if (spindashParticleSystem.isPlaying)
            {
                spindashParticleSystem.Stop();
                playerSprite.IdleSprite();
                spindashParticleSystem.gameObject.SetActive(false);
            }
        }
    }

    // COROUTINE SPINDASH
    private IEnumerator SpindashCooldown()
    {
        // Restablece la velocidad recargada a 0
        spindashVelocity = 0;
                    Debug.Log("SPINDASH: " + spindashVelocity + " / 200");

        // Un bucle de 5 pasadas
        for (int i = 0; i < 5; i++)
        {
            // Espera 0.4 segundos
            yield return new WaitForSeconds(0.4f);
            
            // Suma +40 al total de la velocidad recargada
            spindashVelocity += 40;

            Debug.Log("SPINDASH: " + spindashVelocity + " / 200");
        }
    }


    // CONTROLES B?SICOS
    private void FixedUpdate()
    {
        // Si no est? spindasheando
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
        if(other.gameObject.CompareTag("Ground"))
        {
            playerSprite.IdleSprite();
        }
    }

    // DETECTA EL SUELO
    private bool IsOnGround()
    {
        RaycastHit hitData;

        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out hitData, groundDistance))
        {
            return hitData.collider.CompareTag("Ground");
        }
        else
        {
            return false;
        }
    }

    // SOMBRA DEL PLAYER
    private void shadowPositionRaycast()
    {
        RaycastHit hitData;

        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out hitData, 50))
        {
            // Guardamos la posici?n donde el raycast hace HIT
            Vector3 variableTemporal = hitData.point;

            // Modificamos la altura en Y de la posici?n
            variableTemporal.y += 0.1f;

            // La sombra toma la posici?n del HIT
            shadowPlayer.transform.position = variableTemporal;

            // Guarda la distancia entre la sombra y el player
            float distance = transform.position.y - shadowPlayer.transform.position.y;

            // Modifica la escala de la sombra seg?n la distancia
            shadowPlayer.transform.localScale = Vector3.one * -distance;
        }
    }
}
