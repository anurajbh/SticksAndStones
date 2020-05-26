using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMPlayerStats : MonoBehaviour
{
    //TO DO: imlpement Blackout() and Overload(), figure out locking skills 
    //and reducing attack while (panic)
    public static SMPlayerStats Instance;//only one instance of PlayerStats should be present in the scene, we wont be having two players at once
    public int anxiety = 6;
    readonly int maxAnxiety = 10;
    int ambientW = 15;
    int intrinsicW = 0;
    readonly int maxAmbient = 10;
    readonly int maxIntrinsic = 15;
    bool overloaded = false;
    bool panic = false;
    public int will = 15;
    //Transitions.Process state = new Transitions.Process();
    public Attacks attacks;
    public Skills skills;
    // Start is called before the first frame update
    void Awake()
    {
        will = ambientW + intrinsicW;
        attacks = GetComponent<Attacks>();
        skills = GetComponent<Skills>();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
       /* while (panic)
        {
            while (ambientW > 0)
            {
                if (ambientW - 2 < 0)
                {
                    ambientW = 0;
                    if (TimeProgression.Instance.myCycle == TimeProgression.cycle.dawn || TimeProgression.Instance.myCycle == TimeProgression.cycle.noon)
                    {
                        //PlayerEffects.Instance.StartCoroutine(PlayerEffects.Instance.Blackout());
                    }
                }
                else
                {
                    ambientW = ambientW - 2;
                }
            }
        }*/
    }

    /*void PanicAttack()
    {
        if (overloaded)
        {
            overloaded = false;
        }
        //seal skills
    }*/
    

    public int adjustAnxiety(int amount)
    {
        if (amount + anxiety > maxAnxiety)
        {
            panic = true;
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
            else
            {
                intrinsicW += amount;
            }
        }
        else { 
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

    /*public void switchState(Transitions.Command command)
    {
        state.MoveNext(command);
    }

    public Transitions.ProcessState getState()
    {
        return state.CurrentState;
    }*/

    void Overload()
    {
        //this is simply for the overloaded UI update
    }
}
