using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public static PlayerHP instance;
    public int maxHP = 100;
    private int currentHP;
    public float invincibilityDuration = 1f;
    private float invicibilityTimer;

    public Slider hpBar;
    public TextMeshProUGUI hpText;
    
    private void Awake()
    {
        instance = this;
        currentHP = maxHP;
    }

    public void Update()
    {
        if (invicibilityTimer > 0) invicibilityTimer -= Time.deltaTime;
        hpBar.value = (float)currentHP / (float)maxHP;
        hpText.text = "HP: " + currentHP;
    }

    public void TakeDmg(int dmg)
    {
        if (invicibilityTimer > 0) return;
        currentHP -= dmg;
        invicibilityTimer = invincibilityDuration;
        if (currentHP <= 0) GameManager.Instance.PlayerDeath();
    }
    
    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP) currentHP = maxHP;
    }
}
