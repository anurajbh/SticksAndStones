using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSkills : MonoBehaviour
{
    //script that interfaces the Editor with Attacks and Skills to teach owner object those attributes
    //Not to be used by player because player skills and attacks should be related to PlayerStat values
    public Attacks attacks;//class that holds dictionary of attacks
    
    public Skills skills;//class that holds dictionary of skills
    //TO-DO- int or float?
    [System.Serializable]
    public class Ability
    {
        public string name;
        public int anxietyEffect;//effect on player anxiety
        public int willEffect;//effect on player will
        public int npcEffect;//effect on self
    }
    //Unity Editor does not Serialize structs
    public List<Ability> AttackAbilities;//add attacks to this NPC

    public List<Ability> SkillAbilities;//add skills to this NPC

    void Awake()
    {
        attacks = GetComponent<Attacks>();
        skills = GetComponent<Skills>();
        UpdateNPCSkills();
    }

    private void UpdateNPCSkills()//teach NPC each Skill and Ability that the Designer has added via Editor
    {
        int i = 0;
        while(i<AttackAbilities.Count)
        {
            attacks.Learn(AttackAbilities[i].name, AttackAbilities[i].anxietyEffect, AttackAbilities[i].willEffect, AttackAbilities[i].npcEffect);//add attack to dictonary
            i++;
        }
        i = 0;
        while (i < SkillAbilities.Count)
        {
            skills.Learn(SkillAbilities[i].name, SkillAbilities[i].anxietyEffect, SkillAbilities[i].willEffect, SkillAbilities[i].npcEffect);//add skill to dictionary
            i++;
        }
    }
}
