using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBoost : MonoBehaviour
{
    public int amount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHP>().Heal(amount);
            Destroy(gameObject);
        }
    }
}
