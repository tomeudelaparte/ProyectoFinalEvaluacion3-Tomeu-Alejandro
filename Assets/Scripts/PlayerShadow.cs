using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    public GameObject player;

    private Vector3 hitDataPoint;
    private float groundDistance;

    void Update()
    {
        if (Physics.Raycast(player.transform.position, Vector3.down, out RaycastHit hitData, 50))
        {
            hitDataPoint = hitData.point;

            hitDataPoint.y += 0.1f;

            transform.position = hitDataPoint;

            groundDistance = transform.position.y - player.transform.position.y;

            transform.localScale = Vector3.one * -groundDistance;
        }
    }
}
