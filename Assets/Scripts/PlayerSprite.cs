using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSprite : MonoBehaviour
{
    public GameObject player;
    public GameObject focalPoint;

    public Sprite[] playerSprites;

    void LateUpdate()
    {
        transform.LookAt(focalPoint.transform);

        transform.position = (player.transform.position + Vector3.up * 2.5f);
    }

    private void ChangeSprite(int index)
    {
        GetComponent<Image>().sprite = playerSprites[index];
    }

    public void IdleSprite()
    {
        ChangeSprite(0);
    }

    public void LeftSprite()
    {
        ChangeSprite(1);
    }
    public void RightSprite()
    {
        ChangeSprite(2);
    }

    public void BackSprite()
    {
        ChangeSprite(3);
    }

    public void JumpSprite()
    {
        ChangeSprite(4);
    }

    public void SpindashSprite()
    {
        ChangeSprite(5);
    }
    public void StompSprite()
    {
        ChangeSprite(6);
    }
}
