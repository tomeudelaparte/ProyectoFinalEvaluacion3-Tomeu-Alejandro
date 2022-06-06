using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RealTimeCam : MonoBehaviour
{
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

    public void JumpSprite()
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
