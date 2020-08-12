using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();
    public int maxInventorySize = 20;

    public bool AddItem(ItemObject item, GameObject itemObject)
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
                Container.Add(new InventorySlot(item, itemObject));
                return true;
            } else {
                InventorySlot inventorySlot = Container[slotIndex];
                if (inventorySlot.quantity < 99) {
                    inventorySlot.increaseQuantity(1, itemObject);
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
            Container[index].itemObjects.RemoveAt(Container[index].itemObjects.Count);
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
            stats.adjustWill((int)willChange);
        } else if (item.type == ItemType.Anxiety) {
            float anxietyChange = ((AnxietyItem)item).anxietyChange;
            stats.adjustAnxiety((int)anxietyChange);
        }
    }

    public InventorySlot findItemWithName(string name) {
        foreach(InventorySlot invSlot in Container) {
            if (invSlot.item.itemName == name) {
                return invSlot;
            }
        }
        return null;
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int quantity;
    public List<GameObject> itemObjects;
    public int instanceID;

    public InventorySlot(ItemObject item, GameObject itemObject) 
    {
        this.item = item;
        this.quantity = 1;
        itemObjects.Add(itemObject);
    }

    public void increaseQuantity(int n, GameObject itemObject) {
        quantity += n;
        itemObjects.Add(itemObject);
    }
}