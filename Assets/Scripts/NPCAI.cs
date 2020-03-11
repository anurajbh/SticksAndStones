using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAI : MonoBehaviour
{
    public DialogueTrigger playerTurn;//I should probably rename that script
    public PlayerStats playerStats;
    public NPCEntity NPCEntity;
    void Awake()
    {
        NPCEntity = gameObject.GetComponent<NPCEntity>();
        playerStats = GameObject.Find("PlayerController").GetComponent<PlayerStats>();
        playerTurn = GameObject.Find("Hurt").GetComponent<DialogueTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForTurn();
    }
    public void EnemyTurn()
    {
        int whatItChooses = Random.Range(1, 5);
        switch(whatItChooses)
        {
            case 1:
                playerStats.increaseAnxiety(NPCEntity.npcAnxietyAffect);
                print("They scared you!");
                break;
            case 2:
                NPCEntity.healEntityStat1(NPCEntity.npcHeal);
                print("They healed themself");
                break;
            case 3:
                playerStats.decreaseWill(NPCEntity.npcWillAffect);
                print("They attacked you!");
                break;
            case 4:
                NPCEntity.healEntiyStat2(NPCEntity.npcHeal);
                print("Their mental attribute increased");
                break;

        }
        playerTurn.NPCTurn = false;
        playerTurn.PlayerTurn = true;
    }
    public void CheckForTurn()
    {
        if(!playerTurn.PlayerTurn)
        {
            EnemyTurn();
        }
    }
}
