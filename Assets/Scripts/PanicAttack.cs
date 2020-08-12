using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicAttack : MonoBehaviour
{

    public static PanicAttack Instance { get; private set;}
    WaitForSeconds waitForSeconds = new WaitForSeconds(2.0f);
    void Awake()
    {
    	PlayerStats.Instance.panicAttack = this;
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
    //enable panic attack
    public IEnumerator EnablePanicAttack()
    {
    	yield return StartCoroutine("checkTime");
        yield return StartCoroutine("RestrictWill");
    }
    //ambient will begins to drop to 0, decrease by 1 every 2 seconds
    IEnumerator RestrictWill()
    {
    	WaitForSeconds waitForSeconds = new WaitForSeconds(2.0f);
    	while (PlayerStats.Instance.ambWill > 0)
    	{
    		PlayerStats.Instance.ambWill--;
    		yield return waitForSeconds;
    	}
    }

    //coroutine to check for the time when the panic attack happened
    //blackout if night time
    //daytime? is not completed
    /*IEnumerator checkTime()
    {
    	if (TimeProgression.Instance.myCycle != TimeProgression.Cycle.night)
    	{

    	}
        //actions for day time
    	// else
    	// {
    	// 	//seal all the skills
    	// 	//attack will be lowered
    	// }
    }*/

    private void Update()
    {
        //for testing purposes
    	if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(EnablePanicAttack());
        }
    }
}
