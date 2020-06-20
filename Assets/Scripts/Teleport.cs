using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private GameObject playerController;
    public Vector2 teleportingPoint;
	private AudioSource openDoor;
    
	private void Awake()
    {
        playerController = GameObject.Find("MovePoint");
		openDoor = GetComponent<AudioSource>();
    }

	IEnumerator Teleportation()
	{
		playerController.transform.position = new Vector2(teleportingPoint.x, teleportingPoint.y);
		yield return new WaitForSeconds(2);
		PlayerMovement.teleporting = false;
	}
    
	private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && PlayerMovement.teleporting == false)
        {
			openDoor.Play();
			PlayerMovement.teleporting = true;
			print("teleporting");
			StartCoroutine(Teleportation());
        }
    }
}
