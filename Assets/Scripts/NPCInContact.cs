using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInContact : MonoBehaviour
{
    public bool CanBeSpokenTo = false;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            CanBeSpokenTo = true;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            CanBeSpokenTo = false;
        }
    }
    void Update()
    {
        if(CanBeSpokenTo && Input.GetKeyDown(KeyCode.Z))
        {
            SpeakToNPC();
        }
    }

    private void SpeakToNPC()
    {
        print("Hello fellow sprite!");
        //trigger dialogue system
    }
}
