using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : Action
{
    //TO DO: replace all commented lines with the correct code for this to function
    
    //SMDialogueTrigger error;
    public Dictionary<string, (int, int, int)> skills = new Dictionary<string, (int, int, int)>
    {
        
    }; //skills stored as name, (anxietyEffect, willEffect, enemyDamage) pairs

    private void Awake()
    {
        //player = GameObject.Find("PlayerController").GetComponent<PlayerStats>();
        //enemy = GameObject.Find("NPC").GetComponent<NPC>();
        //error = GameObject.Find("Attack 1").GetComponent<SMDialogueTrigger>();
    }
    
    public override void Learn(string name, int anxiety, int will, int enemyDamage)
    {
        skills.Add(name, (anxiety, will, enemyDamage));
    }

    public override (int, int, int) Use(string moveName)
    {
        if (whosAttacking == 1) //this is done so that player death can be calculated within the BattleSystem
        {
            PlayerStats.Instance.adjustAnxiety(skills[moveName].Item1);
            return skills[moveName];
        }
        PlayerStats.Instance.adjustAnxiety(skills[moveName].Item1);
        PlayerStats.Instance.adjustWill(skills[moveName].Item2);
        return skills[moveName];
    }

    public override bool Check(string moveName) //checks if player has enough will to execute a move
    {
        return PlayerStats.Instance.totalWill < Mathf.Abs(skills[moveName].Item2);
    }

    public int GetSize()
    {
        return skills.Count;
    }
    //Checks for skill in dictionary
    public bool Contains(string skillName) {
        return skills.ContainsKey(skillName);
    }
}
