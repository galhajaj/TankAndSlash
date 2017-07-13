using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMapOnEsc_Temp : MonoBehaviour
{

	void Start ()
    {
        Debug.Log("NOTE: PLEASE PLEASE DO NOT FORGET TO DELETE BackToMapOnEsc_Temp SCRIPT IN MANAGER... PLEASE!");
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("headquartersScene");
	}
}
