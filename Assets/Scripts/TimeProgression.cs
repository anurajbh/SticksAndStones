using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeProgression : MonoBehaviour
{
    public DayNight dayNight;
    public enum cycle
    {
        dawn, noon, dusk, night
    };
    public cycle myCycle;
    private void Awake()
    {
        dayNight = GameObject.FindWithTag("Time").GetComponent<DayNight>();
    }
    private void Update()
    {
        CheckForTimeChange();
    }

    private void CheckForTimeChange()
    {
        if (myCycle == cycle.dawn)
        {
            dayNight.DawnTime();
        }
        else if(myCycle == cycle.noon)
        {
            dayNight.DayTime();
        }
        else if (myCycle == cycle.dusk)
        {
            dayNight.DuskTime();
        }
        else if(myCycle == cycle.night)
        {
            dayNight.NightTime();
        }
    }

    public cycle GetTime()
    {
        return myCycle;
    }
}
