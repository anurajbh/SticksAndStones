using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues")]
public class DialogueBase : ScriptableObject
{
    [System.Serializable]
    public class Info
    {
        public DialogueBase nextDialogue;
        public string charName;
        public Sprite portrait;
        [TextArea(4, 10)]
        public string words;
        //public Action toLearn;    need to double check how to do these
    }

    [Header("Insert dialogue info below")]
    public Info[] dialogueInfo;
}
