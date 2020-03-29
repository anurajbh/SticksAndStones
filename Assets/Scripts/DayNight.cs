using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    public Light myLight;
    public float dayRed, dayGreen, dayBlue;
    public float nightRed, nightGreen, nightBlue;
    Color color0, color1;
    private void Awake()
    {
        color0 = new Color(dayRed / 255f, dayGreen / 255f, dayBlue / 255f);
        color1 = new Color(nightRed / 255f, nightGreen / 255f, nightBlue / 255f);
    }
    public void DayTime()
    {
        myLight.color = color0;
    }
    public void NightTime()
    {
        myLight.color = color1;
    }
}
