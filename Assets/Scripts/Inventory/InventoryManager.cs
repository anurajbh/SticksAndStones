using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryObject inventory;

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Item")) {
            var item = other.GetComponent<Item>();
            if (item) {
                if (inventory.AddItem(item.item)) {
                    Destroy(other.gameObject);
                }
            }
        }
    }

    public void OnApplicationQuit() {
        inventory.Container.Clear();
    }
}
