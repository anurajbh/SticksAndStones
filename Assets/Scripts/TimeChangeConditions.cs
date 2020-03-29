using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChangeConditions : MonoBehaviour
{
    TimeProgression timeProgression;
    public List<bool> spokenTo;
    public int ctr = 0;
    public int max = 0;
    private void Update()
    {
        CheckForConditions();
    }

    private void CheckForConditions()
    {
        for(int i =0; i <= spokenTo.Count; i++)
        {
            if (spokenTo[i])
            {
                max++;
            }

        }
        for(int i = 0; i <= max; i++)
        {
            if (ctr == 0)
            {
                timeProgression.myCycle = TimeProgression.cycle.dawn;
            }
            else if (ctr == 1)
            {
                timeProgression.myCycle = TimeProgression.cycle.noon;
            }
            else if (ctr == 2)
            {
                timeProgression.myCycle = TimeProgression.cycle.dusk;
            }
            else if (ctr == 3)
            {
                timeProgression.myCycle = TimeProgression.cycle.night;
            }
        }
       
    }
}
