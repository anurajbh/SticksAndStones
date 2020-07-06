using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Slider hpSlider;
    public Slider willSlider;

    public void SetEnemyHUD(NPC enemy)
    {
        nameText.text = enemy.enemyName;
        hpSlider.maxValue = enemy.maxHealth;
        hpSlider.value = enemy.currentHealth;
    }

    public void SetPlayerHUD()
    {
        int maxWill = PlayerStats.Instance.maxAmb + PlayerStats.Instance.maxInt;
        willSlider.maxValue = maxWill;
        willSlider.value = PlayerStats.Instance.totalWill;
        //Debug.Log(PlayerStats.Instance.totalWill);
        //Debug.Log(willSlider.maxValue);
    }
}
