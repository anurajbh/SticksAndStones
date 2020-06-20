using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger : Interactable
{
    public DialogueBase dialogue;

    public GameObject npcObject;

    public int combatIndex =7;

    int prevScene;

    Scene combat;

    public void TriggerDialogue()
    {
        DialogueManager.instance.AddDialogue(dialogue);
    }
    public override void Interact()
    {
        if(TimeProgression.Instance.myCycle!=TimeProgression.Cycle.night)
        {
            TriggerDialogue();
        }
        else
        {
            TriggerCombat();
        }
        
    }

    private void Awake()
    {
        //currentScene = SceneManager.GetActiveScene();
    }
    private void TriggerCombat()
    {
        StartCoroutine("LoadCombat");
        //StartCoroutine("WaitForCombat");
    }

    /*private IEnumerator WaitForCombat()
    {
        if(BattleSystem.whatEverGameObject.state == BattleState.WON || BattleSystem.gameObject.state == BattleState.LOST)
        {
            //combat = SceneManager.GetSceneByBuildIndex(combatIndex);
            Scene prevScene = SceneManager.GetSceneByBuildIndex(prevScene);
            AsyncOperation async = SceneManager.LoadSceneAsync(prevScene.buildIndex, LoadSceneMode.Additive);
            while (!async.isDone)//wait for combat to load
            {
                yield return null;
            }
            SceneManager.MoveGameObjectToScene(npcObject, prevScene);
            SceneManager.SetActiveScene(prevScene);
            SceneManager.UnloadSceneAsync(combat);
        }
    }*/

    private IEnumerator LoadCombat()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation async= SceneManager.LoadSceneAsync(combatIndex, LoadSceneMode.Additive);
        combat = SceneManager.GetSceneByBuildIndex(combatIndex);
        //async.allowSceneActivation = false;
        while (!async.isDone)//wait for combat to load
        {
            yield return null;
        }
        //async.allowSceneActivation = true;
        
       // DontDestroyOnLoad(npcObject);
        SceneManager.MoveGameObjectToScene(npcObject, combat);
        SceneManager.SetActiveScene(combat);
        SceneManager.UnloadSceneAsync(currentScene);
        int prevScene = currentScene.buildIndex;
    }

    /*private void Update()
    {
        //start interaction collisions should be done through here
        if (!Manager.triggered && Input.GetKeyDown(KeyCode.Z))
        {
            TriggerDialogue();
        }
    }*/
}
