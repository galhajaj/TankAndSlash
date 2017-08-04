using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garage_ButtonCommands : MonoBehaviour
{
    public GameObject ChipObject;

	public void AddRandomChip()
    {
        // get the next free inventory socket ( = not contains child)
        GameObject grid = GameObject.Find("Grid_Inventory");
        Transform chosenFreeTile = null;
        foreach (Transform tile in grid.transform)
        {
            if (tile.childCount == 0)
            {
                chosenFreeTile = tile;
                break;
            }
        }
        
        // return if inventory is full
        if (chosenFreeTile == null)
            return;

        // return if can't afford
        if (DataManager.Instance.TankParams.Credits < DataManager.Instance.TankParams.ChipCost)
            return;

        // pay & save
        DataManager.Instance.TankParams.Credits -= DataManager.Instance.TankParams.ChipCost;
        DataManager.Instance.SaveDataToFile();

        // add new chip
        GameObject newChip = Instantiate(ChipObject, chosenFreeTile.position, Quaternion.identity);
        newChip.transform.parent = chosenFreeTile;
        newChip.GetComponent<Chip>().initRandomType();
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
}
