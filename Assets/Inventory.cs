using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxInventorySize = 20;
    public GameObject itemEntry;

    void Start() {
        GameObject itemEntries = GameObject.FindGameObjectWithTag("ItemEntries");
        for (int i = 0; i < maxInventorySize; i++) {
            if (itemEntry != null) {
                GameObject entry = Instantiate(itemEntry);
                entry.transform.SetParent(itemEntries.transform);
            }
        }
    }
}
