using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //TO-DO: enemy damage should affect anxiety
    //basically figure out how to die

    public int anxiety;
    public int maxAnx;

    //public int anxDamage;
    //public int willCost;

    public int totalWill;
    public int ambWill;
    public int maxAmb;
    public int intWill;
    public int maxInt;
    bool overloaded;

    public static PlayerStats instance;

    public static Dictionary<string, (int, int, int)> attacks = new Dictionary<string, (int, int, int)>
    {
        { "poke", (1, -1, -1) },
        { "cry", (2, -3, 0) },
    };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        totalWill = ambWill + intWill;
    }

    public int adjustAnxiety(int amount)
    {
        if (amount + anxiety > maxAnx)
        {
            //panic = true;
            //PanicAttack();
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

    public bool adjustWill(int amount)
    {
        if (amount < 0)
        {
            if (intWill == 0)
            {
                return true;    //sets BattleSystem's isDead var to true, ends battle
            }
            else if (ambWill < amount * -1)
            {
                //THIS NEEDS DOUBLE CHECKING
                amount += ambWill;
                intWill += amount;
            }
        }
        else
        {
            if (intWill < maxInt)
            {
                if (amount > maxInt - intWill)
                {
                    amount -= maxInt - intWill;
                    ambWill = amount;
                }
                else
                {
                    intWill += amount;
                }
            }
            else
            {
                if (ambWill + amount > maxAmb)
                {
                    ambWill = maxAmb;
                    Overload();
                }
                else
                {
                    ambWill += amount;
                }
            }
        }

        return false;   //sets BattleSystem's isDead var to false, battle continues
    }

    /*IEnumerator PanicAttack()
    {
        if (overloaded)
        {
            overloaded = false;
            return but for IEnumerator;
        }
        //decrease ambient will to 0, should do this every 2 secs or so
        //trigger blackout if ambWill <= 0 during the day
        //seal skills
    }*/

    void Blackout()
    {
        //this is supposed to have the whole screen fade to black and display
        //a text box that says "I-I couldn't breathe.
        //My vision went dark. The rest of the day went by in a blur."
        //and move straight to the night cycle

        //use 2D canvas for making the screen turn black, change alpha of the canvas group
    }

    void Overload()
    {
        overloaded = true;
        //this is simply for the overloaded UI update
    }
}
