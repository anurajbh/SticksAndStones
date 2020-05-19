using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    public Light myDirLight;//directional light- TODO- add other conditions associated with DayNight
    public Color dayColor, dawnColor, duskColor, nightColor;
    public Vector3 dayRotation, dawnRotation, duskRotation, nightRotation;
    public List<GameObject> lamps = new List<GameObject>();//List of lamps
    public float dayLamp, dawnLamp, duskLamp, nightLamp;//Point light intensities
    private void Awake()
    {
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Lamp")) { //Takes in all the lamps at start of scene
            lamps.Add(obj);
        }
    }
    
    public void DayTime()
    {
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
       // myDirLight.transform.Rotate(duskRotation);
        foreach(GameObject obj in lamps) {
            obj.GetComponent<Light>().intensity = duskLamp;
        }
    }
    public void NightTime()
    {
        myDirLight.color = nightColor;
        //myDirLight.transform.Rotate(nightRotation);
       // TimeProgression.Instance.TransitionToNight();
        foreach(GameObject obj in lamps) {
            obj.GetComponent<Light>().intensity = nightLamp;
        }
    }
}
