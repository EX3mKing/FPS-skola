using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private CharacterController cc;
    private float inputX;
    private float inputY;
    

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        cc.Move(Camera.main.transform.forward * inputY * speed * Time.deltaTime 
                + Camera.main.transform.right * inputX * speed * Time.deltaTime + Physics.gravity * Time.deltaTime);
    }
}
