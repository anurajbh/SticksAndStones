using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Will Item", menuName = "Inventory System/Items/Will")]

public class WillItem : ItemObject
{
    public float willChange = 10f;
    public void Awake() {
        type = ItemType.Will;
    }
}