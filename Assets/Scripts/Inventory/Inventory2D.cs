using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory2D : MonoBehaviour
{
    public List<Item2D> item2DList;
    public Inventory2D()
    {
        item2DList = new List<Item2D>();
        Add2DItem(new Item2D { item2DType = Item2D.Item2DType.Anxiety, amount = 1 });
        Add2DItem(new Item2D { item2DType = Item2D.Item2DType.Will, amount = 1 });
        Debug.Log("New inventory created bruh");
    }
    public void Add2DItem(Item2D item2D)
    {
        item2DList.Add(item2D);
    }
    public List<Item2D> GetItemList()
    {
        return item2DList;
    }
}
