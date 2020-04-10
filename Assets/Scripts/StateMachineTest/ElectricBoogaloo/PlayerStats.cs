using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //TO DO: imlpement Blackout() and Overload(), figure out locking skills 
    //and reducing attack while (panic)

    TimeProgression.cycle time;
    TimeProgression Time;
    public int anxiety = 3;
    readonly int maxAnxiety = 10;
    int ambientW = 15;
    int intrinsicW = 0;
    readonly int maxAmbient = 10;
    readonly int maxIntrinsic = 15;
    bool overloaded = false;
    bool panic = false;
    public int will = 15;
    Transitions.Process state = new Transitions.Process();

    // Start is called before the first frame update
    void Awake()
    {
        Time = GameObject.FindWithTag("Time").GetComponent<TimeProgression>();
        time = Time.GetTime();
        will = ambientW + intrinsicW;
    }

    // Update is called once per frame
    void Update()
    {
        while (panic)
        {
            while (ambientW > 0)
            {
                if (ambientW - 2 < 0)
                {
                    ambientW = 0;
                    if (time == TimeProgression.cycle.dawn || time == TimeProgression.cycle.noon)
                    {
                        Blackout();
                    }
                }
                else
                {
                    ambientW = ambientW - 2;
                }
            }
        }
    }

    void PanicAttack()
    {
        if (overloaded)
        {
            overloaded = false;
        }
        //seal skills
    }

    public int adjustAnxiety(int amount)
    {
        if (amount + anxiety > maxAnxiety)
        {
            panic = true;
            PanicAttack();
        }
        else if (amount + anxiety <= 0)
        {
            anxiety = 0;
        }
        else
        {
            anxiety += amount;
        }
        return anxiety;
    }

    public int adjustWill(int amount)
    {
        if (amount < 0)
        {
            if (intrinsicW == 0)
            {
                return -1;
            }
            else if (ambientW < amount * -1)
            {
                amount += ambientW;
                intrinsicW += amount;
            }
        }
        else
        {
            if (intrinsicW < maxIntrinsic)
            {
                if (amount > maxIntrinsic - intrinsicW)
                {
                    amount -= maxIntrinsic - intrinsicW;
                    ambientW = amount;
                }
                else
                {
                    intrinsicW += amount;
                }
            }
            else
            {
                if (ambientW + amount > maxAmbient)
                {
                    ambientW = maxAmbient;
                    overloaded = true;
                    Overload();
                }
                else
                {
                    ambientW += amount;
                }
            }
        }

        return intrinsicW + ambientW;

    }

    public void switchState(Transitions.Command command)
    {
        state.MoveNext(command);
    }

    public Transitions.ProcessState getState()
    {
        return state.CurrentState;
    }

    void Blackout()
    {
        //this is supposed to have the whole screen fade to black and display
        //a text box that says "I-I couldn't breathe.
        //My vision went dark. The rest of the day went by in a blur."
        //and move straight to the night cycle
    }

    void Overload()
    {
        //this is simply for the overloaded UI update
    }
}
