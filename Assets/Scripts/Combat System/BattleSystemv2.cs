using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST};

public class BattleSystemv2 : MonoBehaviour
{
    /*public BattleState state;
    public GameObject enemyPrefab;
    public BattleHUD enemyHUD;
    public BattleHUD playerHUD;

    InventoryObject inventory;
    GameObject player;
    NPC enemy;
    NPCAI enemyAI;

    List<string> playerOptions = new List<string>(new string[] { "Attack", "Skills", "Use Item", "Run Away" });
    int move = 0;
    //0 = main menu, 1 = attack menu, 2 = skill menu, 3 = inventory, 4 = run, 5 = enemy turn

    public Text dialogueText;
    public Image dialogueBox;
    public GameObject[] optionButtons;
    string moveName;

    //public int prevWill; //what does this do?

    private void Start()
    {
        state = BattleState.START;

        //prevWill = PlayerStats.Instance.totalWill;

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
        if (inventory != null)
        {
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

        yield return new WaitForSeconds(2f);

        dialogueText.text = "What will you do?";

        yield return new WaitForSeconds(2f);

        dialogueBox.gameObject.SetActive(false);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        ParseOptions();
    }

    private void ParseOptions()
    {
        int numOptions = 4; //sets default numOptions
        List<string> buttonNames = playerOptions; //sets default button names
        List<UnityAction> buttonFunctions = new List<UnityAction>(); //to store button functions

        switch (move)
        {
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
                numOptions = inventory.Container.Count - 1 + 1;
                buttonNames = new List<string>();
                for (int i = 0; i < inventory.Container.Count; i++)
                {
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

        optionButtons[0].GetComponent<Button>().Select(); //auto selects first button but no visual

        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].SetActive(false); //no buttons showing
        }

        for (int i = 0; i < numOptions; i++)
        {
            optionButtons[i].GetComponent<Button>().onClick.RemoveAllListeners(); //clear previous options
            optionButtons[i].SetActive(true); //shows just the number of options
            optionButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = buttonNames[i]; //sets button text
            optionButtons[i].GetComponent<Button>().name = buttonNames[i]; //sets button names
            optionButtons[i].GetComponent<Button>().onClick.AddListener(buttonFunctions[i]); //changes button function
        }
        moveName = EventSystem.current.currentSelectedGameObject.name; //updates your action based on what you currently have selected
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

    public void OnAttackSelect()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        //moveName = EventSystem.current.currentSelectedGameObject.name;
        if (PlayerStats.attacks.Check(moveName))
        {
            (int, int, int) stats = PlayerStats.attacks.Use(moveName);
            StartCoroutine(Buffer());

            dialogueBox.gameObject.SetActive(true);
            dialogueText.text = "You hit it with a " + moveName;
            bool isDead = enemy.adjustHealth(stats.Item3);
            enemyHUD.SetEnemyHUD(enemy);

            StartCoroutine(Buffer());
            dialogueBox.gameObject.SetActive(false);

            //StopAllCoroutines();
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
            dialogueBox.gameObject.SetActive(true);
            dialogueText.text = "You can't bring yourself to do it, you can't gather the Will";
            StartCoroutine(Buffer());
            dialogueBox.gameObject.SetActive(false);
        }

        playerHUD.SetPlayerHUD();
        StartCoroutine(Buffer());
        move = 5; //enemy turn for button choice
        ParseOptions();
    }*/
}
