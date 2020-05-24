using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningSystem : MonoBehaviour
{
    Skills skills;
    SMPlayerStats player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<SMPlayerStats>();
    }

    //Adds a skill onto skills
    private void learnSkill(Skill skill) {
        //Todo: get skills from the entity
        skills.Learn(skill.skillName, skill.anxiety, skill.will, skill.enemyDamage);
    } 
}
