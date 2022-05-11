using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    public GameObject player;

    void Update()
    {

        if (Physics.Raycast(player.transform.position, Vector3.down, out RaycastHit hitData, 50))
        {
            Vector3 temp = hitData.point;

            temp.y += 0.1f;

            transform.position = temp;

            float distance = transform.position.y - player.transform.position.y;

            transform.localScale = Vector3.one * -distance;
        }
    }
}
