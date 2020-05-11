using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class EventBehavior : ScriptableObject
{
    public void TestEvent()
    {
        Debug.Log("hi");
    }
}
