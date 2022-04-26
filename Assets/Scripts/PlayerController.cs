using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float speed= 10f  ;
    public float impulse = 5F;
    public float groundDistance = 2f;
    public float speedRotation = 10f;
    public float velocityMax = 100f;
    public float lastPosition;

    public float mouseSensitivity = 100f;
    private float spinspeed = 200f;
    private GameObject focalPoint;
    private float horizontalInput, verticalInput;
    private Rigidbody playerRigidbody;
    private float currentSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        
        focalPoint = GameObject.Find("FocalPoint");
        Physics.gravity *= 2;
        //playerRigidbody.velocity = focalPoint.transform.forward * 200f;
    }

    // Update is called once per frame
    void Update()
    {
        //Jump 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsOnGround())
            {
                // JUMP
                playerRigidbody.AddForce(Vector3.up * impulse, ForceMode.Impulse);

            } else
            {
                //Boost en el aire
                playerRigidbody.AddForce(Vector3.up * 10f, ForceMode.Impulse);
                playerRigidbody.velocity = focalPoint.transform.forward * 50f;
            }

        }
        
        if (Input.GetKeyUp(KeyCode.Mouse0 ))
        {
            if (IsOnGround())
            {
                //Sale disparado hacia adelante respecto a la camara
                playerRigidbody.velocity = focalPoint.transform.forward * 100f;

            }
        }
         if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Spindash
            transform.Rotate(Vector3.left * spinspeed * Time.deltaTime);


          
            if (!IsOnGround())
            {
                //ground pound
                playerRigidbody.AddForce(Vector3.down * 100f, ForceMode.Impulse);

            }

        }
    }

    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        
        verticalInput = Input.GetAxis("Vertical");

        playerRigidbody.AddForce(focalPoint.transform.forward * speed * verticalInput,ForceMode.Force);

        playerRigidbody.AddForce(focalPoint.transform.right * horizontalInput * speed);
               
        if (playerRigidbody.velocity.magnitude > velocityMax)
        {
            playerRigidbody.velocity = playerRigidbody.velocity.normalized * velocityMax;
        }
    }
/*
   private void CurrentSpeed()
    {
        
        currentSpeed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = transform.position;
    }
    */
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
    
}
