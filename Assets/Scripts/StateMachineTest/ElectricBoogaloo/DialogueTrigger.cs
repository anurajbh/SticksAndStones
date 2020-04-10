using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public static Dialogue staticDialogue;
    public Dialogue dialogue;
    PlayerStats player;
    //DialogueManager.Caller caller = DialogueManager.Caller.regDialogue;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.getState() == Transitions.ProcessState.dialogue)
        {
            TriggerDialogue();
            StartCoroutine("WaitForDialogue");
        }
        
    }

    IEnumerator WaitForDialogue()
    {
        while (DialogueManager.moreDialogue)
        {
            yield return null;
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    //this following method will be unnecessary after refactoring error
    //messages to change DialogueTrigger.dialogue to their message... maybe

    public static void TriggerDialogue(Dialogue text)
    {
        staticDialogue = text;
        FindObjectOfType<DialogueManager>().StartDialogue(staticDialogue);
    }
}
