using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private GameObject playerController;
    public Vector2 teleportPoint;
	private AudioSource openDoor;
    
	private void Awake()
    {
        playerController = GameObject.Find("MovePoint");
		openDoor = GetComponent<AudioSource>();
    }

	IEnumerator Teleportation()
	{
		yield return new WaitForSeconds(2);
		PlayerMovement.teleporting = false;
	}
    
	private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerBody") && PlayerMovement.teleporting == false)
        {
			openDoor.Play();
			PlayerMovement.teleporting = true;
			print("Hello there");
            playerController.transform.position = new Vector2(teleportPoint.x, teleportPoint.y);
			StartCoroutine(Teleportation());
        }
    }
}
