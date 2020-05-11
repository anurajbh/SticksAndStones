using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    //TO DO: have a way to remember the state of the scene you came from so you can go back to it after you're done
    //TO DO: implement run function
    public BattleState state;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    PlayerStats player;
    NPC enemy;

    public Text dialogueText;

    // Battle will take place in a separate scene, the code below
    // will cause the player to be in battle upon scene entry
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab);
        player = playerGO.GetComponent<PlayerStats>();

        GameObject enemyGO = Instantiate(enemyPrefab);
        enemy = enemyGO.GetComponent<NPC>();

        dialogueText.text = "A monster approaches.";

        //initialize HUD
        //enemy hp bar should be similar to player's will bar

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        //bool isDead = enemy.TakeDamage(damage)      this needs to find the appropriate damage from the used move
        //edit enemy HUD
        dialogueText.text = "You used";

        yield return new WaitForSeconds(2f);

        /* if (isDead) {
            state = BattleState.WON;
            EndBattle();
        } else {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        } */
    }

    IEnumerator EnemyTurn()
    {
        //call NPCAI for enemy turn

        yield return new WaitForSeconds(2f);

        //bool isDead = make the player take damage
        //update HUD

        yield return new WaitForSeconds(1f);

        /* if (isDead) {
         *  state = BattleState.LOST;
         *  EndBattle();
         *  } else {
         state = BattleState.PLAYERTURN;
         PlayerTurn();
         */
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You've defeated this monster... for now...";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were never strong enough";
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "What will you do?";
    }

    /* IEnumerator PlayerHeal()
    {
        //heal player
        //update HUD
        //some descriptive dialogue text
        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    } */

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
    }
}
