using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Anxiety Item", menuName = "Inventory System/Items/Anxiety")]

public class AnxietyItem : ItemObject
{
    public float anxietyChange = 10f;
    public void Awake() {
        type = ItemType.Will;
    }
}
