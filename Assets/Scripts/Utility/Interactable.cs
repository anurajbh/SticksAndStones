using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //made to act as a super class for any interactable objects (including NPCs with dialogue and stuff)
    //works off of range and distance between the sprites of two bodies instead of collisions :')
    public float interactRange = 2;

    void Update()
    {
        //range checking is less expensive than collision checking
        //also avoids messing around with colliders
        if(Vector2.Distance(gameObject.transform.position, GameManager.instance.player.position) < interactRange)
        {
            //have a "Press Button to Interact" appear
            if(Input.GetKeyDown(KeyCode.Z) && !DialogueManager.spokenTo) //replace with CrossPlatformInput stuff
            {
                Interact();
            }
        }    
    }

    public virtual void Interact()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
