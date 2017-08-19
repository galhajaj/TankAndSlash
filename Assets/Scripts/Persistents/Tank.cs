using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public static Tank Instance;

    private GameObject _currentTurret = null;

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

    public void PutOnTurret(GameObject turretGameObject)
    {
        _currentTurret = Instantiate(turretGameObject, transform.position, transform.rotation);
        _currentTurret.transform.parent = this.transform;
    }

    public void PutOffTurret()
    {
        if (_currentTurret == null)
            return;
        _currentTurret.transform.parent = null;
        DestroyImmediate(_currentTurret.gameObject);
    }
}
