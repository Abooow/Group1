﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public CharacterStats Player;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
        slider.maxValue = Player.MaxHealth;
        slider.value = Player.CurrentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Player.CurrentHealth;
    }

}