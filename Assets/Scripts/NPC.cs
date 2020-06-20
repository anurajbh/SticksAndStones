using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    public string enemyName;
    public int maxHealth;
    public int currentHealth;

    public List<NPCAbility> AttackAbilities;//add attack abilities to this NPC in Editor

    public List<NPCAbility> SkillAbilities;//add skill abilities to this NPC in Editor

    public Attacks attacks;//class that holds dictionary of attacks

    public Skills skills;//class that holds dictionary of skills

    //public bool isNight = false;

    //public string[][] interaction;

    //public abstract (int, int) Use(string moveName);

    //public abstract void Converse();

    public void Awake()
    {
        attacks = GetComponent<Attacks>();
        skills = GetComponent<Skills>();
        UpdateNPCSkills();
    }

    public bool adjustHealth(int amount)
    {
        if (currentHealth + amount <= 0)
        {
            currentHealth = 0;
            return true;    //sets BattleSystem's isDead variable to true, triggers NPC death
        }
        else if (currentHealth + amount > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }

        return false;   //sets BattleSystem's isDead var to false so continues the battle
    }
    private void UpdateNPCSkills()//teach NPC each Skill and Ability that the Designer has added via Editor
    {
        int i = 0;
        while (i < AttackAbilities.Count)
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
[System.Serializable]
public class NPCAbility
{
    public string name;
    public int anxietyEffect;//effect on player anxiety
    public int willEffect;//effect on player will
    public int npcEffect;//effect on self
    // public Sprite abilityImage; // UI image for the ability 

}
