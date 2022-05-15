using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Realtimecam : MonoBehaviour
{
    public GameObject player;


    public Sprite[] playerSprites;

    
    private void ChangeSprite(int index)
    {
        GetComponent<Image>().sprite = playerSprites[index];
    }

    public void IdleSprite()
    {
        ChangeSprite(0);
    }

    public void SpindashSprite()
    {
        ChangeSprite(1);
    }
    public void StompSprite()
    {
        ChangeSprite(2);
    }

    public void jumpSprite()
    {
        ChangeSprite(3);
    }

   void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))

        {
            jumpSprite();
        }

        if(Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.D))

        {
            IdleSprite();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))

        {
            SpindashSprite();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))

        {
            StompSprite();
        }
    }
}
