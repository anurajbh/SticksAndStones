using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    public static PlayerEffects Instance { get; private set; }//this class is singleton because it is only relevant to the player
    public float fadeFactor = 0.02f;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    public IEnumerator Blackout()
    {
        //this is supposed to have the whole screen fade to black and display
        //a text box that says "I-I couldn't breathe.
        //My vision went dark. The rest of the day went by in a blur."
        //and move straight to the night cycle
        //StartCoroutine(DoFade());
        yield return StartCoroutine("DoFade");
        Reload();
    }

    void Reload()
    {
        
        TimeProgression.Instance.myCycle = TimeProgression.cycle.night;
        StartCoroutine(EndFade());
    }

    IEnumerator DoFade()
    {
        CanvasGroup canvasGroup = GetComponentInChildren<CanvasGroup>();
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime;
            yield return new WaitForSeconds(fadeFactor);//pauses to run coroutine again next Frame
        }
    }

    IEnumerator EndFade()
    {
        CanvasGroup canvasGroup = GetComponentInChildren<CanvasGroup>();
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime;
            yield return new WaitForSeconds(fadeFactor);//pauses to run coroutine again next Frame
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(Blackout());
        }
    }
}
