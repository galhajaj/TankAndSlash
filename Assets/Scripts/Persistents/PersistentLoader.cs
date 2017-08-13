﻿using System.Collections;
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

        // hide/show elements by scene name
        /*if (scene.name == "mainScene" || scene.name == "headquartersScene")
        {
            Tank.Instance.transform.localScale = new Vector3(0.0F, 0.0F, 0.0F);
            Inventory.Instance.transform.localScale = new Vector3(0.0F, 0.0F, 0.0F);
        }
        else if (scene.name == "garageScene")
        {
            Tank.Instance.transform.localScale = new Vector3(0.0F, 0.0F, 0.0F);
            Inventory.Instance.transform.localScale = new Vector3(1.0F, 1.0F, 1.0F);
        }
        else if (scene.name == "battlegroundScene")
        {
            Tank.Instance.transform.localScale = new Vector3(1.0F, 1.0F, 1.0F);
            Inventory.Instance.transform.localScale = new Vector3(1.0F, 1.0F, 1.0F);
        }*/
    }

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
}