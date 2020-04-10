using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCInContact : MonoBehaviour
{
    SMPlayerStats player;
    public bool CanBeSpokenTo = false;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<SMPlayerStats>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            CanBeSpokenTo = true;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            CanBeSpokenTo = false;
        }
    }
    void Update()
    {
        if(CanBeSpokenTo && Input.GetKeyDown(KeyCode.Z))
        {
            SpeakToNPC();
        }
    }

    private void SpeakToNPC()
    {
        print("Start battle");
        player.switchState(Transitions.Command.startBattle);
        SceneManager.LoadScene("Combat");
    }
}
