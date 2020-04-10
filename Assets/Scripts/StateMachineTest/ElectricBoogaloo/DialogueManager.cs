using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    Text nameText;
    Text dialogueText;
    PanelScript display;
    private Queue<string> sentences = new Queue<string>();
    NPCAI nPCAI;
    public static bool moreDialogue = false;
    PlayerStats player;
    PanelScript nameTag;
    CanvasGroup parent;
    public enum Caller
    {
        regDialogue,
        playerB,
        enemyB,
        nar
    };
    public static Caller calledBy = Caller.regDialogue;

    void Awake()
    {
        parent = GameObject.Find("DialogueSystem").GetComponent<CanvasGroup>();
        parent.alpha = 0;
        nameText = GameObject.Find("Name").GetComponent<Text>();
        dialogueText = GameObject.Find("Dialogue").GetComponent<Text>();
        display = GameObject.Find("DialoguePanel").GetComponent<PanelScript>();
        nPCAI = GameObject.FindWithTag("NPC").GetComponent<NPCAI>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        nameTag = GameObject.FindWithTag("nameTag").GetComponent<PanelScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moreDialogue && Input.GetKeyDown(KeyCode.Z))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        parent.alpha = 1;
        display.show();
        moreDialogue = true;

        if (nameText.text.Equals(""))
        {
            nameTag.hide();
        }
        else
        {
            nameTag.show();
            nameText.text = dialogue.name;
        }

        Debug.Log("Started " + dialogue.name + "'s dialogue");

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        moreDialogue = true;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            moreDialogue = false;
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        dialogueText.text = sentence;
    }

    public void EndDialogue()
    {
        display.hide();
        parent.alpha = 0;
        if (calledBy == Caller.playerB)
        {
            player.switchState(Transitions.Command.waitForEnemy);
        }
        else if (calledBy == Caller.enemyB)
        {
            player.switchState(Transitions.Command.waitForPlayer);
        }
        else if (calledBy == Caller.regDialogue)
        {
            player.switchState(Transitions.Command.waitForChoice);
        }
        Debug.Log("End of dialogue");
    }
}
