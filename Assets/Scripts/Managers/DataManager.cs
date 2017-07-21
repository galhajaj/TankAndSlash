using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    void Awake()
    {
        
    }

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    // in new game, load from config file (scriptable object) inside editor
    public void LoadInitialData()
    {
        Debug.Log("new game - load initial data");
    }

    // in continue game, load from save file of the current player
    public void LoadDataFromFile()
    {
        Debug.Log("load game - load from save file");
    }

    // create/update the current player save file
    public void SaveDataToFile()
    {
        Debug.Log("save data to file");
    }
}
