using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public enum PackType
    {
        REGULAR,
        PREMIUM,
        SUPER
    }

    public GameObject ChipObject;
    public int ChipsNumberInPack = 5;
    public int RegularPackCost = 100;
    public int PremiumPackCost = 250;
    public int SuperPackCost = 500;

    public const string INVENTORY_GRID_NAME = "Grid_Inventory";
    public const string SKILLS_GRID_NAME = "Grid_Skills";
    public const string TURRETS_GRID_NAME = "Grid_Turrets";

    private GameObject _inventoryGrid;
    public GameObject InventoryGrid { get { return _inventoryGrid; } }
    private GameObject _skillsGrid;
    public GameObject SkillsGrid { get { return _skillsGrid; } }
    private GameObject _turretsGrid;
    public GameObject TurretsGrid { get { return _turretsGrid; } }

    void Start()
    {
        _inventoryGrid = GameObject.Find(INVENTORY_GRID_NAME);
        _skillsGrid = GameObject.Find(SKILLS_GRID_NAME);
        _turretsGrid = GameObject.Find(TURRETS_GRID_NAME);
    }

    void Update()
    {

    }

    private int getCost(PackType packType)
    {
        if (packType == PackType.REGULAR)
            return RegularPackCost;
        if (packType == PackType.PREMIUM)
            return PremiumPackCost;
        if (packType == PackType.SUPER)
            return SuperPackCost;
        return 0;
    }

    public void BuyPack(PackType packType)
    {
        // check if can afford
        int cost = getCost(packType);
        if (DataManager.Instance.Saved.Credits < cost)
            return;

        // check free sockets
        List<Transform> freeSockets = getFreeSocketsForChipsPack();
        if (freeSockets == null)
            return;

        // pay
        DataManager.Instance.Saved.Credits -= cost;

        // add chips
        for (int i = 0; i < freeSockets.Count; ++i)
            createRandomChip(freeSockets[i]);

        // save to file
        DataManager.Instance.SaveDataToFile();
    }

    public void PutOnChip(GameObject chip, Transform socket)
    {
        chip.transform.position = socket.position;
        chip.transform.parent = socket;

        Chip chipScript = chip.GetComponent<Chip>();
        chipScript.SocketName = socket.name;
        chipScript.GridName = socket.parent.name;

        if (chipScript.GridName != "Grid_Inventory")
        {
            chipScript.Install();
            if (chipScript.GridName == "Grid_Turrets")
            {
                deactivateActiveTurret();
                chipScript.IsActive = true;
            }
        }
    }

    public void PutOffChip(GameObject chip)
    {
        Chip chipScript = chip.GetComponent<Chip>();

        if (chipScript.GridName != "Grid_Inventory")
        {
            chipScript.Uninstall();
            if (chipScript.GridName == "Grid_Turrets" && chipScript.IsActive)
            {
                chipScript.IsActive = false;
                activateNextTurret(chipScript.ChipID);
            }
        }
    }

    private void createRandomChip(Transform socket)
    {
        GameObject newChip = Instantiate(ChipObject);

        int randomChipType = UnityEngine.Random.Range(0, 4);

        string resourcesFolder = "";
        Chip.ChipType chipType = Chip.ChipType.NONE;
        if (randomChipType == 0) // const chip
        {
            resourcesFolder = "Const";
            chipType = Chip.ChipType.CONST;
        }
        else if (randomChipType == 1) // turret chip
        {
            resourcesFolder = "Turret";
            chipType = Chip.ChipType.TURRET;
        }
        else if (randomChipType == 2) // consumable chip
        {
            resourcesFolder = "Consumable";
            chipType = Chip.ChipType.CONSUMABLE;
        }
        else if (randomChipType == 3) // state chip
        {
            resourcesFolder = "State";
            chipType = Chip.ChipType.STATE;
        }

        UnityEngine.Object[] allChips = Resources.LoadAll("ChipScripts/" + resourcesFolder);
        int randomChipNumber = UnityEngine.Random.Range(0, allChips.Length);
        string chipName = allChips[randomChipNumber].name;

        Chip chipScript = newChip.AddComponent(Type.GetType(chipName)) as Chip;

        PutOnChip(newChip, socket);

        chipScript.Type = chipType;

        chipScript.ChipName = chipName;
        chipScript.ChipID = DataManager.Instance.GetNextChipID();
    }

    private void createChipByData(ChipData chipData)
    {
        // create
        GameObject newChip = Instantiate(ChipObject);

        // get chips socket
        GameObject grid = GameObject.Find(chipData.GridName);
        Transform socket = grid.transform.Find(chipData.SocketName);

        Chip chipScript = newChip.AddComponent(Type.GetType(chipData.ChipName)) as Chip;
        PutOnChip(newChip, socket);
        chipScript.ChipID = chipData.ChipID;
        chipScript.ChipName = chipData.ChipName;
        chipScript.Type = chipData.ChipType;
    }

    public void FillChipsListFromDataManager()
    {
        foreach (ChipData chipData in DataManager.Instance.Saved.ChipsData)
        {
            createChipByData(chipData);
        }
    }

    private List<Transform> getFreeSocketsForChipsPack()
    {
        List<Transform> freeSockets = new List<Transform>();

        foreach (Transform tile in _inventoryGrid.transform)
        {
            if (tile.childCount == 0)
            {
                freeSockets.Add(tile);
                if (freeSockets.Count >= ChipsNumberInPack)
                    return freeSockets;
            }
        }

        return null;
    }

    private void deactivateActiveTurret()
    {
        foreach (Transform tile in _turretsGrid.transform)
        {
            if (tile.childCount > 0)
            {
                Chip chipScript = tile.GetChild(0).GetComponent<Chip>();
                if (chipScript.IsActive)
                    chipScript.IsActive = false;
            }
        }
    }

    private void activateNextTurret(int removedChipId)
    {
        foreach (Transform tile in _turretsGrid.transform)
        {
            if (tile.childCount > 0)
            {
                Chip chipScript = tile.GetChild(0).GetComponent<Chip>();
                if (chipScript.ChipID != removedChipId)
                {
                    chipScript.IsActive = true;
                    return;
                }
            }
        }
    }
}
