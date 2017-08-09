using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketManager : MonoBehaviour
{
    public static SocketManager Instance;

    public const string INVENTORY_GRID_NAME = "Grid_Inventory";
    public const string SKILLS_GRID_NAME    = "Grid_Skills";
    public const string TURRETS_GRID_NAME   = "Grid_Turrets";

    private GameObject _inventoryGrid;
    public GameObject InventoryGrid { get { return _inventoryGrid; } }
    private GameObject _skillsGrid;
    public GameObject SkillsGrid { get { return _skillsGrid; } }
    private GameObject _turretsGrid;
    public GameObject TurretsGrid { get { return _turretsGrid; } }

    void Awake()
    {
        Instance = this; // init singletone
    }

    void Start ()
    {
		_inventoryGrid  = GameObject.Find(INVENTORY_GRID_NAME);
        _skillsGrid     = GameObject.Find(SKILLS_GRID_NAME);
        _turretsGrid    = GameObject.Find(TURRETS_GRID_NAME);
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
