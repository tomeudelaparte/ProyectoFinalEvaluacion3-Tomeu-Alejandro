using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSprite : MonoBehaviour
{
    public GameObject player;
    public GameObject focalPoint;

    public Sprite[] playerSprites;

    void Update()
    {
        // Mira en direccion al FocalPoint(Camara)
        transform.LookAt(focalPoint.transform);

        // Sigue la posicion del player junto a un offset
        transform.position = (player.transform.position + Vector3.up * 2.5f);
    }

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
        ChangeSprite(1);
    }

    // Pose derecha
    public void RightSprite()
    {
        ChangeSprite(2);
    }

    // Pose hacia atras
    public void BackSprite()
    {
        ChangeSprite(3);
    }

    // Pose salto
    public void JumpSprite()
    {
        ChangeSprite(4);
    }

    // Pose spindash
    public void SpindashSprite()
    {
        ChangeSprite(5);
    }

    // Pose pisoton
    public void StompSprite()
    {
        ChangeSprite(6);
    }
}
