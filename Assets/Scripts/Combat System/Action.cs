using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    public static int whosAttacking = 0; //0 = player, 1 = enemy

    public abstract (int, int, int) Use(string moveName);

    public abstract void Learn(string moveName, int anxiety, int will, int enemyDamage);

    public abstract bool Check(string moveName);
}
