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
    string charName;
    NPC character;
    SMDialogueTrigger trigger;


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

    public IEnumerator Converse(string eventName)
    {
        for (int i = 0; i < Charlotte.events[eventName].Item1; i++)
        {
            string dialogueName = "dialogue" + i;
            Dictionary<string, (string[], bool)> eventSelect = Charlotte.events[eventName].Item2;
            string[] toSay = eventSelect[dialogueName].Item1;
            trigger.dialogue = new Dialogue(charName, toSay);
            if (eventSelect[dialogueName].Item2)
            {
                SMDialogueTrigger.turn = 1;
            }
            else
            {
                SMDialogueTrigger.turn = 0;
            }
            player.switchState(Transitions.Command.enterConvo);
            yield return null;
        }
        yield return null;
    }

    public void Die()
    {
        //should display final dialogue before battle exit
        player.switchState(Transitions.Command.exitBattle);
        //what happens to an NPC when they're defeated?
    }

}
