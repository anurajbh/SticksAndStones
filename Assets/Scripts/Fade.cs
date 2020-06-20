using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public static void FadeIn()
    {
        //StartCoroutine(FadeTo(0f, 1f));
    }

    IEnumerator FadeTo(float aValue, float time)
    {
        float alpha = GetComponent<Renderer>().material.color.a;
        for (float t = 0f; t < 1f; t += Time.deltaTime / time)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            GetComponent<Renderer>().material.color = newColor;
            yield return null;
        }
    }
}
