using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public float deathTimer;
    public GameObject hitEffect;
    public float hitEffectDistance;
    public LayerMask mask;

    public int dmg;
    
    void Start()
    {
        Invoke("SelfDestroy", deathTimer);
    }
    
    private void SelfDestroy()
    {
        Instantiate(hitEffect, transform.position - transform.forward * hitEffectDistance, transform.rotation);
        Destroy(gameObject);
    }

    private void Update()
    {
        if(!Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, speed * Time.deltaTime, mask)) 
            transform.position += transform.forward * (speed * Time.deltaTime);
        else
        {
            if (hit.collider.CompareTag("Player"))
            {
                hit.collider.gameObject.GetComponent<PlayerHP>().TakeDmg(dmg);
            }
            SelfDestroy();
        }
    }
}