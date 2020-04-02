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

    void Awake()
    {
        npc = gameObject.GetComponent<SMNPCEntity>();
        player = GameObject.Find("PlayerController").GetComponent<SMPlayerStats>();
        displayStat = gameObject.GetComponent<SMDialogueTrigger>();
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
        Attacks move = new Attacks();
        (int, int, int) stats = (0, 0, 0);
        int whatItChooses = Random.Range(1, 5);
        switch (whatItChooses)
        {
            case 1:
                stats = move.Use(name);
                print("They scared you!");
                break;
            case 2:
                stats = move.Use(name);
                print("They healed themself");
                break;
            case 3:
                stats = move.Use(name);
                print("They attacked you!");
                break;
            case 4:
                stats = move.Use(name);
                print("Their mental attribute increased");
                break;
        }

        string[] msg = new string[] { "Your anixety changed by " + stats.Item1 +
                "!\nYour will changed by " + stats.Item2 + "!" };
        displayStat.TriggerDialogue(new Dialogue("", msg));
        SMDialogueTrigger.turn = 1;
        player.switchState(Transitions.Command.enemyChoice);
    }
}
