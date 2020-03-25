using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deafult Item", menuName = "Inventory System/Items/Default")]

public class DefaultItem : ItemObject
{
    public void Awake() {
        type = ItemType.Default;
    }
}
