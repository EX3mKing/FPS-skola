using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform camera;
    public float sensitivity;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity;
        
        transform.Rotate(Vector3.up * mouseX);
        
        if (camera.rotation.eulerAngles.x + mouseY < 90 || camera.rotation.eulerAngles.x + mouseY > 270)
            camera.Rotate(Vector3.right * mouseY);
    }
}
