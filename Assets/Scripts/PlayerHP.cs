using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public static PlayerHP instance;
    public int maxHP = 100;
    private int currentHP;
    public float invincibilityDuration = 1f;
    private float invicibilityTimer;

    public Image HpPadding;
    public Image HpCur;
    
    private void Awake()
    {
        instance = this;
        currentHP = maxHP;
    }

    public void Update()
    {
        if (invicibilityTimer > 0) invicibilityTimer -= Time.deltaTime;
    }

    public void TakeDmg(int dmg)
    {
        if (invicibilityTimer > 0) return;
        currentHP -= dmg;
        invicibilityTimer = invincibilityDuration;
        if (currentHP <= 0) print("die");
    }
}
