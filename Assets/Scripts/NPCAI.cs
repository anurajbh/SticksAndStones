using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCAI : MonoBehaviour
{
    public PlayerTurn playerTurn;//I should probably rename that script
    public PlayerStats playerStats;
    public NPCEntity NPCEntity;
    public PlayerTurn playTurn;
    public bool moreDialogue = true;
    Image playerNav;

    void Awake()
    {
        NPCEntity = gameObject.GetComponent<NPCEntity>();
        playerStats = GameObject.Find("PlayerController").GetComponent<PlayerStats>();
        playerTurn = GameObject.Find("PlayerNav").GetComponent<PlayerTurn>();
        playerNav = GameObject.Find("PlayerNav").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForTurn();
    }
    public void EnemyTurn()
    {
        int whatItChooses = Random.Range(1, 5);
        switch (whatItChooses)
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
        moreDialogue = true;
        playerNav.gameObject.SetActive(true);
    }

    public void SwitchTurn()
    {
        NPCEntity.NPCTurn = false;
        playerTurn.playerTurn = true;
    }

    public void CheckForTurn()
    {
        if(!playerTurn.playerTurn)
        {
            if (!moreDialogue)
            {
                EnemyTurn();
            }
        }
    }
}
