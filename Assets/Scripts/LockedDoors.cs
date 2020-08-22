using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoors : Interactable
{
    public DialogueBase lockedDoorPrompt;
    private bool interacting = false;

    public override void Interact() {
        if (!interacting) {
            interacting = true;
            DialogueManager.instance.AddDialogue(lockedDoorPrompt);
        }
    }

    public void OnTriggerExit2D() {
        interacting = false;
    }
}
