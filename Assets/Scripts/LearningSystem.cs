using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningSystem : MonoBehaviour
{
    public PlayerAbility skill; // use this to add one skill
    public PlayerAbility attack; // use this to add one attack
    public List<PlayerAbility> skills; //use this to add skills
    public List<PlayerAbility> attacks; //use this to add attacks

    //Used to learn singular skill
    public void learnSkill() {
        if (!SMPlayerStats.Instance.skills.Contains(skill.name) && skill != null) {
            SMPlayerStats.Instance.skills.Learn(skill.name, skill.anxietyEffect, skill.willEffect, skill.enemyDamage);
        }
    } 
    //Used to learn singular attack
    public void learnAttack() {
        if (!SMPlayerStats.Instance.attacks.Contains(attack.name) && attack != null) {
            SMPlayerStats.Instance.attacks.Learn(attack.name, attack.anxietyEffect, attack.willEffect, attack.enemyDamage);
        }
    } 
    //Used to learn multiple skills
    public void learnMultipleSkills() {
        for (int i = 0; i < skills.Count; i++) {
            if (!SMPlayerStats.Instance.skills.Contains(skills[i].name)) {
                SMPlayerStats.Instance.skills.Learn(skills[i].name, skills[i].anxietyEffect, skills[i].willEffect, skills[i].enemyDamage);
                //broadcast you learned a skill?
            }
        }
    }
    //Used to learn multiple attacks
    public void learnMultipleAttacks() {
        for(int i = 0; i < attacks.Count; i++) {
            if (!SMPlayerStats.Instance.attacks.Contains(attacks[i].name)) {
                SMPlayerStats.Instance.attacks.Learn(attacks[i].name, attacks[i].anxietyEffect, attacks[i].willEffect, attacks[i].enemyDamage);
                //broadcast you learned an attack?
            }
        }
    }
}
