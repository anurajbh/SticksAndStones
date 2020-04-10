using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
	public SMDialogueTrigger trigger;

	void Awake()
	{
		trigger = GetComponent<SMDialogueTrigger>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("PlayerBody"))
		{
			Debug.Log("Encountered locked door");
			string[] sentence = { "This door is locked." };
			trigger.TriggerDialogue(new Dialogue("", sentence));
		}
	}
}
