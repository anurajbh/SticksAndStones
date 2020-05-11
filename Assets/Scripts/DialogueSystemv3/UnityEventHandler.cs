using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UnityEventHandler : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent eventHandler;
    public DialogueBase dialogue;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        eventHandler.Invoke();
        Manager.instance.CloseOptions();
        Manager.triggered = false;

        if (dialogue != null)
        {
            Manager.instance.AddDialogue(dialogue);
        }
    }
}
