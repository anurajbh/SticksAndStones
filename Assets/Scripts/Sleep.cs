using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : Interactable
{
    public override void Interact() {
        print("Here");
        if(TimeProgression.Instance.isDusk()) {
            TimeProgression.Instance.triggerNightTransition = true;
        }
    }
}
