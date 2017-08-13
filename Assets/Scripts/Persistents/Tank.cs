﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public static Tank Instance;

    private GameObject _currentCannon = null;

    // need to add default values to save time & trouble
    public int MaxLife = 5;
    public float Life = 3;
    public float LifeRegenerationRate = 0.05F; // 1/sec
    public int MaxPower = 3;
    public float Power = 1;
    public float PowerRegenerationRate = 0.02F; // 1/sec
    public float MaxSpeed = 300.0F;
    public float Speed = 300.0F;
    public float AngularVelocity = 300.0F;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public void PutOnTurret(string name)
    {
        GameObject cannon = Resources.Load("Turrets/" + name) as GameObject;
        _currentCannon = Instantiate(cannon, transform.position, transform.rotation);
        _currentCannon.transform.parent = this.transform;
    }

    public void PutOffTurret()
    {
        if (_currentCannon == null)
            return;
        _currentCannon.transform.parent = null;
        DestroyImmediate(_currentCannon.gameObject);
    }
}
