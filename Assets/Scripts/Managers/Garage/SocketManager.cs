using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketManager : MonoBehaviour
{
    public static SocketManager Instance;

    private GameObject _inventoryGrid;
    private GameObject _skillsGrid;
    private GameObject _turretsGrid;

    void Awake()
    {
        Instance = this; // init singletone
    }

    void Start ()
    {
		_inventoryGrid  = GameObject.Find("Grid_Inventory");
        _skillsGrid     = GameObject.Find("Grid_Skills");
        _turretsGrid    = GameObject.Find("Grid_Turrets");
    }
	
	void Update ()
    {
		
	}

    public List<Transform> GetFreeSocketsForChipsPack()
    {
        List<Transform> freeSockets = new List<Transform>();

        foreach (Transform tile in _inventoryGrid.transform)
        {
            if (tile.childCount == 0)
            {
                freeSockets.Add(tile);
                if (freeSockets.Count >= ChipManager.Instance.ChipsNumberInPack)
                    return freeSockets;
            }
        }

        return null;
    }
}
