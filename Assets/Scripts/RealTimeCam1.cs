using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RealTimeCam1 : MonoBehaviour
{
    public Sprite[] playerSprites;

    private void ChangeSprite(int index)
    {
        GetComponent<Image>().sprite = playerSprites[index];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpSprite();
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
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

        if (Input.GetKeyDown(KeyCode.A))
        {
            LeftSprite();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            RightSprite();
        }
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

    public void RightSprite()
    {
        ChangeSprite(4);
    }

    public void LeftSprite()
    {
        ChangeSprite(5);
    }

    
}
