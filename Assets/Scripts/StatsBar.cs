using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsBar : MonoBehaviour
{
    //TO DO: replace all commented lines with the correct code for this to function

    public Slider WillSlider;
    public RectTransform mask;
    private float baseWidth;
    //public Slider AnxietySlider;
    //public PlayerStats playerStats;
    private void Awake()
    {
        //playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        //SetWillBar(playerStats.will);
        //SetAnxietyBar(playerStats.anxiety);
        WillSlider = GetComponent<Slider>();
        baseWidth = mask.rect.width;
    }
    private void Update()
    {
        //SetWill(playerStats.will);
        //SetAnxiety(playerStats.anxiety);
        float width = baseWidth * PlayerStats.Instance.totalWill / (PlayerStats.Instance.maxAmb + PlayerStats.Instance.maxInt);
        // mask.rect.Set(mask.rect.x, mask.rect.y, width, mask.rect.height);
        mask.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

    public void SetWillBar(float will)
    {
    	// WillSlider.maxValue = will;
    }

    public void SetWill(float will)
    {
    	// WillSlider.value = will;

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
