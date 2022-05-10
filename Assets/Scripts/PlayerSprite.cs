using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSprite : MonoBehaviour
{
    public Sprite[] playerSprites;

    private GameObject theCam;
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        theCam = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(theCam.transform);

        transform.position = (Player.transform.position + Vector3.up * 2.5f);
    }

    private void ChangeSprite(int index)
    {
        GetComponent<Image>().sprite = playerSprites[index];
    }

    public void IdleSprite()
    {
        ChangeSprite(0);
    }

    public void JumpSprite()
    {
        ChangeSprite(1);
    }
    public void BackwardSprite()
    {
        ChangeSprite(2);
    }

    public void SpindashSprite()
    {
        ChangeSprite(3);
    }
    public void StompSprite()
    {
        ChangeSprite(4);
    }
}
