using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    protected SMPlayerStats player;
    protected SMNPCEntity enemy;


    public abstract (int, int, int) Use(string moveName);

    public abstract void Learn(string moveName, int anxiety, int wilo, int enemyDamage);


}
