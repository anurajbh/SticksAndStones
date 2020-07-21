using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //TO-DO: enemy damage should affect anxiety
    //basically figure out how to die

    public int anxiety;
    public readonly int maxAnx = 10;

    //public int anxDamage;
    //public int willCost;

    public int totalWill;
    public int ambWill;
    public readonly int maxAmb = 5;
    public int intWill;
    public readonly int maxInt = 15;
    bool overloaded;
    bool panic;

    public static PlayerStats Instance;

    public static Attacks attacks;//object that contains a dictionary of attacks
    public static Skills skills;//object that contains a dictionary of skills

    public AnxietyMeter anxietyMeter;

    public PanicAttack panicAttack;

    private void Awake()
    {
        totalWill = ambWill + intWill;
        attacks = GetComponent<Attacks>();
        skills = GetComponent<Skills>();
        //inventory2D = GetComponent<Inventory2D>();
        if (Instance != null && Instance != this)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
            anxiety = maxAnx;
            panic = true;
            PanicAttack.Instance.EnablePanicAttack();
        }
        else if (amount + anxiety <= 0)
        {
            anxiety = 0;
        }
        else
        {
            anxiety += amount;
        }
        if (anxietyMeter != null) {
            anxietyMeter.updateSprite();
        }
        return anxiety;
    }

    public bool adjustWill(int amount)
    {
        if (amount < 0)
        {
            if (ambWill < (amount * -1))
            {
                amount += ambWill;
                intWill += amount;
            }
            else if (ambWill > (amount * -1))
            {
                ambWill += amount;
            }
            else
            {
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
        if (totalWill <= 0)
        {
            return true;    //sets BattleSystem's isDead var to true, ends battle
        }
        return false;   //sets BattleSystem's isDead var to false, battle continues
    }
    //WaitForSeconds waitForSeconds = new WaitForSeconds(2.0f);

    // void PanicAttack()
    // {
    //     // if (overloaded)
    //     // {
    //     //     overloaded = false;
    //     //     return but for IEnumerator;
    //     // }
    //     // while (ambWill > 0 ) 
    //     // {
    //     //     if (overloaded)
    //     //     {
    //     //         overloaded = false;
    //     //     }
    //     //     ambWill--;
    //     //     yield return waitForSeconds;
    //     // }

    //     //decrease ambient will to 0, should do this every 2 secs or so
    //     //trigger blackout if ambWill <= 0 during the day
    //     //seal skills
    
    // }

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
