using System;
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
    PanelScript attack;
    PanelScript skills;
    PanelScript items;
    PanelScript options;

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
        attack = GameObject.Find("Attack sub").GetComponent<PanelScript>();
        skills = GameObject.Find("Skill sub").GetComponent<PanelScript>();
        items = GameObject.Find("Item sub").GetComponent<PanelScript>();
        options = GameObject.Find("Options").GetComponent<PanelScript>();
    }

    public void TriggerStats()
    {
        switch (button.name)
        {
            case "Attack 1":
                NPCEntity.damageEntityStat1(PlayerStats.playerDPS1);//player attacks with attack1
                playerTurn.playerTurn = false;
                NPCEntity.NPCTurn = true;
                break;
            case "Attack 2":
                NPCEntity.damageEntityStat1(PlayerStats.playerDPS1);//player attacks with attack1
                playerTurn.playerTurn = false;
                NPCEntity.NPCTurn = true;
                break;
            case "Attack 3":
                NPCEntity.damageEntityStat1(PlayerStats.playerDPS1);//player attacks with attack1
                playerTurn.playerTurn = false;
                NPCEntity.NPCTurn = true;
                break;
            case "Attack 4":
                NPCEntity.damageEntityStat1(PlayerStats.playerDPS1);//player attacks with attack1
                playerTurn.playerTurn = false;
                NPCEntity.NPCTurn = true;
                break;

            case "Skill 1":
                NPCEntity.damageEntityStat1(PlayerStats.playerDPS1);//player attacks with attack1
                playerTurn.playerTurn = false;
                NPCEntity.NPCTurn = true;
                break;
            case "Skill 2":
                NPCEntity.damageEntityStat1(PlayerStats.playerDPS1);//player attacks with attack1
                playerTurn.playerTurn = false;
                NPCEntity.NPCTurn = true;
                break;
            case "Skill 3":
                NPCEntity.damageEntityStat1(PlayerStats.playerDPS1);//player attacks with attack1
                playerTurn.playerTurn = false;
                NPCEntity.NPCTurn = true;
                break;
            case "Skill 4":
                NPCEntity.damageEntityStat1(PlayerStats.playerDPS1);//player attacks with attack1
                playerTurn.playerTurn = false;
                NPCEntity.NPCTurn = true;
                break;

            case "Item 1":
                NPCEntity.damageEntityStat1(PlayerStats.playerDPS1);//player attacks with attack1
                playerTurn.playerTurn = false;
                NPCEntity.NPCTurn = true;
                break;
            case "Item 2":
                NPCEntity.damageEntityStat1(PlayerStats.playerDPS1);//player attacks with attack1
                playerTurn.playerTurn = false;
                NPCEntity.NPCTurn = true;
                break;
            case "Item 3":
                NPCEntity.damageEntityStat1(PlayerStats.playerDPS1);//player attacks with attack1
                playerTurn.playerTurn = false;
                NPCEntity.NPCTurn = true;
                break;
            case "Item 4":
                NPCEntity.damageEntityStat1(PlayerStats.playerDPS1);//player attacks with attack1
                playerTurn.playerTurn = false;
                NPCEntity.NPCTurn = true;
                break;
        }
    }

    public void SwitchPanels()
    {
        options.hide();
        switch (button.name)
        {
            case "Hurt":
                attack.show();
                skills.hide();
                items.hide();
                break;
            case "Consume":
                attack.hide();
                skills.hide();
                items.show();
                break;
            case "Protect":
                attack.hide();
                skills.show();
                items.show();
                break;
            case "Run":
                playerTurn.playerTurn = false;
                NPCEntity.NPCTurn = true;
                break;
        }
    }
}
