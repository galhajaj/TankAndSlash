﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarageUIUpdate : MonoBehaviour
{
    public Text CreditsText;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        CreditsText.text = "Credits: " + DataManager.Instance.TankParams.Credits.ToString();
    }
}
