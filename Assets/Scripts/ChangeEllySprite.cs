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

    void Awake() 
    {
        playerCharacter = GameObject.FindWithTag("Player");
    }

    void Update() 
    {
        if (TimeProgression.Instance.myCycle == TimeProgression.Cycle.night) {
            playerCharacter.GetComponent<SpriteRenderer>().sprite = nighttimeSprite;
            playerCharacter.GetComponent<Animator>().runtimeAnimatorController = nightAnimator;
        } else if (TimeProgression.Instance.myCycle == TimeProgression.Cycle.dawn) {
            playerCharacter.GetComponent<SpriteRenderer>().sprite = daytimeSprite;
            playerCharacter.GetComponent<Animator>().runtimeAnimatorController = dayAnimator;
        }
    }
}
