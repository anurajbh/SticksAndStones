using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnxietySprites : MonoBehaviour
{
    public Sprite Anxiety1;
    public Sprite Anxiety2;
    public Sprite Anxiety3;
    public Sprite Anxiety4;
    public Sprite Anxiety5;
    public Sprite Anxiety6;
    public Sprite Anxiety7;
    public Sprite Anxiety8;
    public Sprite Anxiety9;
    public Sprite Anxiety10;


    public float anxietyLevel;
    // Update is called once per frame

    public float getAnxietyLevel() {
        return anxietyLevel;
    }
    private void Awake()
    {
    }
    private void Update()
    {
    	anxietyLevel = PlayerStats.Instance.anxiety;
        if (anxietyLevel == 1) {
        	this.gameObject.GetComponent<Image>().sprite = Anxiety1;
        }   
        else if (anxietyLevel == 2) {
        	this.gameObject.GetComponent<Image>().sprite = Anxiety2;
        }
        else if (anxietyLevel == 3) {
        	this.gameObject.GetComponent<Image>().sprite = Anxiety3;
        }    
        else if (anxietyLevel == 4) {
        	this.gameObject.GetComponent<Image>().sprite = Anxiety4;
        } 
        else if (anxietyLevel == 5) {
        	this.gameObject.GetComponent<Image>().sprite = Anxiety5;
        } 
        else if (anxietyLevel == 6) {
        	this.gameObject.GetComponent<Image>().sprite = Anxiety6;
        } 
        else if (anxietyLevel == 7) {
        	this.gameObject.GetComponent<Image>().sprite = Anxiety7;
        } 
        else if (anxietyLevel == 8) {
        	this.gameObject.GetComponent<Image>().sprite = Anxiety8;
        } 
        else if (anxietyLevel == 9) {
        	this.gameObject.GetComponent<Image>().sprite = Anxiety9;
        } 
        else if (anxietyLevel == 10) {
        	this.gameObject.GetComponent<Image>().sprite = Anxiety10;
        }
    }
    
    
}
