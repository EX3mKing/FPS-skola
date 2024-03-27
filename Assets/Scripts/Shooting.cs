using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

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
    
    public List<Weapon> weapons;
    public List<Weapon> weaponsNotFound;
    public int currentWeaponIndex;

    private float zoomStart;
    private float zoomTarget;
    public float zoomSpeed;
    
    private Vector3 aimStartPosition;
    public Transform AimPosition;
    public float aimSpeed;
    
    public PostProcessVolume postProcessVolume;
    private DepthOfField dof;

    private void Start()
    {
        zoomStart = Camera.main.fieldOfView;
        startLocation = transform.position;
        startRotation = transform.rotation.eulerAngles;
        rotateTarget = currentWeapon.bulletSpawn;
        bulletSpawn = currentWeapon.bulletSpawn;
        SetCurrentWeapon(0);
        aimStartPosition = currentWeapon.transform.localPosition;
        postProcessVolume.profile.TryGetSettings<DepthOfField>(out dof);
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

        if (Input.GetMouseButton(1)) ZoomIn(); 
        else ZoomOut();
        
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetCurrentWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetCurrentWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetCurrentWeapon(2);
        if (Input.GetKeyDown(KeyCode.Tab)) RotateCurrentWeapon();
    }

    private void LateUpdate()
    {
        Vector3 targetLocation = Vector3.forward;
        
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, distance, mask);

        if (hit.collider != null) targetLocation = hit.point;
        else targetLocation = camera.transform.forward * distance;
        rotateTarget.transform.LookAt(targetLocation, Vector3.up);
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, zoomTarget, Time.deltaTime * zoomSpeed);
        
        dof.focusDistance.value = Vector3.Distance(hit.point, transform.position);

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
    
    public void SetCurrentWeapon(int index)
    {
        if (index > weapons.Count - 1) return;
        currentWeapon.gameObject.SetActive(false);
        currentWeapon = weapons[index];
        currentWeaponIndex = index;
        currentWeapon.gameObject.SetActive(true);
        rotateTarget = currentWeapon.bulletSpawn;
        bulletSpawn = currentWeapon.bulletSpawn;
        zoomTarget = zoomStart;
        if (currentWeapon.laser)
        {
            lineRenderer.enabled = true;
            laserPoint.SetActive(true);
        }
        else
        {
            lineRenderer.enabled = false;
            laserPoint.SetActive(false);
        }
    }

    private void RotateCurrentWeapon()
    {
        currentWeaponIndex++;
        currentWeaponIndex %= weapons.Count;
        SetCurrentWeapon(currentWeaponIndex);
    }
    
    private void ZoomIn()
    {
        zoomTarget = currentWeapon.zoom;
        currentWeapon.transform.position = Vector3.MoveTowards(currentWeapon.transform.position, 
            AimPosition.position, aimSpeed * Time.deltaTime);
    }
    
    private void ZoomOut()
    {
        zoomTarget = zoomStart;
        currentWeapon.transform.localPosition = Vector3.MoveTowards(currentWeapon.transform.localPosition, 
            aimStartPosition, aimSpeed * Time.deltaTime);
    }
    
    public void PickUpWeapon(Weapon weapon)
    {
        if (!weaponsNotFound.Contains(weapon)) return;
        weapons.Add(weapon);
        weaponsNotFound.Remove(weapon);
        weapon.gameObject.SetActive(false);
        SetCurrentWeapon(weapons.Count - 1);
    }
}
