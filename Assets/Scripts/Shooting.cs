using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Tooltip("Gun or Obj")]
    private Transform rotateTarget;
    private Transform bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject laserPoint;
    [SerializeField] private LayerMask mask;
    [SerializeField] private LineRenderer lineRenderer;
    public TextMeshProUGUI ammoText;
    public float distance = 30f;

    public Weapon currentWeapon;

    private Vector3 startLocation;
    private Vector3 startRotation;

    private void Start()
    {
        startLocation = transform.position;
        startRotation = transform.rotation.eulerAngles;
        rotateTarget = currentWeapon.bulletSpawn;
        bulletSpawn = currentWeapon.bulletSpawn;
    }

    private void Update()
    {
        ammoText.text = currentWeapon.ammoCurrent.ToString();
        if (currentWeapon.automatic){
            if (Input.GetMouseButton(0) && currentWeapon.currentTimeBetweenShots <= 0f && currentWeapon.ammoCurrent > 0)
            {
                Instantiate(currentWeapon.bullet, currentWeapon.bulletSpawn.position, currentWeapon.bulletSpawn.rotation);
                currentWeapon.currentTimeBetweenShots = currentWeapon.fireRate;
                currentWeapon.ammoCurrent--;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && currentWeapon.ammoCurrent > 0)
            {
                Instantiate(currentWeapon.bullet, currentWeapon.bulletSpawn.position,
                    currentWeapon.bulletSpawn.rotation);
                currentWeapon.ammoCurrent--;
            }
        }
            
    }

    private void LateUpdate()
    {
        Vector3 targetLocation = Vector3.forward;
        
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, distance, mask);

        if (hit.collider != null) targetLocation = hit.point;
        else targetLocation = camera.transform.forward * distance;
        rotateTarget.transform.LookAt(targetLocation, Vector3.up);
        
        if(!currentWeapon.laser) return;
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
        
    }
}
