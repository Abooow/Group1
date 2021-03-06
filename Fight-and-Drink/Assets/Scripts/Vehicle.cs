﻿using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// class vehicle for cars in the game
/// </summary>
public class Vehicle : MonoBehaviour
{
    public bool IsInVehicle = false;

    private bool pauseRadio = false;
    private VehicleMovment vehicleScript;
    private GameObject player;
    private AudioSource audiosource;

    /// <summary>
    /// Called before the first frame update
    /// </summary>
    public void Start()
    {
        vehicleScript = GetComponent<VehicleMovment>();
        
        vehicleScript.enabled = false;
        
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    void Update()
    {
        if (IsInVehicle && Input.GetKeyDown(KeyCode.E))
        {
            Radio.Instance.Pause();
            CameraTargetFocus.Instance.Target = player.transform;

            CarSound.Instance.PlayCarDoor();
            vehicleScript.enabled = false;
            player.SetActive(true);
            player.transform.parent = null;
            IsInVehicle = false;
            player.transform.position = this.gameObject.transform.position;
        }
         if(IsInVehicle && Input.GetKeyDown(KeyCode.F1)) 
        {
            Radio.Instance.NextChannel();
        }
         if(IsInVehicle && Input.GetKeyDown(KeyCode.F4))
        {
            if (!pauseRadio)
            {
                Radio.Instance.Pause();
                pauseRadio = true;
            }
            else if (pauseRadio)
            {
                Radio.Instance.Resume();
                pauseRadio = false;
            }
        }
   
    }

    /// <summary>
    /// Triggers when Player object is inside vehicle collider2d area
    /// </summary>
    /// <param name="collision">Collider2D object that is inside vehicle car Collider2D. 
    /// In this case Player</param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.F) && IsInVehicle == false)
        {           
            OnEnterVehicle(collision);
        }
    }

    /// <summary>
    /// Used when user is "inside" vehicle 
    /// </summary>
    /// <param name="collision">Collider2D object that is inside vehicle car Collider2D. 
    /// In this case Player</param>
    private void OnEnterVehicle(Collider2D collision)
    {
        Radio.Instance.Resume();
        CameraTargetFocus.Instance.Target = transform;

        IsInVehicle = true;
        CarSound.Instance.PlayCarDoor();
        player = collision.gameObject;
        collision.gameObject.SetActive(false);
        collision.transform.parent = this.gameObject.transform;
        vehicleScript.enabled = true;
    }
}
