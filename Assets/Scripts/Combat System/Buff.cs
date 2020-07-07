using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Buff", menuName = "Buff")]
public class Buff : ScriptableObject
{
    public int activeDay;

    public int defenseBoost;

    public int attackBoost;
    
}
