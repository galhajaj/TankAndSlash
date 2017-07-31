using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageButtonCommands : MonoBehaviour
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
        Instantiate(ChipObject, chosenFreeTile.position, Quaternion.identity).transform.parent = chosenFreeTile;

        /*GameObject grid = GameObject.Find("SocketsGrid");
        int rand = Random.Range(0, grid.transform.childCount - 1);
        Transform randTile = grid.transform.GetChild(rand);
        Instantiate(ChipObject, randTile.position, Quaternion.identity);*/
        /*foreach (Transform child in grid.transform)
        {
            Instantiate(ChipObject, child.position, Quaternion.identity);
        }*/
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
}
