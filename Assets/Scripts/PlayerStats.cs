using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float anxiety = 3f;
    public float intrinsicW = 15f;
    public float ambientW = 0;
    public float will = 5f;
    public bool day = true;
    public float playerDPS1 = 10;//placeholder name for attack1
    public float playerDPS2 = 10;//placeholder name for attack2
    public Transitions.Process state = new Transitions.Process();

    private void Update()
    {
        will = intrinsicW + ambientW;
        if (anxiety >= 6)
        {
            panicAttack();
        }
    }

    public void increaseAnxiety(float amount)
    {
        anxiety += amount;
    }

    public void increaseWill(float amount)
    {
        ambientW += amount;
    }

    public void decreaseAnxiety(float amount)
    {
        anxiety -= amount;
    }

    public void decreaseWill(float amount)
    {
        if (ambientW > amount)
        {
            ambientW -= amount;
        }
        else
        {
            amount -= ambientW;
            intrinsicW -= amount;
        }
    }

    private void panicAttack()
    {
        if (day)
        {
            ambientW = 0;
        }
        else
        {
            //disable all skills
        }
    }

    public void switchState(Transitions.Command command)
    {
        state.MoveNext(command);
    }

    public Transitions.ProcessState getState()
    {
        return state.CurrentState;
    }
}

