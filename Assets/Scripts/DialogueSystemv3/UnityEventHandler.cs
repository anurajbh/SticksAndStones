using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UnityEventHandler : MonoBehaviour
{
    [HideInInspector] public UnityEvent eventHandler;
    [HideInInspector] public DialogueBase dialogue;

    public void InitiateAction()
    {
        StartCoroutine(InDialogueBuffer());
    }

    IEnumerator InDialogueBuffer()
    {
        yield return new WaitForSeconds(0.01f); //needed for controlling execution flow
        eventHandler.Invoke(); //triggers the button's event
        DialogueManager.instance.CloseOptions(); //makes sure options are closed post dialogue if not handled already
        DialogueManager.triggered = false; //makes sure different dialogues don't overlap

        if (dialogue != null)
        {
            DialogueManager.instance.AddDialogue(dialogue);
        } else if (DialogueManager.instance.abilityLearned == true) 
        {
            DialogueManager.instance.LearnAbilities();
        }
    }
}
