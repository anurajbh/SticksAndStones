using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGiver : Interactable
{
    public DialogueOptions itemPrompt;
    public override void Interact() {
        if (TimeProgression.Instance.myCycle == TimeProgression.Cycle.dusk) {
            DialogueManager.instance.AddDialogue(itemPrompt);
        }
    }

}
