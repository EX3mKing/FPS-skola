using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnPoint : MonoBehaviour
{
    private void Start()
    {
        PlayerMovement.instance.gameObject.SetActive(false);
        if (PlayerPrefs.HasKey("SpawnPoint"))
        {
            if (PlayerPrefs.GetString("SpawnPoint",name) == gameObject.name)
            {
                PlayerMovement.instance.gameObject.transform.position = transform.position;
                PlayerMovement.instance.gameObject.transform.rotation = transform.rotation;
            }
        }
        PlayerMovement.instance.gameObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) PlayerPrefs.SetString("SpawnPoint", gameObject.name);
    }
}
