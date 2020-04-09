using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsBar : MonoBehaviour
{
    public Slider WillSlider;
    //public Slider AnxietySlider;
    public SMPlayerStats playerStats;
    private void Awake()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<SMPlayerStats>();
        SetWillBar(playerStats.will);
        //SetAnxietyBar(playerStats.anxiety);
    }
    private void Update()
    {
        SetWill(playerStats.will);
        //SetAnxiety(playerStats.anxiety);
    }

    public void SetWillBar(float will)
    {
    	WillSlider.maxValue = will;
    }

    public void SetWill(float will)
    {
    	WillSlider.value = will;
    }

    //public void SetAnxietyBar(float anxiety)
    //{
    //    AnxietySlider.maxValue = anxiety;
   //}

    //public void SetAnxiety(float anxiety)
    //{
    //    AnxietySlider.value = anxiety;
    //}
}
