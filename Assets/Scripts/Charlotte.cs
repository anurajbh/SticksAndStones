using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charlotte : NPC
{
    //TO DO: replace all commented lines with the correct code for this to function

    //DialogueTrigger trigger;
    //PlayerTurn playerTurn;
    int eventCounter = 1;
    int dialogueCounter = 1;
    private bool convo = false;
    public static char dialogueChoice;
    CanvasGroup parent;

    static Dictionary<string, (string, int, int)> attacks = new Dictionary<string, (string, int, int)>
    {
        { "Woeful Screech", ("The monster lets out a high pitched, deafening screech", 2, -1) },
        { "Speechless Gambit", ("The monster hurls a megaphone at you", 0, -2) },
    };

    private void Awake()
    {
        //player = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        //enemy = GameObject.FindWithTag("NPC").GetComponent<NPCEntity>();
        //trigger = GetComponent<DialogueTrigger>();
        //playerTurn = GameObject.FindWithTag("Player").GetComponent<PlayerTurn>();
        parent = GameObject.Find("DialogueSystem").GetComponent<CanvasGroup>();
    }



    /*public override (int, int) Use(string moveName)
    {
        int anxiety = attacks[moveName].Item2;
        int will = attacks[moveName].Item3;
        //player.adjustAnxiety(anxiety);
        //player.adjustWill(will);
        string[] msg = new string[] { attacks[moveName].Item1 };
        //DialogueTrigger.TriggerDialogue(new Dialogue("", msg));
       // player.switchState(Transitions.Command.enemyChoice);
        return (anxiety, will);
    }

    public override void Converse()
    {
        convo = true;
    }*/

    
}
