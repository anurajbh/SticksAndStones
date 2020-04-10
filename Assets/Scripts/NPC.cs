using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour
{
    protected PlayerStats player;
    protected NPCEntity enemy;
    public string[][] interaction;

    public abstract (int, int) Use(string moveName);

    public abstract void Converse();

}
