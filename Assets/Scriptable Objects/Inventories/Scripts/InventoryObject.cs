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
            Container.Add(new InventorySlot(item));
            return true;
        }
        return false;
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;

    public InventorySlot(ItemObject item) 
    {
        this.item = item;
    }
}