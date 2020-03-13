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
    public bool PlayerTurn = true;
    public bool NPCTurn = false;
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().startDialogue(dialogue);
    }
    private void Awake()
    {
        PlayerStats = GameObject.Find("PlayerController").GetComponent<PlayerStats>();
        NPCEntity = GameObject.Find("NPC").GetComponent<NPCEntity>();
        button = gameObject.GetComponent<Button>();
    }
    public void TriggerStats()
    {
        switch(button.name)
        {
            case "Hurt":
                NPCEntity.damageEntityStat1(PlayerStats.playerDPS1);//player attacks with attack1
                PlayerTurn = false;
                NPCTurn = true;
                break;
                //TODO- Check for button names and cause stuff to happen to Player Anxiety and Will depending on buttonname 
                //TODO- Check for other cases

        }
    }
}
