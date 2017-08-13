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

    public List<GameObject> Chips = new List<GameObject>();

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
        List<Transform> freeSockets = _getFreeSocketsForChipsPack();
        if (freeSockets == null)
            return;

        // pay
        DataManager.Instance.Saved.Credits -= cost;

        // add chips
        for (int i = 0; i < freeSockets.Count; ++i)
            createRandomChip(freeSockets[i]);
    }

    public void PutOnChip(GameObject chip, Transform socket)
    {
        chip.transform.position = socket.position;
        chip.transform.parent = socket;

        Chip chipScript = chip.GetComponent<Chip>();
        chipScript.SocketName = socket.name;
        chipScript.GridName = socket.parent.name;

        if (chipScript.GridName != "Grid_Inventory")
            chipScript.Install();
    }

    public void PutOffChip(GameObject chip)
    {
        Chip chipScript = chip.GetComponent<Chip>();

        if (chipScript.GridName != "Grid_Inventory")
            chipScript.Uninstall();
    }

    private void createRandomChip(Transform socket)
    {
        GameObject newChip = Instantiate(ChipObject);

        int randomChipType = UnityEngine.Random.Range(0, 2);

        string resourcesFolder = "";
        Chip.ChipType chipType;
        if (randomChipType == 0) // const chip
        {
            resourcesFolder = "Const";
            chipType = Chip.ChipType.CONST;
        }
        else// if (randomChipType == 1) // turret chip
        {
            resourcesFolder = "Turret";
            chipType = Chip.ChipType.TURRET;
        }

        UnityEngine.Object[] allChips = Resources.LoadAll("ChipScripts/" + resourcesFolder);
        int randomChipNumber = UnityEngine.Random.Range(0, allChips.Length);
        string chipName = allChips[randomChipNumber].name;

        Chip chipScript = newChip.AddComponent(Type.GetType(chipName)) as Chip;

        PutOnChip(newChip, socket);

        chipScript.Type = chipType;

        chipScript.ChipName = chipName;
        chipScript.ChipID = DataManager.Instance.GetNextChipID();

        Chips.Add(newChip);

        // update chips data
        UpdateChipsDataInDataManager();
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

        Chips.Add(newChip);
    }

    public void FillChipsListFromDataManager()
    {
        foreach (ChipData chipData in DataManager.Instance.Saved.ChipsData)
        {
            createChipByData(chipData);
        }
    }

    public void UpdateChipsDataInDataManager()
    {
        // clear current data
        DataManager.Instance.Saved.ChipsData.Clear();

        // renew
        foreach (GameObject chip in Chips)
        {
            ChipData chipData = chip.GetComponent<Chip>().GetChipData();
            DataManager.Instance.Saved.ChipsData.Add(chipData);
        }

        // save
        DataManager.Instance.SaveDataToFile();
    }

    private List<Transform> _getFreeSocketsForChipsPack()
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
}
