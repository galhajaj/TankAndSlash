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
        int maxLifeToRegenerateTo = Mathf.CeilToInt(DataManager.Instance.Life);

        if (Mathf.FloorToInt(DataManager.Instance.Life) < maxLifeToRegenerateTo)
            DataManager.Instance.Life += Time.deltaTime * DataManager.Instance.LifeRegenerationRate;

        if (DataManager.Instance.Life > maxLifeToRegenerateTo)
            DataManager.Instance.Life = maxLifeToRegenerateTo;
    }

    private void powerRegeneration()
    {
        if (DataManager.Instance.Power < DataManager.Instance.MaxPower)
            DataManager.Instance.Power += Time.deltaTime * DataManager.Instance.PowerRegenerationRate;
        else
            DataManager.Instance.Power = DataManager.Instance.MaxPower;
    }
}
