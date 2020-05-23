using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMNPCAI : MonoBehaviour
{
    //TO DO: implement the different attacks for Charlotte, James, and the shadow
    //(essentially find and assign 'name')

    public SMNPCEntity npc;
    SMPlayerStats player;
    SMDialogueTrigger displayStat;
    public Charlotte charlotte;

    void Awake()
    {
        npc = gameObject.GetComponent<SMNPCEntity>();
        player = GameObject.FindWithTag("Player").GetComponent<SMPlayerStats>();
        displayStat = gameObject.GetComponent<SMDialogueTrigger>();
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
        switch (whatItChooses)
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
        displayStat.TriggerDialogue(new Dialogue("", msg));
        player.switchState(Transitions.Command.enemyChoice);
        SMDialogueTrigger.turn = 3;
    }
}
