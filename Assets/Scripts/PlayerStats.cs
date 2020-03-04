using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    float anxiety;
    float will;

    void increaseAnxiety(float amount)
    {
        anxiety += amount;
    }

    void increaseWill(float amount)
    {
        will += amount;
    }

    void decreaseAnxiety(float amount)
    {
        anxiety -= amount;
    }

    void decreaseWill(float amount)
    {
        will -= amount;
    }
}

