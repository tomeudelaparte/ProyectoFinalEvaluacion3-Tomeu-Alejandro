using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RealTimeCam : MonoBehaviour
{
    public Sprite[] playerSprites;

    // Cambia el sprite segun el index en la componente Image
    private void ChangeSprite(int index)
    {
        GetComponent<Image>().sprite = playerSprites[index];
    }

    // Pose predeterminada
    public void IdleSprite()
    {
        ChangeSprite(0);
    }

    // Pose izquierda
    public void LeftSprite()
    {
        ChangeSprite(5);
    }

    // Pose derecha
    public void RightSprite()
    {
        ChangeSprite(4);
    }

    // Pose salto
    public void JumpSprite()
    {
        ChangeSprite(3);
    }

    // Pose spindash
    public void SpindashSprite()
    {
        ChangeSprite(1);
    }

    // Pose pisoton
    public void StompSprite()
    {
        ChangeSprite(2);
    }
}
