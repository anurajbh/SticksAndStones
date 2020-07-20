using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharlottePlacement : MonoBehaviour
{
   
    private TimeProgression time;

    private Trigger trigger;

    private Vector2 offScene = new Vector2(100, 100);
    public Vector2 day2;
    public Vector2 day3;
    public Vector2 day5;
    public Vector2 day7;
    public DialogueBase day2Dialogue;
    public DialogueBase day3Dialogue;
    public DialogueBase day5Dialogue;
    public DialogueBase day7Dialogue;

	void Awake() 
	{
        time = GameObject.Find("Time").GetComponent<TimeProgression>();
        trigger = this.GetComponent<Trigger>();
	}

    void OnEnable() 
    {
        Debug.Log("Charlotte enabled");
        if (time.daysElapsed == 1) {
            this.transform.position = day2;
        } else if (time.daysElapsed == 2) {
            this.transform.position = day3;
        } else if (time.daysElapsed == 4) {
            this.transform.position = day5;
        } else if (time.daysElapsed == 6) {
            this.transform.position = day7;
        }
    }

    void Update() 
    {
        if (time.daysElapsed == 0) {
            this.transform.position = offScene;
        } else if (time.daysElapsed == 1) {
            trigger.dialogue = day2Dialogue;
            if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Overworld")) {
                this.transform.position = day2;
            } else {
                this.transform.position = offScene;
            }
        } else if (time.daysElapsed == 2) {
            trigger.dialogue = day3Dialogue;
            if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Main Building - Floor 1")) {
                this.transform.position = day3;
            } else {
                this.transform.position = offScene;
            }
        } else if (time.daysElapsed == 3) {
            this.transform.position = offScene;
        } else if (time.daysElapsed == 4) {
            trigger.dialogue = day5Dialogue;
            if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Overworld")) {
                this.transform.position = day5;
            } else {
                this.transform.position = offScene;
            }
        } else if (time.daysElapsed == 5) {
            this.transform.position = offScene;
        } else if (time.daysElapsed == 6) {
            trigger.dialogue = day7Dialogue;
            if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Main Building - Floor 2")) {
                this.transform.position = day7;
            } else {
                this.transform.position = offScene;
            }
        }
    }

}
