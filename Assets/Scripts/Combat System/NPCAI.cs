using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAI : MonoBehaviour
{
    //TO DO: redo the random number thing because it can't find attacks in skills and vice versa
    public NPC npc;
    List<string> attackList = new List<string>();
    List<string> skillList = new List<string>();
    //public NPCAI instance; //singleton

    void Awake()
    {
        //set singleton class
        /*if (instance != null)
        {
            Debug.LogWarning("fix this: " + gameObject.name);
        }
        else
        {
            instance = this;
        }*/

        npc = gameObject.GetComponent<NPC>(); //Get NPC

        //add attacks and skills to moveList
        foreach (string key in npc.attacks.attacks.Keys)
        {
            attackList.Add(key);
        }
        foreach (string key in npc.skills.skills.Keys)
        {
            skillList.Add(key);
        }
    }

    public (int, int, int) EnemyAttack()
    {
        (int, int, int) stats = (0, 0, 0); //stats for display message
        int whatItChooses;

        int whichList = Random.Range(1, 3);
        if (whichList == 1)
        {
            whatItChooses = Random.Range(1, attackList.Count + 1);    //randomly picks a number within range of the attackList length
            stats = npc.attacks.Use(attackList[whatItChooses - 1]); //uses move based on random number
        }
        else
        {
            whatItChooses = Random.Range(1, skillList.Count + 1);    //randomly picks a number within range of the attackList length
            stats = npc.attacks.Use(attackList[whatItChooses - 1]); //uses move based on random number
        }
        
        
        //set and display status message
        string msg = "Your anixety changed by " + stats.Item1 +
                "!\nYour will changed by " + stats.Item2 + "!";
        //DialogueBase statMsg = ScriptableObject.CreateInstance(typeof(DialogueBase)) as DialogueBase;
        //statMsg.init(msg);
        //Trigger.dialogue = statMsg;
        //Trigger.TriggerDialogue();
        return stats;
    }
}
