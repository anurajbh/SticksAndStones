using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger : Interactable
{
    //literally just triggers dialogue
    //notice it derives from interactable

    public GameObject npcObject;

    public int combatIndex =7;

    int prevScene;

    Scene combat;

    public static DialogueBase dialogue;

    //a separate method in case we need to call it separate to the interactable stuff
    public static void TriggerDialogue()
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
}
