using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEffects : MonoBehaviour
{
    public static PlayerEffects Instance { get; private set; }//this class is singleton because it is only relevant to the player
    public Text blackoutText;
    public float fadeFactor = 0.2f;
    public CanvasGroup canvasGroup;
    void Awake()
    {
        canvasGroup = GameObject.Find("BlackoutImage").GetComponent<CanvasGroup>();
        blackoutText = GameObject.Find("BlackoutText").GetComponent<Text>();
        //textObject = GameObject.Find("BlackoutText");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            Destroy(GetComponent<PlayerMovement>().movePoint.gameObject);
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
        yield return StartCoroutine("DisplayBlackoutText");
        yield return new WaitForSeconds(3f);
        Reload();
    }

    void Reload()
    {

        TimeProgression.Instance.TransitionToNight();
        StartCoroutine(EndFade());
    }

    IEnumerator DoFade()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime;
            yield return new WaitForSeconds(fadeFactor);//pauses to run coroutine again next Frame
        }
    }
    IEnumerator DisplayBlackoutText()
    {
        blackoutText.enabled = true;
        yield return null;
    }
    IEnumerator EndFade()
    {
        blackoutText.enabled = false;
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime;
            yield return new WaitForSeconds(fadeFactor);//pauses to run coroutine again next Frame
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))//testing for blackout
        {
            StartCoroutine(Blackout());
        }
        else if (Input.GetKeyDown(KeyCode.U))//testing for night effect on stats
        {
            TimeProgression.Instance.TransitionToNight();
        }
    }
}
