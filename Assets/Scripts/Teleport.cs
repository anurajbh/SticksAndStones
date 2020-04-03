using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject playerController;
    public Vector2 teleportPoint;
    private void Awake()
    {
        playerController = GameObject.Find("MovePoint");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerBody"))
        {
            print("Hello there");
            playerController.transform.position = new Vector2(teleportPoint.x, teleportPoint.y);
        }
    }
}
