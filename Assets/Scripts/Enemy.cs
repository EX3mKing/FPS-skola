using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour

{
    public Animator anim;
    public GameObject bulletSpawn;
    public GameObject bullet;
    public int hp = 30;
    //public Rigidbody rb;
    //public float speed;
    
    public float timeToReturn = 5f;
    public float distanceChase = 15f;
    public float distanceStopChase = 20f; 
    public float distanceAttack = 2f;
    public float timeBetweenAttacks = 1f;
    public float attackWaitTimer;

    private Vector3 playerPosition;
    private Vector3 startPosition;
    private bool chase = false;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
    }


    private void Update()
    {
        playerPosition = PlayerMovement.instance.transform.position;
        attackWaitTimer -= Time.deltaTime;
        //playerPosition.y = transform.position.y;
        
        float distance = Vector3.Distance(transform.position, playerPosition);

        if (!chase && distance < distanceChase)
        {
            chase = true;
            CancelInvoke("ReturnToStart");
        }
        if (chase)
        {
            if (distance > distanceAttack)
            {
                //transform.LookAt(playerPosition);
                //rb.velocity = transform.forward * speed;
                agent.SetDestination(playerPosition);
            }
            else
            {
                //rb.velocity = Vector3.zero;
                chase = false;
                agent.SetDestination(transform.position);
                Shoot();
                
            }
            
            if (distance > distanceStopChase)
            {
                //rb.velocity = Vector3.zero;
                chase = false;
                agent.SetDestination(transform.position);
                Invoke("ReturnToStart", timeToReturn);
            }
        }
        
        anim.SetBool("Walk", agent.velocity.magnitude > 0f);
    }
    
    private void ReturnToStart()
    {
        agent.SetDestination(startPosition);
    }

    public void hit(int dmg)
    {
        hp -= dmg;
        
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Shoot()
    {
        if (attackWaitTimer > 0) return;
        attackWaitTimer = timeBetweenAttacks;
        bulletSpawn.transform.LookAt(PlayerHP.instance.transform);
        Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
    }
}
