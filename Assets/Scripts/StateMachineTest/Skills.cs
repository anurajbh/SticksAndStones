using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : Action
{
    //SMDialogueTrigger error;
    static Dictionary<string, (int, int, int)> skills = new Dictionary<string, (int, int, int)>//int or float?
    {
        
    }; //skills stored as name, (anxietyEffect, willEffect, enemyDamage) pairs

    private void Awake()
    {
        //player = GameObject.Find("PlayerController").GetComponent<SMPlayerStats>();// using the player instance instead
        enemy = GameObject.Find("NPC").GetComponent<SMNPCEntity>();//should be context-specific, tied to the one on which it is being used or the one who is using it(if enemy)
       // error = GameObject.Find("Attack 1").GetComponent<SMDialogueTrigger>();
    }
    
    public override void Learn(string name, int anxiety, int will, int enemyDamage)
    {
        skills.Add(name, (anxiety, will, enemyDamage));
    }

    public override (int, int, int) Use(string moveName)
    {
        SMPlayerStats.Instance.adjustAnxiety(skills[moveName].Item1);
        if (SMPlayerStats.Instance.adjustWill(skills[moveName].Item2) < 0)
        {
            string[] msg = new string[] { "You don't have enough Will!" };
            //error.TriggerDialogue(new Dialogue("", msg));
            //player.switchState(Transitions.Command.waitForPlayer);//should be in combat system
            return (0, 0, 0);
        }
        enemy.adjustHealth(skills[moveName].Item3);
        return skills[moveName];
    }

    public static int GetSize()
    {
        return skills.Count;
    }
}
