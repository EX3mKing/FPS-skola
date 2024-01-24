using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float deathTimer;
    
    void Start()
    {
        Invoke("SelfDestroy", deathTimer);
    }
    
    private void SelfDestroy()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if(!Physics.Raycast(transform.position, transform.forward, speed * Time.deltaTime)) 
            transform.position += transform.forward * (speed * Time.deltaTime);
        else SelfDestroy();
    }
}
