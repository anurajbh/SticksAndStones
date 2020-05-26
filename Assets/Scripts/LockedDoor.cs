using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    //TO DO: replace commented line with correct dialogue syntax
	public Trigger trigger;

	void Awake()
	{
		trigger = GetComponent<Trigger>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("PlayerBody"))
		{
			Debug.Log("Encountered locked door");
			string[] sentence = { "This door is locked." };
			//trigger.TriggerDialogue(new Dialogue("", sentence));
		}
	}
}
