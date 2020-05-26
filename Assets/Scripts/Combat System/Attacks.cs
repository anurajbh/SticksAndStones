using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Attack", menuName = "Attack")]
public class Attacks : Action
{
    //TO DO: trigger error message for limited will, deal enemy damage

    PlayerStats player = PlayerStats.instance;

    //SMDialogueTrigger error;
    /*static Dictionary<string, (int, int, int)> allAttacks = new Dictionary<string, (int, int, int)>
    {
        //hub dictionary for all attacks/skills that the player can learn
        //also a hub for npc moves
        { "poke", (1, -1, -1) },
        { "cry", (2, -3, 0) },
    };    */                                                                             //attacks will be stored as name, (anxietyEffect, willEffect, enemyDamage) pairs

    private void Awake()
    {
        //enemy = GameObject.Find("NPC").GetComponent<NPC>();
        //error = GameObject.Find("Attack 1").GetComponent<SMDialogueTrigger>();
    }
    public override void Learn(string name, int anxiety, int will, int enemyDamage)
    {
        PlayerStats.attacks.Add(name, (anxiety, will, enemyDamage));
    }

    public override (int, int, int) Use(string moveName)
    {
        player.adjustAnxiety(PlayerStats.attacks[moveName].Item1);
        if (!player.adjustWill(PlayerStats.attacks[moveName].Item2))
        {
            string[] msg = new string[] { "You don't have enough Will!" };
            //error.TriggerDialogue(new Dialogue("", msg));
            return (0, 0, 0);
        }                   //rework playerStats so that you get unsuccessful moves if you don't have enough will
        //enemy.adjustHealth(attacks[moveName].Item3);
        return PlayerStats.attacks[moveName];

        //return (0, 0, 0);
    }

}
