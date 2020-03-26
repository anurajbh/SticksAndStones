using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEntity : MonoBehaviour
{
    public float entityStat1 = 50;//placeholder stat
    public float entityStat2 = 50;//placeholder stat
    public float npcAnxietyAffect = 10;//damage value 1
    public float npcWillAffect = 10;//damage value 2
    public float npcHeal = 10;
    public bool NPCTurn = false;

    public void healEntityStat1(float amount)
    {
        entityStat1 += amount;
    }

    public void healEntiyStat2(float amount)
    {
        entityStat2 += amount;
    }

    public void damageEntityStat1(float amount)
    {
        entityStat1 -= amount;
    }

    public void damageEntityStat2(float amount)
    {
        entityStat2 -= amount;
    }
}
