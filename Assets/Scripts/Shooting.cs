using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform gun;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private Camera camera;
    public float distance = 30f;

    private Vector3 startLocation;
    private Vector3 startRotation;

    private void Start()
    {
        startLocation = transform.position;
        startRotation = transform.rotation.eulerAngles;
    }

    private void Update()
    {
        Vector3 targetLocation = Vector3.forward;
        
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, distance);
        print(hit.collider);
        
        if (hit.collider != null) targetLocation = hit.point;
        else targetLocation = camera.transform.forward * distance;
        
        gun.transform.LookAt(targetLocation);
        
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        }
    }
}
