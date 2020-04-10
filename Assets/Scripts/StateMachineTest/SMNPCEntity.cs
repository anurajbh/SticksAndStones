using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMNPCEntity : MonoBehaviour
{

    int health = 10;
    readonly int maxHealth = 10;
    SMPlayerStats player;
    public bool spokenTo = false;
    string charName;
    NPC character;
    SMDialogueTrigger trigger;
    public static char dialogueChoice = 'a';


    private void Awake()
    {
        player = GameObject.Find("PlayerController").GetComponent<SMPlayerStats>();
        charName = gameObject.name;
        trigger = gameObject.GetComponent<SMDialogueTrigger>();
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
        string[] endSpeech = { "Hmph. I didn’t ask for your permission to make my own life choices.",
        "I’ll join the drama club on my own accord! You don’t have any say in it!"};
        trigger.TriggerDialogue(new Dialogue("Elly", endSpeech));
        player.adjustAnxiety(-2);
        player.adjustWill(2);
        Destroy(gameObject);
        player.switchState(Transitions.Command.exitBattle);
        
    }

}
