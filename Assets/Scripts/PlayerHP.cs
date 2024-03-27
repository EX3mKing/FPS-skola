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

    public Image effectImage;
    public float effectAlpha;
    public float effectSpeed;
    
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
        
        effectImage.color = new Color(effectImage.color.r, effectImage.color.g, effectImage.color.b, 
            Mathf.MoveTowards(effectImage.color.a, 0f, effectSpeed * Time.deltaTime));
    }

    public void TakeDmg(int dmg)
    {
        if (invicibilityTimer > 0) return;
        currentHP -= dmg;
        invicibilityTimer = invincibilityDuration;
        if (currentHP <= 0) GameManager.Instance.PlayerDeath();
        
        effectImage.color = new Color(effectImage.color.r, effectImage.color.g, effectImage.color.b, effectAlpha);
    }
    
    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP) currentHP = maxHP;
    }
}
