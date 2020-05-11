using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "NPC", menuName = "NPC")]
public class NPC : MonoBehaviour //ScriptableObject
{
    //TO DO: replace all commented lines with the correct code for this to function

    //protected PlayerStats player;
    //protected NPCEntity enemy;

    public int maxHealth;
    public int currentHealth;
    
    //public string[][] interaction;

    //public abstract (int, int) Use(string moveName);

    //public abstract void Converse();

    public bool adjustHealth(int amount)
    {
        if (currentHealth + amount <= 0)
        {
            currentHealth = 0;
            return true;    //sets BattleSystem's isDead variable to true, triggers NPC death
        }
        else if (currentHealth + amount > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }

        return false;   //sets BattleSystem's isDead var to false so continues the battle
    }

    //associate with character specific attacks and skills
    //either add a dictionary per npc in here
    //OR somehow create an object (like a scriptable object or smth) that can be reused for each npc whil customizing the data

    //have a list of attacks associated with npc (keep empty)
    //have a way to add elements from the list to the dictionary
    //should be done on awake
}
