using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Default,
    Will,
    Anxiety
}
public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public string itemName;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;
    [TextArea(15, 20)]
    public string useDescription;
}
