﻿using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// class vehicle for cars in the game
/// </summary>
public class Vehicle : MonoBehaviour
{
    public float MaxVelocity;
    public float Acceleration;
    public float Weight;
    private bool inVehicle = false;
    private bool pauseRadio = false;
    private VehicleMovment vehicleScript;
    private GameObject player;

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
        if (inVehicle && Input.GetKeyDown(KeyCode.E))
        {
            vehicleScript.enabled = false;
            player.SetActive(true);
            player.transform.parent = null;
            inVehicle = false;
            player.transform.position = this.gameObject.transform.position;
            Radio.Instance.Pause();
        }
         if(inVehicle && Input.GetKeyDown(KeyCode.F1)) 
        {
            Radio.Instance.NextChannel();
        }
         if(inVehicle && Input.GetKeyDown(KeyCode.F4))
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
    /// function for accelerating the car
    /// </summary>
    virtual protected void Accelerate()
    {
    }

    /// <summary>
    /// function for slowing down the car
    /// </summary>
    virtual protected void Brake()
    {
    }

    /// <summary>
    /// Triggers when Player object is inside vehicle collider2d area
    /// </summary>
    /// <param name="collision">Collider2D object that is inside vehicle car Collider2D. 
    /// In this case Player</param>
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.F) && inVehicle == false)
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
        inVehicle = true;
        player = collision.gameObject;
        Debug.Log("boom");
        collision.gameObject.SetActive(false);
        collision.transform.parent = this.gameObject.transform;
        vehicleScript.enabled = true; 
    }
}
