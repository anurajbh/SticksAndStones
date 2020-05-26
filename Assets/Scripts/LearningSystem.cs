using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningSystem : MonoBehaviour
{
    SMPlayerStats player;
    public Skills skills;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<SMPlayerStats>();
        skills = GetComponent<Skills>();
    }

    //Adds a skill onto skills
    private void learnSkill(Skills skill) {
        //Todo: get skills from the entity
        player = GameObject.FindWithTag("Player").GetComponent<SMPlayerStats>();
    } 
}
