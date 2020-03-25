using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WillBar : MonoBehaviour
{
    public Slider slider;

    public void SetWillBar(int will)
    {
    	slider.maxValue = will;
    	slider.value = will;
    }

    public void SetWill(int will)
    {
    	slider.value = will;
    }
}
