using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : Action
{
    SMDialogueTrigger error;
    static Dictionary<string, (int, int, int)> attacks = new Dictionary<string, (int, int, int)>
    {
        { "poke", (1, -1, -1) },
        { "cry", (2, -3, 0) },
    };                                                                                 //attacks will be stored as name, (anxietyEffect, willEffect, enemyDamage) pairs

    private void Awake()
    {
        player = GameObject.Find("PlayerController").GetComponent<SMPlayerStats>();
        enemy = GameObject.Find("NPC").GetComponent<SMNPCEntity>();
        error = GameObject.Find("Attack 1").GetComponent<SMDialogueTrigger>();
    }
    public override void Learn(string name, int anxiety, int will, int enemyDamage)
    {
        attacks.Add(name, (anxiety, will, enemyDamage));
    }

    public override (int, int, int) Use(string moveName)
    {
        player.adjustAnxiety(attacks[moveName].Item1);
        if (player.adjustWill(attacks[moveName].Item2) < 0)
        {
            string[] msg = new string[] { "You don't have enough Will!" };
            error.TriggerDialogue(new Dialogue("", msg));
            player.switchState(Transitions.Command.waitForPlayer);
            return (0, 0, 0);
        }                   //rework playerStats so that you get unsuccessful moves if you don't have enough will
        enemy.adjustHealth(attacks[moveName].Item3);
        return attacks[moveName];
    }

    public static int GetSize()
    {
        return attacks.Count;
    }
}
