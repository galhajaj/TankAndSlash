using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddChipTemp : MonoBehaviour
{
    public GameObject ChipObject;

	public void addRandomChip()
    {
        GameObject grid = GameObject.Find("SocketsGrid");
        int rand = Random.Range(0, grid.transform.childCount - 1);
        Transform randTile = grid.transform.GetChild(rand);
        Instantiate(ChipObject, randTile.position, Quaternion.identity);
        /*foreach (Transform child in grid.transform)
        {
            Instantiate(ChipObject, child.position, Quaternion.identity);
        }*/
    }
    
    // Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
