using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : Interactable
{
    public override void Interact() {
        if(TimeProgression.Instance.isDusk()) {
            TimeProgression.Instance.triggerNightTransition = true;
        }
    }
}
