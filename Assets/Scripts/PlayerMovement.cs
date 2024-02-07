using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speedNormal = 5f;
    public float speedSprint = 10f;
    public float speedCrouch = 2f;
    
    public float jump = 20f;
    public int numOfJumps = 2;
    Vector3 moveInput = Vector3.zero;
    
    /*
    [Header("head bob")]
    public float headBobMovementAmount = 1f;
    public float headBobSpeedNormal = 1f;
    public float headBobSpeedSprint = 2f;
    public float headBobSpeedCrouch = 0.5f;
    public float headBobStartHeight = 0f;
    public float headBobTimer = 0f;
    */
    
    private CharacterController cc;
    private Vector3 curGravity;
    private int curJumps = 2;
    
    public Transform camera;
    public float sensitivity;
    
    public static PlayerMovement instance;

    private void Awake()
    {
        instance = this;
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        CameraAndPlayerRotation();
        
        moveInput = Vector3.zero;
        moveInput += transform.right * Input.GetAxisRaw("Horizontal");
        moveInput += transform.forward *  Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        // HEADBOB DON'T USE
        /*
        float headBobSpeed = 0;
        if (moveInput.magnitude > 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                moveInput *= speedSprint;
                headBobSpeed = headBobSpeedSprint;
            }
            else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            {
                moveInput *= speedCrouch;
                headBobSpeed = headBobSpeedCrouch;
            }
            else 
            {
                moveInput *= speedNormal;
                headBobSpeed = headBobSpeedNormal;
            }
            
            headBobTimer += headBobSpeed * Time.deltaTime;
            camera.localPosition = new Vector3
                (0, headBobStartHeight + Mathf.Sin(headBobTimer) * headBobMovementAmount, 0);
            // ukoliko kamera nije centrirana na playera    
            //(camera.localPosition.x, headBobStartHeight + Mathf.Sin(headBobTimer) * headBobMovementAmount, camera.localPosition.z);
        }
        else
        {
            headBobTimer = 0;
        }
        */
        
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        { moveInput *= speedSprint; }
        else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        { moveInput *= speedCrouch; }
        else 
        { moveInput *= speedNormal; }
        
        if (Input.GetButtonDown("Jump")) moveInput.y = 1;
        if(cc.isGrounded)
        { 
            curJumps = numOfJumps; 
            curGravity.y = 0; 
        }
        else curGravity += Physics.gravity * Time.deltaTime;
        
        if (moveInput.y > 0 && curJumps > 0)
        {
            curJumps--;
            curGravity.y = jump;
        }

        cc.Move((moveInput + curGravity) * Time.deltaTime);
    }

    private void CameraAndPlayerRotation()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity;
        
        transform.Rotate(Vector3.up * mouseX);
        if (camera.rotation.eulerAngles.x - mouseY < 90 || camera.rotation.eulerAngles.x - mouseY > 270)
            camera.Rotate(Vector3.right * -mouseY);
    }
}
