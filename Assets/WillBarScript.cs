using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WillBarScript : MonoBehaviour
{

    public Slider slider;
    public void SetMaxWill(int will)
    {
    	slider.maxValue = will;
    	slider.value = will;
    }
    public void setWill(int will)
    {
    	slider.value = will; 
    }
}
