using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public DialogueBase dialogue;

    public void TriggerDialogue()
    {
        Manager.instance.AddDialogue(dialogue);
    }

    private void Update()
    {
        //start interaction collisions should be done through here
        if (!Manager.triggered && Input.GetKeyDown(KeyCode.Z))
        {
            TriggerDialogue();
        }
    }
}
