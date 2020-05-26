using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : Interactable
{
    public DialogueBase dialogue;

    public void TriggerDialogue()
    {
        DialogueManager.instance.AddDialogue(dialogue);
    }

    public override void Interact()
    {
        TriggerDialogue();
    }

    /*private void Update()
    {
        //start interaction collisions should be done through here
        if (!Manager.triggered && Input.GetKeyDown(KeyCode.Z))
        {
            TriggerDialogue();
        }
    }*/
}
