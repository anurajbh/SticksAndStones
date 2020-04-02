using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float anxiety = 50;
    public float will = 50;
    public float playerDPS1 = 10;//placeholder name for attack1
    public float playerDPS2 = 10;//placeholder name for attack2
    public void increaseAnxiety(float amount)
    {
        anxiety += amount;
    }

    public void increaseWill(float amount)
    {
        will += amount;
    }

    public void decreaseAnxiety(float amount)
    {
        anxiety -= amount;
    }

    public void decreaseWill(float amount)
    {
        will -= amount;
    }
}

