using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMDialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    SMPlayerStats player;
    public static int turn = 0; //0 indicates call by regular dialogue, 1 indicates dialogue with choice, 2 indicates
                                //call by player, 3 indicates call by enemy

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<SMPlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (player.getState())
        {
            case Transitions.ProcessState.dialogue:
                TriggerDialogue();
                switch (turn)
                {
                    case 0:
                        break;
                    case 1:
                        player.switchState(Transitions.Command.waitForChoice);
                        break;
                    case 2:
                        player.switchState(Transitions.Command.waitForEnemy);
                        break;
                    case 3:
                        player.switchState(Transitions.Command.waitForPlayer);
                        break;
                    default:
                        break;
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
