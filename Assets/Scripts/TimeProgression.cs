using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        //InvokeRepeating("TrackTime", 1f, 1f);
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
    public void ChangeTime()
    {
        myCycle = nextTime;
        if(myCycle == Cycle.dawn)
        {
            daysElapsed++;
        }
        CheckForTimeChange();

    }
    public void CheckForTimeChange()
    {
        if (myCycle == Cycle.dawn)
        {
            nextTime = Cycle.noon;
            dayNight.DawnTime();
        }
        else if(myCycle == Cycle.noon)
        {
            //isNight = false;
            nextTime = Cycle.dusk;
            dayNight.DayTime();
        }
        else if (myCycle == Cycle.dusk)
        {
            nextTime = Cycle.night;
            dayNight.DuskTime();
        }
        else if(myCycle == Cycle.night)
        {
            TransitionToNight();
            nextTime = Cycle.dawn;
            dayNight.NightTime();
        }
    }
    public void TransitionToNight()
    {
        myCycle = Cycle.night;
        PlayerStats.Instance.adjustWill(-PlayerStats.Instance.anxiety / 2);
    }
}
