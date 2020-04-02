using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeProgression : MonoBehaviour
{
    DayNight dayNight;
    public enum cycle
    {
        dawn, noon, dusk, night
    };
    public cycle myCycle;
    private void Awake()
    {
        myCycle = cycle.dawn;
    }
    private void Update()
    {
        CheckForTimeChange();
    }

    private void CheckForTimeChange()
    {
        if (myCycle == cycle.dawn || myCycle == cycle.noon)
        {
            dayNight.DayTime();
        }
        else if(myCycle == cycle.dusk || myCycle == cycle.night)
        {
            dayNight.NightTime();
        }
    }

}
