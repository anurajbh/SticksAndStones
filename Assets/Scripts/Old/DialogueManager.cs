using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    Text NameText;
    Text DialogueText;
    //public GameObject Options;
    //public GameObject Speech;
    PanelScript Options;
    PanelScript Speech;
    private Queue<string> sentences = new Queue<string>();
    NPCAI nPCAI;
    Image playerNav;
    public bool moreDialogue = false;
    
    void Awake()
    {
        Options = GameObject.Find("Options").GetComponent<PanelScript>();
        Speech = GameObject.Find("DialoguePanel").GetComponent<PanelScript>();
        NameText = GameObject.Find("Name").GetComponent<Text>();
        DialogueText = GameObject.Find("Dialogue").GetComponent<Text>();
        nPCAI = GameObject.Find("NPC").GetComponent<NPCAI>();
        playerNav = GameObject.Find("PlayerNav").GetComponent<Image>();
    }

    private void Update()
    {
        if (moreDialogue && Input.GetKeyDown(KeyCode.Z)) {
            DisplayNextSentence();
        }
    }

    public void startDialogue(Dialogue dialogue)
    {
        Options.GetComponent<PanelScript>().hide();
        Speech.GetComponent<PanelScript>().show();
        playerNav.gameObject.SetActive(false);
        NameText.text = dialogue.name;
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
        DialogueText.text = sentence;
        //DisplayNextSentence();
    }

    public void EndDialogue()
    {
        nPCAI.EnemyTurn();
        nPCAI.SwitchTurn();
        Speech.GetComponent<PanelScript>().hide();
        Options.GetComponent<PanelScript>().show();
        playerNav.gameObject.SetActive(true);
        Debug.Log("End of Dialogue");
    }

}
