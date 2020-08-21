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
    [SerializeField] int creditsThemeIndex = 3;
    [SerializeField] int menuThemeIndex = 0;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Cancel")!=0)
        {
            SceneManager.LoadScene(buildIndex);
            AudioManager.instance.Play(menuThemeIndex);
        }
        timeElapsed += Time.deltaTime;
        seconds = (int)timeElapsed % 60;
        //force a scene load of main menu on end credits
        if(seconds >= timer)
        {
            SceneManager.LoadScene(buildIndex);
            AudioManager.instance.Play(menuThemeIndex);
        }
    }
    private void Awake()
    {
        timeElapsed = 0;
        seconds = 0;
        AudioManager.instance.Play(creditsThemeIndex);
    }
}
