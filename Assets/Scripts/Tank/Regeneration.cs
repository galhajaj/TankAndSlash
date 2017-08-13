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
        int maxLifeToRegenerateTo = Mathf.CeilToInt(Tank.Instance.Life);

        if (Mathf.FloorToInt(Tank.Instance.Life) < maxLifeToRegenerateTo)
            Tank.Instance.Life += Time.deltaTime * Tank.Instance.LifeRegenerationRate;

        if (Tank.Instance.Life > maxLifeToRegenerateTo)
            Tank.Instance.Life = maxLifeToRegenerateTo;
    }

    private void powerRegeneration()
    {
        if (Tank.Instance.Power < Tank.Instance.MaxPower)
            Tank.Instance.Power += Time.deltaTime * Tank.Instance.PowerRegenerationRate;
        else
            Tank.Instance.Power = Tank.Instance.MaxPower;
    }
}
