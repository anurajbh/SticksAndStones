using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCChangeSprite : MonoBehaviour
{
    public Sprite daytimeSprite;
    public Sprite nighttimeSprite;
    public GameObject npc;
    public Material nightLighting;

    private bool changedToNight = false;
    private bool changedToDay = false;

    void Awake()
    {
        npc = GameObject.FindWithTag("NPC");
    }

    void LateUpdate()
    {
        if (TimeProgression.Instance.myCycle == TimeProgression.Cycle.night && !changedToNight)
        {
            changedToDay = false;
            npc.GetComponent<Renderer>().material = nightLighting; // change the material on the sprite
            npc.GetComponent<SpriteRenderer>().sprite = nighttimeSprite;
            changedToNight = true;
        }
        else if (TimeProgression.Instance.myCycle == TimeProgression.Cycle.dawn && !changedToDay)
        {
            changedToNight = false;
            npc.GetComponent<SpriteRenderer>().sprite = daytimeSprite;
            changedToDay = true;
        }
    }
}
