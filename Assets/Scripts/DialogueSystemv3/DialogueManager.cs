using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public static bool triggered;
    public DialogueBase next;
    public DialogueBase learningDialogue;

    private bool typing;
    private string completeText;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("fix this: " + gameObject.name);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (triggered)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                NextLine();
            }
        }
    }

    public GameObject display;
    public Text dialogueName;
    public GameObject nameTag;
    public Text dialogueText;
    public Image dialoguePortrait;
    public float delay = 0.01f;
    private int numOptions;

    public Queue<DialogueBase.Info> dialogueInfo = new Queue<DialogueBase.Info>();

    public bool isDialogueOption = false;
    public GameObject optionUI;
    public GameObject[] optionButtons;
    public GameObject dialogueUI;
    private bool buffer;
    [HideInInspector] public bool abilityLearned = false;

    public void AddDialogue(DialogueBase db)
    {
        if (triggered)
        {
            return;
        }

        StartCoroutine(Buffer());

        triggered = true;
        dialogueInfo.Clear();
        display.SetActive(true);
        dialogueUI.SetActive(true);

        ParseOptions(db);

        foreach (DialogueBase.Info info in db.dialogueInfo)
        {
            dialogueInfo.Enqueue(info);
        }

        DequeueDialogue();
    }

    public void DequeueDialogue()
    {
        if (typing)
        {
            if (buffer) return;
            CompleteText();
            StopAllCoroutines();
            typing = false;
            return;
        }

        if (dialogueInfo.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueBase.Info info = dialogueInfo.Dequeue();
        completeText = info.words;
        dialogueText.text = info.words;
        dialoguePortrait.sprite = info.portrait;

        //Adds abilities to learning system
        if (info.abilityToLearn != null) 
        {
            if (info.isAttack) 
            {
                LearningSystem.instance.AddAttack(info.abilityToLearn);
            } else if (info.isSkill) 
            {
                LearningSystem.instance.AddSkill(info.abilityToLearn);
            } else {
                Debug.Log("Please set " + info.abilityToLearn.name + " to be a skill or attack!");
            }
        }

        if (info.charName == "")
        {
            dialogueName.text = "";
            nameTag.SetActive(false);
        }
        else
        {
            dialogueName.text = info.charName;
            nameTag.SetActive(true);
        }

        if (info.nextDialogue != null)
        {
            next = info.nextDialogue;
        }
        else
        {
            next = null;
        }

        dialogueText.text = "";
        StartCoroutine(TypeText(info));
    }

    IEnumerator TypeText(DialogueBase.Info info)
    {
        typing = true;
        dialogueText.text = "";
        foreach(char c in info.words.ToCharArray())
        {
            yield return new WaitForSeconds(delay);
            dialogueText.text += c;
            yield return null;
        }
        typing = false;
    }

    IEnumerator Buffer()
    {
        yield return new WaitForSeconds(0.1f);
        buffer = false;
    }

    private void CompleteText()
    {
        dialogueText.text = completeText;
    }

    public void NextLine()
    {
        DequeueDialogue();
    }

    public void EndDialogue()
    {
        if (!LearningSystem.instance.isAttacksEmpty()) 
        {
            LearningSystem.instance.LearnAttacks();
            abilityLearned = true;
        } else if (!LearningSystem.instance.isSkillsEmpty()) 
        {
            LearningSystem.instance.LearnSkills();
            abilityLearned = true;
        }
        OptionsLogic();
        if (next != null)   
        {
            AddDialogue(next);  
            Debug.Log("triggered");
        }
    }

    private void OptionsLogic()
    {
        if (isDialogueOption)
        {
            optionUI.SetActive(true);
            dialogueUI.SetActive(false);
            
        }
        else
        {
            display.SetActive(false);
            triggered = false;
        }

    }

    public void CloseOptions()
    {
        optionUI.SetActive(false);
    }

    private void ParseOptions(DialogueBase db)
    {
        if (db is DialogueOptions)
        {
            isDialogueOption = true;
            DialogueOptions dialogueOptions = db as DialogueOptions;
            numOptions = dialogueOptions.optionInfo.Length;

            optionButtons[0].GetComponent<Button>().Select();

            for (int i = 0; i < optionButtons.Length; i++)
            {
                optionButtons[i].SetActive(false);
            }

            for (int i = 0; i < numOptions; i++)
            {
                optionButtons[i].SetActive(true);
                optionButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = dialogueOptions.optionInfo[i].buttonName;
                UnityEventHandler handler = optionButtons[i].GetComponent<UnityEventHandler>();
                handler.eventHandler = dialogueOptions.optionInfo[i].myEvent;

                if (dialogueOptions.optionInfo[i].nextDialogue != null)
                {
                    handler.dialogue = dialogueOptions.optionInfo[i].nextDialogue;
                }
                else
                {
                    handler.dialogue = null;
                }
            }
        }
        else
        {
            isDialogueOption = false;
        }
    }

    public void LearnAbilities() {
        learningDialogue.dialogueInfo[0].words = "From this conversation you feel like you've learned:" + LearningSystem.instance.toString();
        abilityLearned = false;
        LearningSystem.instance.clearLists();
        AddDialogue(learningDialogue);
        Debug.Log("learning dialogue activated");
    }
}
