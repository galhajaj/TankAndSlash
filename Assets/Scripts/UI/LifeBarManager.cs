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
        // insert bar units
        for (int i = 0; i < Mathf.RoundToInt(DataManager.Instance.TankParams.MaxLife); ++i)
        {
            GameObject lifeUnit = Instantiate(BarUnitObject);
            lifeUnit.transform.SetParent(this.transform, false);
            _lifeUnitsList.Add(lifeUnit);
        }
	}
	
	void Update ()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < _lifeUnitsList.Count; ++i)
        {
            if (Mathf.FloorToInt(DataManager.Instance.TankParams.Life) > i)
            {
                _lifeUnitsList[i].transform.Find("Fill").transform.localScale = new Vector3(1.0F, 1.0F, 1.0F);
            }
            else if (Mathf.FloorToInt(DataManager.Instance.TankParams.Life) == i)
            {
                float ratio = DataManager.Instance.TankParams.Life - Mathf.FloorToInt(DataManager.Instance.TankParams.Life);
                _lifeUnitsList[i].transform.Find("Fill").transform.localScale = new Vector3(ratio, 1.0F, 1.0F);
            }
            else
            {
                _lifeUnitsList[i].transform.Find("Fill").transform.localScale = new Vector3(0.0F, 1.0F, 1.0F);
            }
        }
    }
}
