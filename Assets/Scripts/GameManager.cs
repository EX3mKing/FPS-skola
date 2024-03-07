using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float deathWaitTime;
    public GameObject player;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) PlayerPrefs.SetString("SpawnPoint", "");
    }

    public void PlayerDeath()
    {
        player.SetActive(false);
        Invoke("ReloadScene", deathWaitTime);
    }
    
    public void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
