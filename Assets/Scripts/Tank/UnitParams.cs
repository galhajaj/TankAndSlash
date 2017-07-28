using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitParams : MonoBehaviour
{
    private float _life;
    public float Life
    {
        get
        {
            return _life;
        }
    }

	void Start ()
    {
        _life = DataManager.Instance.TankParams.MaxLife;	
	}
	
	void Update ()
    {
        //Debug.Log(_life);
        lifeRegeneration();
    }

    public void TakeDamage(float damage)
    {
        _life -= damage;
    }

    private void lifeRegeneration()
    {
        if (_life < DataManager.Instance.TankParams.MaxLife)
            _life += Time.deltaTime * DataManager.Instance.TankParams.LifeRegenerationRate;
        else
            _life = DataManager.Instance.TankParams.MaxLife;
    }
}
