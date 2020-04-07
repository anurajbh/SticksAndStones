using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampScript : MonoBehaviour
{
    public bool isLamp;
    public void changeLampIntesity(float intensity) {
        if (isLamp) {
            GetComponent<Light>().intensity = intensity;
        }
    }
}
