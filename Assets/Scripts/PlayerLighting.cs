using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLighting : MonoBehaviour
{

	public Material spriteLighting; // the material to be used on the player prefab
	public GameObject playerCharacter;

    void Awake()
    {
		playerCharacter = GameObject.FindWithTag("Player");
    }

	void OnTriggerEnter2D(Collider2D other) 
	{
		Debug.Log("Changing lighting effects on player sprite");
		playerCharacter.GetComponent<Renderer>().material = spriteLighting; // change the material on the sprite
	}
		
}
