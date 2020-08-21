using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeirdlySpecificScript : MonoBehaviour
{
    public int buildIndex;
    float timeElapsed;
    public int timer;
    [SerializeField] int seconds;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Cancel")!=0)
        {
            //AudioManager.instance.Play(0);
            SceneManager.LoadScene(buildIndex);
        }
        timeElapsed += Time.deltaTime;
        seconds = (int)timeElapsed % 60;
        //force a scene load of main menu on end credits
        if(seconds >= timer)
        {
            //AudioManager.instance.Play(0);
            SceneManager.LoadScene(buildIndex);
        }
    }
    private void Awake()
    {
        timeElapsed = 0;
        seconds = 0;
    }
}
