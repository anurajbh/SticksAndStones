using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogue : MonoBehaviour
{
    public DialogueOptions dialogueOption;
    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.K)){
          DialogueManager.instance.AddDialogue(dialogueOption);
      }   
    }
}
