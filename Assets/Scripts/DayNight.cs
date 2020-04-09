using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    public Light myLight;//directional light- TODO- add other conditions associated with DayNight
    public float dayRed, dayGreen, dayBlue;//day rgb
    public float nightRed, nightGreen, nightBlue;//night rgb
    Color color0, color1;
    private void Awake()
    {
<<<<<<< Updated upstream
        color0 = new Color(dayRed / 255f, dayGreen / 255f, dayBlue / 255f);
        color1 = new Color(nightRed / 255f, nightGreen / 255f, nightBlue / 255f);
=======
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Lamp")) { //Takes in all the lamps at start of scene
            lamps.Add(obj);
        }
        //DawnTime();//JUST HERE FOR TESTING PURPOSES. Doesn't need to start on a default light setting
>>>>>>> Stashed changes
    }
    public void DayTime()
    {
<<<<<<< Updated upstream
        myLight.color = color0;
    }
    public void NightTime()
    {
        myLight.color = color1;
=======
        myDirLight.color = dayColor;//Changes sunlight color
        //myDirLight.transform.Rotate(dayRotation);//Changes the intensity of the light by rotating
        foreach(GameObject obj in lamps) {//Changes the intensity of all lamps
            obj.GetComponent<Light>().intensity = dayLamp;
        }
    }
    public void DawnTime() {
        myDirLight.color = dawnColor;
        //myDirLight.transform.Rotate(dawnRotation);
        foreach(GameObject obj in lamps) {
            obj.GetComponent<Light>().intensity = dawnLamp;
        }
    }
    public void DuskTime() {
        myDirLight.color = duskColor;
        //myDirLight.transform.Rotate(duskRotation);
        foreach(GameObject obj in lamps) {
            obj.GetComponent<Light>().intensity = duskLamp;
        }
    }
    public void NightTime()
    {
        myDirLight.color = nightColor;
       // myDirLight.transform.Rotate(nightRotation);
        foreach(GameObject obj in lamps) {
            obj.GetComponent<Light>().intensity = nightLamp;
        }
>>>>>>> Stashed changes
    }
}
