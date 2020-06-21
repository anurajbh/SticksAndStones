using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChangeConditions : MonoBehaviour
{
    public TimeProgression timeProgression;
    public List<bool> spokenTo;
    public int ctr = 0;
    public int max = 0;
    static int i = 0;
    static int j = 0;
    private void Update()
    {
        //CheckForConditions();
    }

    private void CheckForConditions()
    {
        while(i <= spokenTo.Count)
        {
            if (spokenTo[i])
            {
                max++;//incrementing for each character spoken to
            }
            i++;
        }
        //is there a better way of doing this than two loops- this seems computationally inefficient?
        while (j <= max)//allowing ctr to move between 0, 1, 2 and 3 for each spokenTo
        {
            switch (ctr)
            {
                case 0:
                    timeProgression.myCycle = TimeProgression.Cycle.dawn;
                    break;
                case 1:
                    timeProgression.myCycle = TimeProgression.Cycle.noon;
                    break;
                case 2:
                    timeProgression.myCycle = TimeProgression.Cycle.dusk;
                    break;
                case 3:
                    timeProgression.myCycle = TimeProgression.Cycle.night;
                    break;
                default:
                    ctr = 0;
                    break;
            }
            ctr++;
            j++;
        }
       
    }
}
