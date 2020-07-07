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
    //TO DO: when loading new scene, make elly be standing next to her bed like she just woke up
    //TO DO: implement checking which day it is for running + stat buffs
    //TO DO: add something to show that you did damage to the enemy

    public BattleState state;
    public GameObject enemyPrefab;
    public BattleHUD enemyHUD;
    public BattleHUD playerHUD;

    InventoryObject inventory;
    GameObject player;
    NPC enemy;
    NPCAI enemyAI;

    List<string> playerOptions = new List<string>(new string[] { "Attacc", "Protecc", "Snacc", "RUN AWAY!!" });
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
        //set up enemy and enemy AI
        // GameObject enemyGO = Instantiate(enemyPrefab);
        GameObject enemyGO = enemyPrefab;
        enemy = enemyGO.GetComponent<NPC>();
        enemyAI = enemyGO.GetComponent<NPCAI>();

        //display starting message
        dialogueBox.gameObject.SetActive(true);
        dialogueText.text = "A monster approaches.";

        //set HUDs
        enemyHUD.SetEnemyHUD(enemy);
        playerHUD.SetPlayerHUD();

        yield return new WaitForSeconds(2f);

        dialogueText.text = "What will you do?";

        yield return new WaitForSeconds(2f);

        dialogueBox.gameObject.SetActive(false);

        //start the battle, move to player turn
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator EnemyTurn()
    {
        Action.whosAttacking = 1; // for Action to know how to deal with will checks for abilities
        (int, int, int) stats = enemyAI.EnemyAttack(); // saving stats like this can be used to make the dialogue more specific when the enemy attacks
       yield return StartCoroutine(DisplayMessage(enemy.enemyName + " attacked! ")); //attack message

        yield return new WaitForSeconds(1f);

        bool isDead = PlayerStats.Instance.adjustWill(stats.Item2); //checks if player is dead post-damage
        enemy.adjustHealth(stats.Item3); //in case it's a self harming move

        enemyHUD.SetEnemyHUD(enemy);
        playerHUD.SetPlayerHUD();

        Action.whosAttacking = 0; //go back to player input for Action

        yield return new WaitForSeconds(1f);

        //check whether the player won or lost
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
            PlayerStats.Instance.adjustWill(prevWill + 1);
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were never strong enough.";
            PlayerStats.Instance.adjustWill(prevWill - 1);
        }

        StartCoroutine(Buffer());
        dialogueBox.gameObject.SetActive(false);
        StartCoroutine(Buffer());
        StartCoroutine(Buffer());
        //need to wait for input here
        TimeProgression.Instance.ChangeTime();

        //transition back to game
        SceneManager.LoadScene("Overworld");
    }

    void PlayerTurn()
    {
        ParseOptions();
    }

    /*
    void EndTurn()
    {
        StartCoroutine(Buffer());
        move = 5;
        ParseOptions();
    } */

    IEnumerator Buffer()
    {
        yield return new WaitForSeconds(2f);
    }

    public void OnAttackButton() // opens attack submenu
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

        moveName = EventSystem.current.currentSelectedGameObject.name; //gets currently selected button for move name

        if (PlayerStats.attacks.Check(moveName)) //if enough will
        {
            //do attack
            (int, int, int) stats = PlayerStats.attacks.Use(moveName); //can be used for more specific attack message

            StartCoroutine(DisplayMessage("You hit it with a " + moveName));
            bool isDead = enemy.adjustHealth(stats.Item3);
            enemyHUD.SetEnemyHUD(enemy);

            //DAMAGE SHAKE STILL NOT WORKING
            StartCoroutine(onDamageShake());
            StartCoroutine(Buffer());

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
        }
        else
        {
           //not enough will message
           StartCoroutine(DisplayMessage("You can't bring yourself to do it, you can't gather the Will"));
        }

        //update playerHUD and move back to enemy turn
        playerHUD.SetPlayerHUD();
        StartCoroutine(Buffer());
        StartCoroutine(Buffer());
        //EndTurn();
        //move = 5; //enemy turn for button choice
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

            StartCoroutine(Buffer());

            DisplayMessage("You used" + moveName);

            StartCoroutine(Buffer());

            bool isDead = enemy.adjustHealth(stats.Item3);
            enemyHUD.SetEnemyHUD(enemy);
            StopAllCoroutines();
            //onDamageShake still not working
            StartCoroutine(onDamageShake());
            StartCoroutine(Buffer());

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
        }
        else
        {
            DisplayMessage("You can't bring yourself to do it, your willpower is low.");
        }
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

       DisplayMessage("You used " + itemSlotToUse.item.name + "!");

       Debug.Log("used item" + itemSlotToUse.item.name);

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
                List<UnityAction> mainActions;
                if (TimeProgression.Instance.daysElapsed == 6) {
                    numOptions = 3;
                    mainActions = new List<UnityAction>(new UnityAction[] { OnAttackButton, OnSkillButton, OnItemButton });
                    buttonFunctions = mainActions;
                    break;
                }
                numOptions = 4;
                mainActions = new List<UnityAction>(new UnityAction[] { OnAttackButton, OnSkillButton, OnItemButton, OnRunButton });
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
                Debug.Log("Here");
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

    IEnumerator onDamageShake()
    {
        Vector2 initialPosition = enemy.transform.position; //save originial position

        yield return new WaitForSeconds(1f);

        for (int i = 0; i <= 4; i++) //get random position and move to it 4 times
        {
            Vector2 newPosition = Random.insideUnitCircle * 2;
            enemy.transform.Translate(newPosition);
            yield return new WaitForSeconds(.1f);
        }

        enemy.transform.position = initialPosition; //restore original position
    }

    IEnumerator DisplayMessage(string msg) 
    {
        dialogueBox.gameObject.SetActive(true);
        dialogueText.text = msg; 
        yield return new WaitForSeconds(2f);
        dialogueBox.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
    }
}
