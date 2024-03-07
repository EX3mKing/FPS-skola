using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletSpawn;
    public bool automatic;
    public bool laser;
    public float fireRate;
    public float currentTimeBetweenShots;
    public int ammo;
    public int ammoCurrent;

    private void Update()
    {
        if (currentTimeBetweenShots > 0) currentTimeBetweenShots -= Time.deltaTime;
    }
}
