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

    public float mouseSensitivity = 100f;

    private GameObject focalPoint;
    private float horizontalInput, verticalInput;
    private Rigidbody playerRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();

        focalPoint = GameObject.Find("FocalPoint");
        Physics.gravity *= 2;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround())
        {
            playerRigidbody.AddForce(Vector3.up * impulse, ForceMode.Impulse);
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
