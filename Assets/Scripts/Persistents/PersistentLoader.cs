using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentLoader : MonoBehaviour
{
    public GameObject DataManagerObj;
    public GameObject TankObj;
    public GameObject InventoryObj;

    void Awake()
    {
        if (DataManager.Instance == null)
            Instantiate(DataManagerObj);
        if (Tank.Instance == null)
            Instantiate(TankObj);
        if (Inventory.Instance == null)
            Instantiate(InventoryObj);
    }

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
}
