using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SMDialogueManager : MonoBehaviour
{
    Text nameText;
    Text dialogueText;
    //PanelScript options;
    PanelScript display;
    private Queue<string> sentences = new Queue<string>();
    SMNPCAI nPCAI;
    //Image playerNav;
    bool moreDialogue = false;
    SMPlayerStats player;
    PanelScript nameTag;
    CanvasGroup parent;

    // Start is called before the first frame update
    void Awake()
    {
        parent = GameObject.Find("DialogueSystem").GetComponent<CanvasGroup>();
        parent.alpha = 0;
        nameText = GameObject.Find("Name").GetComponent<Text>(); 
        dialogueText = GameObject.Find("Dialogue").GetComponent<Text>();
        //options = GameObject.Find("Options").GetComponent<PanelScript>();
        display = GameObject.Find("DialoguePanel").GetComponent<PanelScript>();
        nPCAI = GameObject.FindWithTag("NPC").GetComponent<SMNPCAI>();
        //playerNav = GameObject.Find("PlayerNav").GetComponent<Image>();
        player = GameObject.FindWithTag("Player").GetComponent<SMPlayerStats>();
        nameTag = GameObject.FindGameObjectWithTag("nameTag").GetComponent<PanelScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moreDialogue && Input.GetKeyDown(KeyCode.Z))
        {
            DisplayNextSentence();
        }
    }

    public void startDialogue(Dialogue dialogue)
    {
        parent.alpha = 1;
        //options.GetComponent<PanelScript>().hide();
        display.GetComponent<PanelScript>().show();
        //playerNav.gameObject.SetActive(false);
        SMDialogueTrigger.moreDialogue = true;
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
        //DisplayNextSentence();
    }

    public void EndDialogue()
    {
        display.GetComponent<PanelScript>().hide();
        parent.alpha = 0;
        //options.GetComponent<PanelScript>().show();
        //playerNav.gameObject.SetActive(true);
        SMDialogueTrigger.moreDialogue = false;
        player.switchState(Transitions.Command.waitForEnemy);
        Debug.Log("End of Dialogue");
    }

}
