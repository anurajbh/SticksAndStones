using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEllySprite : MonoBehaviour
{
    public Sprite daytimeSprite;
    public Sprite nighttimeSprite;
    public RuntimeAnimatorController dayAnimator;
    public RuntimeAnimatorController nightAnimator;
    public GameObject playerCharacter;
    public Material nightLighting;

    private bool changedToNight = false;
    private bool changedToDay = false;

    void Awake() 
    {
        playerCharacter = GameObject.FindWithTag("Player");
    }

    void LateUpdate() 
    {
        if (TimeProgression.Instance.myCycle == TimeProgression.Cycle.night && !changedToNight) {
            changedToDay = false;
            playerCharacter.GetComponent<Renderer>().material = nightLighting; // change the material on the sprite
            playerCharacter.GetComponent<Animator>().runtimeAnimatorController = nightAnimator;
            playerCharacter.GetComponent<SpriteRenderer>().sprite = nighttimeSprite;
            changedToNight = true;
        } else if (TimeProgression.Instance.myCycle == TimeProgression.Cycle.dawn && !changedToDay) {
            changedToNight = false;
            playerCharacter.GetComponent<SpriteRenderer>().sprite = daytimeSprite;
            playerCharacter.GetComponent<Animator>().runtimeAnimatorController = dayAnimator;
            changedToDay = true;
        }
    }

}
