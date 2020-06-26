using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Dialogue Option", menuName = "DialogueOption")]
public class DialogueOptions : DialogueBase
{
    [System.Serializable]
    public class Options
    {
        public DialogueBase nextDialogue;
        public string buttonName;
        public UnityEvent myEvent; //variable that allows access to button events and triggers (as done in DialogueManager)
    }

    public Options[] optionInfo;
}
