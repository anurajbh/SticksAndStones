using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    Button button;
    PlayerStats PlayerStats;
    NPCEntity NPCEntity;
    public PlayerTurn playerTurn;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().startDialogue(dialogue);
    }
    private void Awake()
    {
        PlayerStats = GameObject.Find("PlayerController").GetComponent<PlayerStats>();
        NPCEntity = GameObject.Find("NPC").GetComponent<NPCEntity>();
        button = gameObject.GetComponent<Button>();
        playerTurn = GameObject.Find("PlayerNav").GetComponent<PlayerTurn>();
    }
    public void TriggerStats()
    {
        switch(button.name)
        {
            case "Hurt":
                NPCEntity.damageEntityStat1(PlayerStats.playerDPS1);//player attacks with attack1
                playerTurn.playerTurn = false;
                NPCEntity.NPCTurn = true;
                break;
            //TODO- Check for button names and cause stuff to happen to Player Anxiety and Will depending on buttonname 
            //TODO- Check for other cases
            case "Consume":
                playerTurn.playerTurn = false;
                NPCEntity.NPCTurn = true;
                break;
            case "Protect":
                playerTurn.playerTurn = false;
                NPCEntity.NPCTurn = true;
                break;
            case "Run":
                playerTurn.playerTurn = false;
                NPCEntity.NPCTurn = true;
                break;
        }
    }
}
