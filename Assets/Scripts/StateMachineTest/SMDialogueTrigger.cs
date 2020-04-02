using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMDialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    SMPlayerStats player;
    public static int turn = 0; //0 indicates call by player, 1 indicates call by enemy

    private void Awake()
    {
        player = GameObject.Find("PlayerController").GetComponent<SMPlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (player.getState())
        {
            //need a way to differentiate when this state has been called by
            //player action or enemy action
            case Transitions.ProcessState.dialogue:
                TriggerDialogue();
                if (turn == 0)
                {
                    player.switchState(Transitions.Command.waitForEnemy);
                }
                else if (turn == 1)
                {
                    player.switchState(Transitions.Command.waitForPlayer);
                }
                break;
            default:
                break;
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().startDialogue(dialogue);
    }

    public void TriggerDialogue(Dialogue text)
    {
        dialogue = text;
        FindObjectOfType<DialogueManager>().startDialogue(dialogue);
    }
}
