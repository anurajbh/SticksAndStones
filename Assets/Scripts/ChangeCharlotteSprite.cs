﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCharlotteSprite : MonoBehaviour
{
    public Sprite daytimeSprite;
    public Sprite nighttimeSprite;
    public Material nightLighting;

    private bool changedToNight = false;
    private bool changedToDay = false;

    void LateUpdate() 
    {
        if (TimeProgression.Instance.myCycle == TimeProgression.Cycle.night && !changedToNight) {
            changedToDay = false;
            GetComponent<Renderer>().material = nightLighting; // change the material on the sprite
            GetComponent<SpriteRenderer>().sprite = nighttimeSprite;
            changedToNight = true;
        } else if (TimeProgression.Instance.myCycle == TimeProgression.Cycle.dawn && !changedToDay) {
            changedToNight = false;
            GetComponent<SpriteRenderer>().sprite = daytimeSprite;
            changedToDay = true;
        }
    }

}