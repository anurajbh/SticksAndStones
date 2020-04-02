using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMNPCEntity : MonoBehaviour
{
    //TO DO: finish implementing NPC death/defeat (more below)

    int health = 10;
    readonly int maxHealth = 10;
    SMPlayerStats player;
    public bool spokenTo = false;

    private void Awake()
    {
        player = GameObject.Find("PlayerController").GetComponent<SMPlayerStats>();
    }

    public int adjustHealth(int amount)
    {
        if (health + amount <= 0)
        {
            health = 0;
            Die();
        }
        else if (health + amount > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += amount;
        }

        return health;
    }

    public void Die()
    {
        //should display final dialogue before battle exit
        player.switchState(Transitions.Command.exitBattle);
        //what happens to an NPC when they're defeated?
    }

}
