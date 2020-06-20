using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Attack", menuName = "Attack")]
public class Attacks : Action
{
    public Dictionary<string, (int, int, int)> attacks = new Dictionary<string, (int, int, int)>//int or float?
    {
    };                                                                                 //attacks stored as name, (anxietyEffect, willEffect, enemyDamage) pairs

    private void Awake()
    {
       
    }
    public override void Learn(string name, int anxiety, int will, int enemyDamage)
    {
        attacks.Add(name, (anxiety, will, enemyDamage));
    }

    public override (int, int, int) Use(string moveName)
    {
        if (whosAttacking == 1)
        {
            PlayerStats.Instance.adjustAnxiety(attacks[moveName].Item1);
            return attacks[moveName];
        }
        PlayerStats.Instance.adjustAnxiety(attacks[moveName].Item1);
        PlayerStats.Instance.adjustWill(attacks[moveName].Item2);
        //not adjusting enemy health from here so that the bool cna be passed back through BattleSystem
        //should differentiate between when the NPC is using a move vs the player
        //enemy.adjustHealth(attacks[moveName].Item3);
        return attacks[moveName];
    }

    public override bool Check(string moveName) //checks if player has enough will to execute a move
    {
        return !(PlayerStats.Instance.totalWill < Mathf.Abs(attacks[moveName].Item2));
    }

    public int GetSize()
    {
        return attacks.Count;
    }

    //Checks for skill in dictionary
    public bool Contains(string attackName) {
        return attacks.ContainsKey(attackName);
    }
}
