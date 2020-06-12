using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTransition : MonoBehaviour
{
	public Vector2 teleportPoint; 
	public GameObject playerController;
    public float fadeFactor = 0.2f;
    public CanvasGroup canvasGroup;
	private bool inBounds = false;
	public AudioSource openDoor;
    
	public virtual void Awake()
    {
		playerController = GameObject.Find("MovePoint");
		canvasGroup = GameObject.Find("BlackoutImage").GetComponent<CanvasGroup>();
		openDoor = GetComponent<AudioSource>();
	}
		
    public virtual IEnumerator Blackout()
    {
        yield return StartCoroutine("DoFade");
        yield return StartCoroutine("MovePlayer");
        yield return new WaitForSeconds(0.5f);
        Reload();
    }

    protected void Reload()
    {
        StartCoroutine(EndFade());
    }

    IEnumerator DoFade()
    {
		openDoor.Play();
		while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime;
            yield return new WaitForSeconds(fadeFactor);//pauses to run coroutine again next Frame
        }
    }
	IEnumerator MovePlayer()
    {
		playerController.transform.position = new Vector2(teleportPoint.x, teleportPoint.y);
		PlayerMovement.teleporting = true;
        yield return null;
    }
    IEnumerator EndFade()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime;
            yield return new WaitForSeconds(fadeFactor);//pauses to run coroutine again next Frame
        }
		PlayerMovement.teleporting = false;
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		inBounds = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		inBounds = false;
	}
   
	private void Update()
    {
		if (inBounds && Input.GetKeyDown(KeyCode.Z)) {
			Debug.Log("switching locations");
			StartCoroutine(Blackout());
		}

		/*if (Input.GetKeyDown(KeyCode.T))//testing for blackout
        {
            StartCoroutine(Blackout());
        }
        else if (Input.GetKeyDown(KeyCode.Y))//testing for night effect on stats
        {
            TimeProgression.Instance.TransitionToNight();
        }*/
    }
}
