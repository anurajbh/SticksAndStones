using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    Button button;
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().startDialogue(dialogue);
    }
    public void TriggerStats()
    {
        switch(button.name)
        {
            default:
            break;
                //TODO- Check for button names and cause stuff to happen to Player Anxiety and Will depending on buttonname 
        }
    }
}
