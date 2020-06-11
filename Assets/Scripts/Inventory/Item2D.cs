using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Item2D
{
    public enum Item2DType
    {
        Anxiety,
        Will
    }
    public Item2DType item2DType;
    public int amount;
}