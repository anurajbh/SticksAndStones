using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : Interactable
{
    //literally just triggers dialogue
    //notice it derives from interactable

    public static DialogueBase dialogue;

    //a separate method in case we need to call it separate to the interactable stuff
    public static void TriggerDialogue()
    {
        DialogueManager.instance.AddDialogue(dialogue);
    }

    public override void Interact()
    {
        TriggerDialogue();
    }
}
