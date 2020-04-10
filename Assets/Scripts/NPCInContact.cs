using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCInContact : MonoBehaviour
{
    SMPlayerStats player;
    public bool CanBeSpokenTo = false;
    TimeProgression.cycle time;
    TimeProgression Time;
    static int eventCounter = 1;
    string interaction = "event" + eventCounter;
    Charlotte npc;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<SMPlayerStats>();
        Time = GameObject.FindWithTag("Time").GetComponent<TimeProgression>();
        time = Time.GetTime();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("colliding");
        if (other.CompareTag("NPC"))
        {
            CanBeSpokenTo = true;
            npc = other.GetComponent<Charlotte>();
            Debug.Log("can be spoken to");
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            CanBeSpokenTo = false;
            npc = other.GetComponent<Charlotte>();
            Debug.Log("can no longer be spoken to");
        }
    }
    void Update()
    {
        if(CanBeSpokenTo && Input.GetKeyDown(KeyCode.Z))
        {
            SpeakToNPC();
            print("speaking to npc");
        }
    }

    private void SpeakToNPC()
    {
        if (time == TimeProgression.cycle.dawn || time == TimeProgression.cycle.noon)
        {
            npc.Converse();
        }
        else
        {
            print("Start battle");
            player.switchState(Transitions.Command.startBattle);
            //SceneManager.LoadScene("Combat");
        }
    }
}
