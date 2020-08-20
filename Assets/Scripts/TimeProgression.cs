using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeProgression : MonoBehaviour
{
    public DayNight dayNight;
    public static TimeProgression Instance;
    //public float currentTime = 0f;
    public int daysElapsed = 0;
    public enum Cycle
    {
        dawn, noon, dusk, night
    };
    public Cycle myCycle;
    public Cycle nextTime;//to keep track of next time of day

    //public GameObject playerController;

    public CanvasGroup canvasGroup;

    public float fadeFactor = 0.2f;

    public Text duskText;

    public Text sleepText;

    public bool triggerDuskTransition = false;//for testing

    public bool triggerNightTransition = false;//for testing, potentially use TimeProgression.Instance.triggerNightTransition in the bed to trigger night time?

    public GameObject charlotte; // charlotte npc prefab to be instantiated in appropriate scenes
    private static bool charlotteInitialized = false;

    private static bool charlotteActivated = false;

    public IEnumerator coroutine;
    private void Awake()
    {
        dayNight = GameObject.FindWithTag("Time").GetComponent<DayNight>();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        //playerController = GameObject.Find("MovePoint");
        canvasGroup = GameObject.Find("BlackoutImage").GetComponent<CanvasGroup>();
        duskText = GameObject.Find("DuskText").GetComponent<Text>();
        sleepText = GameObject.Find("NightText").GetComponent<Text>();
        //InvokeRepeating("TrackTime", 1f, 1f);
        if (!charlotteInitialized) {
            charlotte = GameObject.Find("Charlotte");
            charlotte.SetActive(false);
            charlotteInitialized = true;
        }
    }

    void Start() {
        CheckForTimeChange();
    }
    /*public void TrackTime()//to be invoked every 1 sec
    {
        currentTime += 5f;
        if (currentTime <= 216f)
        {
            myCycle = Cycle.dawn;
            nextTime = Cycle.noon;
        }
        else if(currentTime > 216f && currentTime <= 432f)
        {
            myCycle = Cycle.noon;
            nextTime = Cycle.dusk;
        }
        else if (currentTime > 432f && currentTime <= 648f)
        {
            myCycle = Cycle.dusk;
            nextTime = Cycle.night;
        }
        else if (currentTime > 648f && currentTime <= 864f)
        {
            myCycle = Cycle.night;
            nextTime = Cycle.dawn;
        }
        else if(currentTime>865f)
        {
            daysElapsed++;
            currentTime = 0f;
        }

        //accomodate for weekends??
    }*/
    private void Update()
    {
        if(triggerDuskTransition)
        {
            StartCoroutine("TransitionToDusk");
            //isDusk = false;
        }
        if (triggerNightTransition)
        {
            triggerNightTransition = false;
            StartCoroutine("TransitionToNight");
            //isDusk = false;
        }
        if(myCycle!=Cycle.night)
        {
            nextTime = myCycle + 1;
        }
        else if(myCycle == Cycle.night)
        {
            DialogueManager.spokenTo = false;
            nextTime = Cycle.dawn;
        }
        if (daysElapsed == 1 && (myCycle == Cycle.noon || myCycle == Cycle.night) && !charlotteActivated) { // logic for charlotte npc visibility
            charlotte.SetActive(true);
            charlotteActivated = true;
        }
        if (daysElapsed == 2 && (myCycle == Cycle.noon || myCycle == Cycle.night) && !charlotteActivated) {
            charlotte.SetActive(true);
            charlotteActivated = true;
        }
        if (daysElapsed == 3 && charlotteActivated) {
            charlotte.SetActive(false);
            charlotteActivated = false;
        }
        if (daysElapsed == 3 && (myCycle == Cycle.noon || myCycle == Cycle.night) && !charlotteActivated) {
            charlotte.SetActive(true);
            charlotteActivated = true;
        }
        if (daysElapsed == 5 && charlotteActivated) {
            charlotte.SetActive(false);
            charlotteActivated = false;
        }
        if (daysElapsed == 4 && (myCycle == Cycle.noon || myCycle == Cycle.night) && !charlotteActivated) {
            charlotte.SetActive(true);
            charlotteActivated = true;
        }
        if (myCycle != Cycle.noon && myCycle != Cycle.night && charlotteActivated) {
            charlotte.SetActive(false);
            charlotteActivated = false;
        } 
    }
    public void ChangeTime()
    {
        myCycle = nextTime;
        if(myCycle == Cycle.dawn)
        {
            daysElapsed++;
        }
        DialogueManager.instance.previousDialogues.Clear();
        CheckForTimeChange();
    }
    public void CheckForTimeChange()
    {
        if (myCycle == Cycle.dawn)
        {
            dayNight.DawnTime();
        }
        else if(myCycle == Cycle.noon)
        {
            //isNight = false;
            dayNight.DayTime();
        }
        else if (myCycle == Cycle.dusk)
        {
            StartCoroutine("TransitionToDusk");
            //nextTime = Cycle.night;
            //dayNight.DuskTime();
        }
        else if(myCycle == Cycle.night)
        {
            StartCoroutine("TransitionToNight");
            dayNight.NightTime();
        }
    }
    public IEnumerator TransitionToDusk()
    {
        triggerDuskTransition = false;
        myCycle = Cycle.dusk;
        dayNight.DuskTime();
        yield return StartCoroutine("DuskTransition");
    }
    public IEnumerator DuskTransition()
    {
        yield return StartCoroutine("DoFade");
        LoadStartingScene();
        coroutine = DisplayBlackoutText(duskText);
        yield return StartCoroutine(coroutine);
        yield return new WaitForSeconds(3f);
        PlayerMovement.teleporting = false;
        StartCoroutine("EndFade");
        //isDusk = false;
    }

    public IEnumerator TransitionToNight()
    {
        
        myCycle = Cycle.night;
        dayNight.NightTime();
        PlayerStats.Instance.adjustWill(-PlayerStats.Instance.anxiety / 2);
        yield return StartCoroutine("NightTransition");
    }
    public IEnumerator NightTransition()
    {
        yield return StartCoroutine("DoFade");
        coroutine = DisplayBlackoutText(sleepText);
        yield return StartCoroutine(coroutine);
        SceneManager.LoadScene("Overworld (Night)");
        yield return new WaitForSeconds(3f);
        StartCoroutine("EndFade");
        //isDusk = false;
    }

    private void LoadStartingScene()
    {
        SceneManager.LoadScene("Overworld");
        PlayerMovement.Instance.movePoint.position = new Vector3(12.2f, 5.2f, -.1f);
        PlayerMovement.teleporting = true;
    }
    IEnumerator DoFade()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime;
            yield return new WaitForSeconds(fadeFactor);//pauses to run coroutine again next Frame
        }
    }
    IEnumerator DisplayBlackoutText(Text text)
    {
        text.enabled = true;
        yield return null;
    }
    IEnumerator EndFade()
    {
        duskText.enabled = false;
        sleepText.enabled = false;
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime;
            yield return new WaitForSeconds(fadeFactor);//pauses to run coroutine again next Frame
        }
    }

    public bool isDusk() {
        return myCycle == Cycle.dusk;
    }

}
