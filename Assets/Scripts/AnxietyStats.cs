using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnxietyStats : MonoBehaviour
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

    public PlayerStats playerStats;

    public float anxietyLevel; 
    // Update is called once per frame
    private void Update()
    {
    	anxietyLevel = playerStats.anxiety;
        if (anxietyLevel == 10) {
        	this.gameObject.GetComponent<Image>().sprite = Anxiety1;
        }   
        else if (anxietyLevel == 20) {
        	this.gameObject.GetComponent<Image>().sprite = Anxiety2;
        }
        else if (anxietyLevel == 30) {
        	this.gameObject.GetComponent<Image>().sprite = Anxiety3;
        }    
        else if (anxietyLevel == 40) {
        	this.gameObject.GetComponent<Image>().sprite = Anxiety4;
        } 
        else if (anxietyLevel == 50) {
        	this.gameObject.GetComponent<Image>().sprite = Anxiety5;
        } 
        else if (anxietyLevel == 60) {
        	this.gameObject.GetComponent<Image>().sprite = Anxiety6;
        } 
        else if (anxietyLevel == 70) {
        	this.gameObject.GetComponent<Image>().sprite = Anxiety7;
        } 
        else if (anxietyLevel == 80) {
        	this.gameObject.GetComponent<Image>().sprite = Anxiety8;
        } 
        else if (anxietyLevel == 90) {
        	this.gameObject.GetComponent<Image>().sprite = Anxiety9;
        } 
        else if (anxietyLevel == 100) {
        	this.gameObject.GetComponent<Image>().sprite = Anxiety10;
        } 
    }
    
    
}
