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
        // insert bar units
        for (int i = 0; i < Mathf.RoundToInt(DataManager.Instance.TankParams.MaxPower); ++i)
        {
            GameObject powerUnit = Instantiate(BarUnitObject);
            powerUnit.transform.SetParent(this.transform, false);
            _powerUnitsList.Add(powerUnit);
        }
    }

    void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < _powerUnitsList.Count; ++i)
        {
            if (Mathf.FloorToInt(DataManager.Instance.TankParams.Power) > i)
            {
                _powerUnitsList[i].transform.Find("Fill").transform.localScale = new Vector3(1.0F, 1.0F, 1.0F);
            }
            else if (Mathf.FloorToInt(DataManager.Instance.TankParams.Power) == i)
            {
                float ratio = DataManager.Instance.TankParams.Power - Mathf.FloorToInt(DataManager.Instance.TankParams.Power);
                _powerUnitsList[i].transform.Find("Fill").transform.localScale = new Vector3(ratio, 1.0F, 1.0F);
            }
            else
            {
                _powerUnitsList[i].transform.Find("Fill").transform.localScale = new Vector3(0.0F, 1.0F, 1.0F);
            }
        }
    }
}
