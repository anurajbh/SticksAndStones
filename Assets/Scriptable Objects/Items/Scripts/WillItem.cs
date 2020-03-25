using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Will Item", menuName = "Inventory System/Items/Will")]

public class WillItem : ItemObject
{
    public float willChange = 10f;
    public void Awake() {
        type = ItemType.Will;
        if (willChange < 0) {
            useDescription = "Will decreased by " + (willChange * -1.0f).ToString() + ".";
        } else {
            useDescription = "Will increased by " + willChange.ToString() + ".";
        }
    }
}