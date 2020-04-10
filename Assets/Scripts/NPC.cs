using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour
{
    protected SMPlayerStats player;
    protected SMNPCEntity enemy;
    public string[][] interaction;

    public abstract void Use(string moveName);

}
