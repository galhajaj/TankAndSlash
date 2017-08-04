using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBarManager : MonoBehaviour
{
    public GameObject BarUnitObject;

    private List<GameObject> _powerUnitsList = new List<GameObject>();

    void Start()
    {

    }

    void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        // update bar numbers
        while (_powerUnitsList.Count <  Mathf.RoundToInt(DataManager.Instance.TankParams.MaxPower))
        {
            GameObject powerUnit = Instantiate(BarUnitObject);
            powerUnit.transform.SetParent(this.transform, false);
            _powerUnitsList.Add(powerUnit);
        }

        // fill bars
        for (int i = 0; i < _powerUnitsList.Count; ++i)
        {
            float ratio = 0.0F;

            if (Mathf.FloorToInt(DataManager.Instance.TankParams.Power) > i)
                ratio = 1.0F;
            else if (Mathf.FloorToInt(DataManager.Instance.TankParams.Power) == i)
                ratio = DataManager.Instance.TankParams.Power - Mathf.FloorToInt(DataManager.Instance.TankParams.Power);

            _powerUnitsList[i].transform.Find("Fill").transform.localScale = new Vector3(ratio, 1.0F, 1.0F);

        }
    }
}
