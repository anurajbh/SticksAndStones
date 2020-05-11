using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SMPlayerTurn : MonoBehaviour
{
    int index = 0;
    int totalOptions = 4;
    int totalChoices = 3;
    readonly float cxOffset = 200.93f;
    readonly float cyOffset = 120.59f;
    readonly float dyOffset = 40f;
    Vector2 position;
    Button button0;
    Button button1;
    Button button2;
    Button button3;
    Button Continue;
    Image playerNav;

    SMPlayerStats player;

    PanelScript attack;
    PanelScript skills;
    PanelScript items;
    PanelScript options;
    PanelScript choices;
    CanvasGroup parent;
    //SMDialogueTrigger displayStat;

    void Awake()
    {
        parent = GameObject.Find("DialogueSystem").GetComponent<CanvasGroup>();
        parent.alpha = 0;
        player = GameObject.FindWithTag("Player").GetComponent<SMPlayerStats>();
        //dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        //Continue = GameObject.Find("Continue").GetComponent<Button>();
        

    }

    // Update is called once per frame
    void Update()
    {
        playerNav = GameObject.Find("PlayerNav").GetComponent<Image>();
        attack = GameObject.Find("Attack sub").GetComponent<PanelScript>();
        skills = GameObject.Find("Skill sub").GetComponent<PanelScript>();
        items = GameObject.Find("Item sub").GetComponent<PanelScript>();
        options = GameObject.Find("Options").GetComponent<PanelScript>();
        choices = GameObject.Find("Choices").GetComponent<PanelScript>();

        options.hide();
        attack.hide();
        skills.hide();
        items.hide();
        playerNav.color = new Color(playerNav.color.r, playerNav.color.g, playerNav.color.b, 0f);
        switch (player.getState())
        {
            case Transitions.ProcessState.playerTurn:
                parent.alpha = 1;
                playerNav.color = new Color(playerNav.color.r, playerNav.color.g, playerNav.color.b, 1f);
                options.show();
                ScrollThroughOptions();
                CheckForSubOption();
                break;
            case Transitions.ProcessState.attackSub:
                button0 = attack.gameObject.transform.Find("Attack 1").GetComponent<Button>();
                button1 = attack.gameObject.transform.Find("Attack 2").GetComponent<Button>();
                button2 = attack.gameObject.transform.Find("Attack 3").GetComponent<Button>();
                button3 = attack.gameObject.transform.Find("Attack 4").GetComponent<Button>();
                ScrollThroughOptions();             //should make this able to scroll up and down with more than 4 items
                CheckForAction("attack");
                break;
            case Transitions.ProcessState.skillSub:
                button0 = skills.gameObject.transform.Find("Skill 1").GetComponent<Button>();
                button1 = skills.gameObject.transform.Find("Skill 2").GetComponent<Button>();
                button2 = skills.gameObject.transform.Find("Skill 3").GetComponent<Button>();
                button3 = skills.gameObject.transform.Find("Skill 4").GetComponent<Button>();
                ScrollThroughOptions();
                CheckForAction("skill");
                break;
            case Transitions.ProcessState.itemSub:
                button0 = items.gameObject.transform.Find("Item 1").GetComponent<Button>();
                button1 = items.gameObject.transform.Find("Item 2").GetComponent<Button>();
                button2 = items.gameObject.transform.Find("Item 3").GetComponent<Button>();
                button3 = items.gameObject.transform.Find("Item 4").GetComponent<Button>();
                ScrollThroughOptions();
                CheckForAction("item");
                break;
            case Transitions.ProcessState.dialogueChoice:
                playerNav.color = new Color(playerNav.color.r, playerNav.color.g, playerNav.color.b, 1f);
                choices.show();
                ScrollThroughChoices();
                break;
            default:
                break;
        }
    }

    public char CheckForChoice()
    {
        char choice = 'a';
        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (index)
            {
                case 0:
                    choice = 'a';
                    break;
                case 1:
                    choice = 'b';
                    break;
                case 2:
                    choice = 'c';
                    break;
                default:
                    break;
            }
            choices.hide();
            playerNav.color = new Color(playerNav.color.r, playerNav.color.g, playerNav.color.b, 0f);
        }
        return choice;
    }

    private void ScrollThroughChoices()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (index < totalChoices)
            {
                index++;
                position = transform.position;
                position.y -= dyOffset;
                transform.position = position;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (index > 0)
            {
                index--;
                position = transform.position;
                position.y += dyOffset;
                transform.position = position;
            }
        }
    }

    private void CheckForAction(string action)
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            options.show();
            attack.hide();
            skills.hide();
            items.hide();
            player.switchState(Transitions.Command.back);
        }

        Text text;
        string name;
        Action move = new Attacks();
        (int, int, int) stats = (0, 0, 0);

        switch (action)
        {
            case "attack":
                break;
            case "skill":
                move = new Skills();
                break;
            case "item":
                //move = new Items();
                break;
            default:
                break;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //gonna need a way to scroll down for when there are more than 4 options
            switch (index)
            {
                case 0:
                    text = button0.GetComponentInChildren<Text>();
                    name = text.ToString();
                    //displayStat = button0.GetComponent<SMDialogueTrigger>();
                    stats = move.Use(name);
                    break;
                case 1:
                    text = button1.GetComponentInChildren<Text>();
                    name = text.ToString();
                    //displayStat = button1.GetComponent<SMDialogueTrigger>();
                    stats = move.Use(name);
                    break;
                case 2:
                    text = button2.GetComponentInChildren<Text>();
                    name = text.ToString();
                    //displayStat = button2.GetComponent<SMDialogueTrigger>();
                    stats = move.Use(name);
                    break;
                case 3:
                    text = button3.GetComponentInChildren<Text>();
                    name = text.ToString();
                    //displayStat = button3.GetComponent<SMDialogueTrigger>();
                    stats = move.Use(name);
                    break;
                default:
                    break;
            }

            playerNav.color = new Color(playerNav.color.r, playerNav.color.g, playerNav.color.b, 0f);
            string[] msg = new string[] { "Your anixety changed by " + stats.Item1 +
                "!\nYour will changed by " + stats.Item2 + "!\nYou dealt " + stats.Item3 +
                " damage to the enemy!"};
            //displayStat.TriggerDialogue(new Dialogue("", msg));
            //SMDialogueTrigger.turn = 2;
            player.switchState(Transitions.Command.playerChoice);
        }
    }

    private void CheckForSubOption()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (index)
            {
                case 0:
                    options.hide();
                    attack.show();
                    skills.hide();
                    items.hide();
                    //totalOptions = Attacks.GetSize();
                    player.switchState(Transitions.Command.attackSelect);
                    break;
                case 1:
                    options.hide();
                    items.show();
                    attack.hide();
                    skills.hide();
                    //totalOptions = Items.GetSize();
                    player.switchState(Transitions.Command.itemSelect);
                    break;
                case 2:
                    options.hide();
                    skills.show();
                    attack.hide();
                    items.hide();
                    totalOptions = Skills.GetSize();
                    player.switchState(Transitions.Command.skillSelect);
                    break;
                case 3:
                    options.hide();
                    parent.alpha = 0;
                    player.switchState(Transitions.Command.exitBattle);
                    break;
                default:
                    break;
            }
        }
    }

    private void ScrollThroughOptions()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (index < totalOptions / 2)
            {
                index = index + 2;
                position = transform.position;
                position.y -= cyOffset;
                transform.position = position;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (index > (totalOptions - 1) / 2)
            {
                index = index - 2;
                position = transform.position;
                position.y += cyOffset;
                transform.position = position;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (index % 2 == 0)
            {
                index = index + 1;
                position = transform.position;
                position.x += cxOffset;
                transform.position = position;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (index % 2 != 0)
            {
                {
                    index = index - 1;
                    position = transform.position;
                    position.x -= cxOffset;
                    transform.position = position;
                }
            }
        }
    }
}
