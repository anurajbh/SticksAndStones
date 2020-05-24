using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningSystem : MonoBehaviour
{
    SMPlayerStats player;
    public Skills ;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<SMPlayerStats>();
        skills = GetComponent<Skills>();
    }

    //Adds a skill onto skills
    private void learnSkill(Skill skill) {
        //Todo: get skills from the entity
        player = GameObject.FindWithTag("Player").GetComponent<SMPlayerStats>();
    } 
}
