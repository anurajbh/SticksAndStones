using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LearningSystem : MonoBehaviour
{
    public static LearningSystem instance;
    public List<PlayerAbility> skills; //use this to add skills
    public List<PlayerAbility> attacks; //use this to add attacks

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("fix this: " + gameObject.name);
        }
        else
        {
            instance = this;
        }
    }

    //Used to learn skills
    public void LearnSkills() {
        for (int i = 0; i < skills.Count; i++) {
            if ( !PlayerStats.Instance.skills.Contains(skills[i].name)) {
                PlayerStats.Instance.skills.Learn(skills[i].name, skills[i].anxietyEffect, skills[i].willEffect, skills[i].enemyDamage);
                Debug.Log(skills[i].name + " has been learned!");
            } else {
                Debug.Log(skills[i].name + " has already been learned!");
            }
        }
        //skills.Clear();
    }
    //Used to learn attacks
    public void LearnAttacks() {
        for(int i = 0; i < attacks.Count; i++) {
            if (PlayerStats.Instance.skills == null || !PlayerStats.Instance.attacks.Contains(attacks[i].name)) {
                PlayerStats.Instance.attacks.Learn(attacks[i].name, attacks[i].anxietyEffect, attacks[i].willEffect, attacks[i].enemyDamage);
                Debug.Log(attacks[i].name + " has been learned!");
            } else {
                Debug.Log(attacks[i].name + " has already been learned!");
            }
        }
        //attacks.Clear();
    }
    //Use this if you need to learn both skills and attacks
    public void LearnBoth() {
        LearnAttacks();
        LearnSkills();
    }

    //Use these to add skills and attacks onto the list
    public void AddSkill(PlayerAbility skill) {
        skills.Add(skill);
    }
    public void AddAttack(PlayerAbility attack) {
        attacks.Add(attack);
    }

    public bool isAttacksEmpty() {
        return attacks == null || attacks.Count == 0; 
    }

    public bool isSkillsEmpty() {
        return skills == null || skills.Count == 0;
    }
    
    public void clearLists() {
        attacks.Clear();
        skills.Clear();
    }

    public string toString() {
        string message = "";
        for (int i = 0; i < attacks.Count; i++) {
            message = message + " [" + attacks[i].name + "]";
        } 
        for (int i = 0; i < skills.Count; i++) {
                message = message + " [" + skills[i].name + "]";
        }
        return message;
    }
}
