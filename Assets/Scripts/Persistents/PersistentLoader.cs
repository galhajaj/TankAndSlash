using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentLoader : MonoBehaviour
{
    public GameObject DataManagerObj;
    public GameObject TankObj;
    public GameObject InventoryObj;

    void Awake()
    {
        // destroy persistents in main scene
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "mainScene")
        {
            if (DataManager.Instance != null)
                DestroyImmediate(DataManager.Instance.gameObject);
            if (Tank.Instance != null)
                DestroyImmediate(Tank.Instance.gameObject);
            if (Inventory.Instance != null)
                DestroyImmediate(Inventory.Instance.gameObject);
        }

        // init persistents
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
