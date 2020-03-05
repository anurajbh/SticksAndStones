using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public GameObject options;
    public GameObject speech;

    private Queue<string> sentences = new Queue<string>();
    // Start is called before the first frame update
    void Awake()
    {
      
    }

    public void startDialogue(Dialogue dialogue)
    {
        options.GetComponent<PanelScript>().hide();
        speech.GetComponent<PanelScript>().show();
        nameText.text = dialogue.name;
        Debug.Log("Started " + dialogue.name + "'s dialogue");

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        dialogueText.text = sentence;
        DisplayNextSentence();
    }

    public void EndDialogue()
    {
        speech.GetComponent<PanelScript>().hide();
        options.GetComponent<PanelScript>().show();
        Debug.Log("End of Dialogue");
    }

}
