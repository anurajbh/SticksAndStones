using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public PanelScript options;
    public PanelScript speech;

    private Queue<string> sentences = new Queue<string>();
    // Start is called before the first frame update
    void Start()
    {

    }

    public void startDialogue(Dialogue dialogue)
    {
        options.hide();
        speech.show();
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
        speech.hide();
        options.show();
        Debug.Log("End of Dialogue");
    }

}
