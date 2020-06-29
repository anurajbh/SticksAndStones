using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : PlayerTransition
{
    public string sceneName;

    public bool incrementTime;

    public void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(transform.parent.gameObject);
        DontDestroyOnLoad(canvasGroup.transform.parent.gameObject);
    }

    public override IEnumerator Blackout()
    {
        yield return StartCoroutine("DoFade");
        SceneManager.LoadScene(sceneName);
        yield return StartCoroutine("MovePlayer");
        yield return new WaitForSeconds(0.5f);
		yield return StartCoroutine("EndFade");
        //yield return new WaitForSeconds(1f);
        Destroy(transform.parent.gameObject);
		TimeProgression.Instance.dayNight.lamps = new List<GameObject>(); //List of lamps
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Lamp")) { //Takes in all the lamps at start of scene
			TimeProgression.Instance.dayNight.lamps.Add(obj);
		}
    }

	public void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("switching locations");
		StartCoroutine(Blackout());
	}

	public void Update() {
		
	}
}
