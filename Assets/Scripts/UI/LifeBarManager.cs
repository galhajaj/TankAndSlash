using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarManager : MonoBehaviour
{
    public GameObject BarUnitObject;

    private List<GameObject> _lifeUnitsList = new List<GameObject>();

	void Start ()
    {

	}
	
	void Update ()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        // update bar numbers
        while (_lifeUnitsList.Count < Mathf.RoundToInt(DataManager.Instance.TankParams.MaxLife))
        {
            GameObject lifeUnit = Instantiate(BarUnitObject);
            lifeUnit.transform.SetParent(this.transform, false);
            _lifeUnitsList.Add(lifeUnit);
        }

        // fill bars
        for (int i = 0; i < _lifeUnitsList.Count; ++i)
        {
            float ratio = 0.0F;

            if (Mathf.FloorToInt(DataManager.Instance.TankParams.Life) > i)
                ratio = 1.0F;
            else if (Mathf.FloorToInt(DataManager.Instance.TankParams.Life) == i)
                ratio = DataManager.Instance.TankParams.Life - Mathf.FloorToInt(DataManager.Instance.TankParams.Life);

            _lifeUnitsList[i].transform.Find("Fill").transform.localScale = new Vector3(ratio, 1.0F, 1.0F);
        }
    }
}
