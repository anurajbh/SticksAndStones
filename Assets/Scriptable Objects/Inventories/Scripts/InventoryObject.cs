using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();
    public int maxInventorySize = 20;

    public bool AddItem(ItemObject item)
    {
        if (Container.Count < maxInventorySize) {
            int slotIndex = -1;
            for (int i = 0; i < Container.Count; i++) {
                if (Container[i].item == item) {
                    slotIndex = i;
                    break;
                }
            }
            if (slotIndex == -1) {
                Container.Add(new InventorySlot(item));
                return true;
            } else {
                InventorySlot inventorySlot = Container[slotIndex];
                if (inventorySlot.quantity < 99) {
                    inventorySlot.increaseQuantity(1);
                    return true;
                }
                return false;
            }
        }
        return false;
    }

    public bool RemoveItem(int index) {
        if (Container.Count > 0) {
            Container[index].quantity -= 1;
            if (Container[index].quantity == 0) {
                Container.RemoveAt(index);
                return true;
            }
        }
        return false;
    }

    public void UseItem(ItemObject item, GameObject playerController) {
        PlayerStats stats = playerController.GetComponent<PlayerStats>();
        if (item.type == ItemType.Will) {
            float willChange = ((WillItem)item).willChange;
            if (willChange < 0) {
                stats.decreaseWill(willChange * -1.0f);
            } else {
                stats.increaseWill(willChange);
            }
        } else if (item.type == ItemType.Anxiety) {
            float anxietyChange = ((AnxietyItem)item).anxietyChange;
            if (anxietyChange < 0) {
                stats.decreaseAnxiety(anxietyChange * -1.0f);
            }
            else {
                stats.increaseAnxiety(anxietyChange);
            }
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int quantity;

    public InventorySlot(ItemObject item) 
    {
        this.item = item;
        this.quantity = 1;
    }

    public void increaseQuantity(int n) {
        quantity += n;
    }
}