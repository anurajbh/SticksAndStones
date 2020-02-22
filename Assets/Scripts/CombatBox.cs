using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class CombatBox : MonoBehaviour
{
    public Text dialogue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void hurtEvent()
    {
        dialogue.text = "You're too weak to hurt anyone";

    }

    public void protectEvent()
    {
        dialogue.text = "Your arms are broken";
    }

    public void consumeEvent()
    {
        dialogue.text = "Do you really need that extra cake?";
    }

    public void runEvent()
    {
        dialogue.text = "You can't run from this";
    }

}
