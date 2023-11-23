using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action shootInput;
    public static Action reloadInput;
    [SerializeField] public AudioSource reloadSound;

    private void Start()
    {
        reloadSound = GetComponent<AudioSource>();

    }
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if (shootInput == null)
                Debug.Log("NULLLLLL APRENDE A PROGRAMAR BURROOOOO");
            shootInput?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            reloadSound.Play();

            reloadInput?.Invoke();
        }
    }
}
