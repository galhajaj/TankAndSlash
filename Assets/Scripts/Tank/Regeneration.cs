using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : MonoBehaviour
{
	void Start ()
    {

	}
	
	void Update ()
    {
        lifeRegeneration();
        powerRegeneration();
    }

    private void lifeRegeneration()
    {
        int maxLifeToRegenerateTo = Mathf.CeilToInt(DataManager.Instance.TankParams.Life);

        if (Mathf.FloorToInt(DataManager.Instance.TankParams.Life) < maxLifeToRegenerateTo)
            DataManager.Instance.TankParams.Life += Time.deltaTime * DataManager.Instance.TankParams.LifeRegenerationRate;

        if (DataManager.Instance.TankParams.Life > maxLifeToRegenerateTo)
            DataManager.Instance.TankParams.Life = maxLifeToRegenerateTo;
    }

    private void powerRegeneration()
    {
        if (DataManager.Instance.TankParams.Power < DataManager.Instance.TankParams.MaxPower)
            DataManager.Instance.TankParams.Power += Time.deltaTime * DataManager.Instance.TankParams.PowerRegenerationRate;
        else
            DataManager.Instance.TankParams.Power = DataManager.Instance.TankParams.MaxPower;
    }
}
