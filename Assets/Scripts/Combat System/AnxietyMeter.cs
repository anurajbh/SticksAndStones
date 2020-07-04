using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnxietyMeter : MonoBehaviour
{
    public Sprite[] sprites;
    public Image image;

    void Start()
    {
        PlayerStats.Instance.anxietyMeter = this;
        image = GetComponent<Image>();
        updateSprite();
    }

    // Update is called once per frame
    public void updateSprite()
    {
        image.sprite = sprites[PlayerStats.Instance.anxiety-1];
    }
}
