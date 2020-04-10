using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAI : MonoBehaviour
{
    public NPCEntity npc;
    PlayerStats player;
    public Charlotte charlotte;

    void Awake()
    {
        npc = GetComponent<NPCEntity>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        charlotte = GetComponent<Charlotte>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.getState() == Transitions.ProcessState.enemyTurn)
        {
            EnemyTurn();
        }
    }

    public void EnemyTurn()
    {
        (int, int) stats = (0, 0);
        int whatItChooses = Random.Range(1, 3);
        switch  (whatItChooses)
        {
            case 1:
                stats = charlotte.Use("Woeful Screech");
                break;
            case 2:
                stats = charlotte.Use("Speechless Gambit");
                break;
            default:
                break;
        }

        string[] msg = new string[] { "Your anixety changed by " + stats.Item1 +
                "!\nYour will changed by " + stats.Item2 + "!" };
        DialogueManager.calledBy = DialogueManager.Caller.nar;
        DialogueTrigger.TriggerDialogue(new Dialogue("", msg));
        DialogueManager.calledBy = DialogueManager.Caller.enemyB;
        player.switchState(Transitions.Command.enemyChoice);
    }
}
