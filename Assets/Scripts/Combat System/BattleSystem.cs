using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    //TO DO: have a way to remember the state of the scene you came from so you can go back to it after you're done
    //TO DO: implement Item use with inventory
    //TO DO: implement checking which day it is for running + stat buffs
    //TO DO: add something to show that you did damage to the enemy

    public BattleState state;
    public GameObject enemyPrefab;
    public BattleHUD enemyHUD;
    public BattleHUD playerHUD;

    //PlayerStats player = PlayerStats.Instance;
    InventoryObject inventory;
    GameObject player;
    NPC enemy;
    NPCAI enemyAI;

    List<string> playerOptions = new List<string>(new string[] {"Attacc", "Protecc", "Snacc", "RUN AWAY!!"});
    int move = 0; //this is to indicate whether the player is in the main menu or a submenu
    //0 = main menu, 1 = attack menu, 2 = skill menu, 3 = inventory, 4 = run, 5 = enemy

    public Text dialogueText;
    public Image dialogueBox;
    public GameObject[] optionButtons;

    public int prevWill;

    // Battle will take place in a separate scene, the code below
    // will cause the player to be in battle upon scene entry
    void Start()
    {
        state = BattleState.START;

        prevWill = PlayerStats.Instance.totalWill;
        //these are temporary so the player has some moves to look through
        PlayerStats.attacks.Learn("stick", 1, -2, -3);
        PlayerStats.attacks.Learn("stone", 2, -3, -4);
        PlayerStats.skills.Learn("cry", 1, -3, 0);
        PlayerStats.skills.Learn("yell", -3, -1, 0);
        PlayerStats.skills.Learn("aa", 2, 3, 2);
        PlayerStats.skills.Learn("h", 0, 0, 0);
        //Finds the inventory in the scene
        player = GameObject.FindWithTag("Player");
        InventoryManager inventoryManager = player.GetComponent<InventoryManager>();
        inventory = inventoryManager.inventory;
        if (inventory != null) {
            Debug.Log("Inventory get");
        }
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject enemyGO = Instantiate(enemyPrefab);
        enemy = enemyGO.GetComponent<NPC>();
        enemyAI = enemyGO.GetComponent<NPCAI>();

        dialogueBox.gameObject.SetActive(true);
        dialogueText.text = "A monster approaches.";

        enemyHUD.SetEnemyHUD(enemy);
        playerHUD.SetPlayerHUD(); 

        yield return new WaitForSeconds(1f);

        dialogueText.text = "What will you do?";

        yield return new WaitForSeconds(1f);

        dialogueBox.gameObject.SetActive(false);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator EnemyTurn()
    {
        Action.whosAttacking = 1;
        (int, int, int) stats = enemyAI.EnemyAttack();
        dialogueBox.gameObject.SetActive(true);
        dialogueText.text = enemy.enemyName + " attacked! ";

        yield return new WaitForSeconds(1f);

        bool isDead = PlayerStats.Instance.adjustWill(stats.Item2);
        enemy.adjustHealth(stats.Item3);

        enemyHUD.SetEnemyHUD(enemy);
        playerHUD.SetPlayerHUD();
        
        Action.whosAttacking = 0;

        yield return new WaitForSeconds(1f);
        dialogueBox.gameObject.SetActive(false);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            move = 0;
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        dialogueBox.gameObject.SetActive(true);
        
        if (state == BattleState.WON)
        {
            dialogueText.text = "You've defeated this monster... for now...";
            PlayerStats.Instance.adjustWill(prevWill+1);
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were never strong enough.";
            PlayerStats.Instance.adjustWill(prevWill-1);
        }

        TimeProgression.Instance.ChangeTime();

        StartCoroutine(Buffer());
        StartCoroutine(Buffer());

        //transition back to game
        SceneManager.LoadScene("Overworld");
    }

    void PlayerTurn()
    {
        ParseOptions();
    }

    IEnumerator Buffer()
    {
        yield return new WaitForSeconds(1f);
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        move = 1;
        ParseOptions();
    }

    string moveName;

    public void OnAttackSelect()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        moveName = EventSystem.current.currentSelectedGameObject.name;
        if (PlayerStats.attacks.Check(moveName))
        {
            (int, int, int) stats = PlayerStats.attacks.Use(moveName);
            //have the enemy shake or something to show that you did damage too
            //display hit message?

            StartCoroutine(Buffer());

            bool isDead = enemy.adjustHealth(stats.Item3);
            enemyHUD.SetEnemyHUD(enemy);

            StartCoroutine(Buffer());

            if (isDead) {
                state = BattleState.WON;
                EndBattle();
            } /*else {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            } */
        }
        else
        {
            dialogueBox.gameObject.SetActive(true);
            dialogueText.text = "You can't bring yourself to do it, your willpower is low.";
        }
        dialogueBox.gameObject.SetActive(false);
        playerHUD.SetPlayerHUD();
        StartCoroutine(Buffer());
        move = 5;
        ParseOptions();
    }

    public void OnSkillButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        move = 2;
        ParseOptions();
    }

    public void OnSkillSelect()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        moveName = EventSystem.current.currentSelectedGameObject.name;
        if (PlayerStats.skills.Check(moveName))
        {
            (int, int, int) stats = PlayerStats.skills.Use(moveName);
            //have the enemy shake or something to show that you did damage too
            //display hit message?
            //state = BattleState.ENEMYTURN;
        }
        else
        {
            dialogueBox.gameObject.SetActive(true);
            dialogueText.text = "You can't bring yourself to do it, your willpower is low.";
        }
        dialogueBox.gameObject.SetActive(false);
        playerHUD.SetPlayerHUD();
        StartCoroutine(Buffer());
        move = 5;
        ParseOptions();

    }

    public void OnItemButton()
    {
       if (state != BattleState.PLAYERTURN)
       {
            return;
       }
        move = 3;
        ParseOptions();
    }

    public void OnItemSelect() 
    {
        if (state != BattleState.PLAYERTURN)
       {
            return;
       }
       moveName = EventSystem.current.currentSelectedGameObject.name;
       InventorySlot itemSlotToUse = inventory.findItemWithName(moveName);
       inventory.UseItem(itemSlotToUse.item,player);
       inventory.RemoveItem(inventory.Container.IndexOf(itemSlotToUse));

       dialogueBox.gameObject.SetActive(true);
       dialogueText.text = "You used " + itemSlotToUse.item.name + "!";

       Debug.Log("used item" + itemSlotToUse.item.name);

       dialogueBox.gameObject.SetActive(false);
       playerHUD.SetPlayerHUD();
       StartCoroutine(Buffer());
       move = 5;
       ParseOptions();
    }

    public void OnRunButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        //if specific day, not allowed to run

        move = 4;
        ParseOptions();
    }

    public void OnRunConfirm()
    {
        state = BattleState.LOST;
        EndBattle();
    }

    public void OnStayConfirm()
    {
        move = 0;
        ParseOptions();
    }


    private void ParseOptions()
    {
        int numOptions = 4;
        List<string> buttonNames = playerOptions;
        List<UnityAction> buttonFunctions = new List<UnityAction>();

        switch (move) {
            case 0:
                numOptions = 4;
                List<UnityAction> mainActions = new List<UnityAction>(new UnityAction[] { OnAttackButton, OnSkillButton, OnItemButton, OnRunButton });
                buttonFunctions = mainActions;
                break;
            case 1:
                numOptions = PlayerStats.attacks.GetSize();
                buttonNames = new List<string>();
                foreach (string key in PlayerStats.attacks.attacks.Keys)
                {
                    buttonNames.Add(key);
                }
                for (int i = 0; i <= 4; i++)
                {
                    buttonFunctions.Add(OnAttackSelect);
                }
                break;
            case 2:
                numOptions = PlayerStats.skills.GetSize();
                buttonNames = new List<string>();
                foreach (string key in PlayerStats.skills.skills.Keys)
                {
                    buttonNames.Add(key);
                }
                for (int i = 0; i <= 4; i++)
                {
                    buttonFunctions.Add(OnSkillSelect);
                }
                break;
            case 3:
                //GET INVENTORY COUNT AND NAMES 
                numOptions = inventory.Container.Count - 1 + 1;
                buttonNames = new List<string>();
                for (int i = 0; i < inventory.Container.Count; i++) {
                    buttonNames.Add(inventory.Container[i].item.itemName);
                }
                for (int i = 0; i <= 4; i++)
                {
                    buttonFunctions.Add(OnItemSelect);
                }
                break;
            case 4:
                numOptions = 2;
                buttonNames = new List<string>(new string[] { "Run", "Stay" });
                buttonFunctions.Add(OnRunConfirm);
                buttonFunctions.Add(OnStayConfirm);
                break;
            case 5:
                numOptions = 0;
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
                break;
        }
        

        optionButtons[0].GetComponent<Button>().Select(); //has the first button automatically selected (won't be highlighted until you move the cursor)

        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].SetActive(false); //makes sure no buttons are showing
        }

        for (int i = 0; i < numOptions; i++) 
        {
            optionButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
            optionButtons[i].SetActive(true); //activates only the required number of options
            optionButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = buttonNames[i]; //sets button names
            optionButtons[i].GetComponent<Button>().name = buttonNames[i];
            optionButtons[i].GetComponent<Button>().onClick.AddListener(buttonFunctions[i]); //changes button functions
        }

        moveName = EventSystem.current.currentSelectedGameObject.name;
    }
}
