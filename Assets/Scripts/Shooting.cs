using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Tooltip("Gun or Obj")]
    [SerializeField] private Transform rotateTarget;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject laserPoint;
    [SerializeField] private LayerMask mask;
    [SerializeField] private LineRenderer lineRenderer;
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
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        }
    }

    private void LateUpdate()
    {
        Vector3 targetLocation = Vector3.forward;
        
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, distance, mask);
        print(hit.collider);
        
        if (hit.collider != null) targetLocation = hit.point;
        else targetLocation = camera.transform.forward * distance;
        
        RaycastHit gunHit = new RaycastHit();
        Physics.Raycast(rotateTarget.position, (targetLocation - rotateTarget.position), out gunHit, mask);
        if (gunHit.collider != null)
        {
            laserPoint.SetActive(true);
            laserPoint.transform.position = gunHit.point;
            lineRenderer.SetPosition(0, gunHit.point);
            lineRenderer.SetPosition(1, rotateTarget.position);
        }
        else
        {
            lineRenderer.SetPosition(0, targetLocation);
            lineRenderer.SetPosition(1, rotateTarget.position);
            laserPoint.SetActive(false);
        }
        
        rotateTarget.transform.LookAt(targetLocation, transform.up);
    }
}
