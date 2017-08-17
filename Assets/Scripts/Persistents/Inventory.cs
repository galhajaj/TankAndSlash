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
            if (chipScript.Type == Chip.ChipType.TURRET || chipScript.Type == Chip.ChipType.SKILL)
            {
                ActivateChipAndDeactivateAllOthers(socket);
            }
        }
    }

    public void PutOffChip(GameObject chip)
    {
        Chip chipScript = chip.GetComponent<Chip>();

        if (chipScript.GridName != "Grid_Inventory")
        {
            chipScript.Uninstall();
            if (chipScript.Type == Chip.ChipType.TURRET || chipScript.Type == Chip.ChipType.SKILL)
            {
                if (chipScript.IsActive)
                    DeactivateActiveChipAndActivateNextOne(chipScript.Type, false);
            }
        }
    }

    private void createRandomChip(Transform socket)
    {
        GameObject newChip = Instantiate(ChipObject);

        int randomChipType = UnityEngine.Random.Range(0, 5);

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
        else if (randomChipType == 4) // skill chip
        {
            resourcesFolder = "Skill";
            chipType = Chip.ChipType.SKILL;
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
        chipScript.ChipID = chipData.ChipID;
        chipScript.ChipName = chipData.ChipName;
        chipScript.Type = chipData.ChipType;
        PutOnChip(newChip, socket);
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

    public void ActivateChipAndDeactivateAllOthers(Transform tile)
    {
        // return if empty tile
        if (tile.childCount <= 0)
            return;

        // return if already active
        Chip chipScript = tile.GetChild(0).GetComponent<Chip>();
        if (chipScript.IsActive)
            return;

        // deactivate all
        Transform grid = tile.parent;
        foreach (Transform tileItem in grid)
        {
            if (tileItem.childCount > 0)
            {
                Chip chipInTileScript = tileItem.GetChild(0).GetComponent<Chip>();
                if (chipInTileScript.IsActive && chipInTileScript.Type == chipScript.Type)
                    chipInTileScript.IsActive = false;
            }
        }

        // activate chosen
        chipScript.IsActive = true;
    }

    public void DeactivateActiveChipAndActivateNextOne(Chip.ChipType chipType, bool isStayingActiveIfNextOneNotExist, bool isNextForward = true)
    {
        // get relevant grid
        Transform grid = null;
        if (chipType == Chip.ChipType.TURRET)
            grid = TurretsGrid.transform;
        if (chipType == Chip.ChipType.SKILL)
            grid = SkillsGrid.transform;

        if (grid == null)
            return;

        // create list of all sockets with chips in chosen type
        int activeChipIndex = 0;
        List<Chip> chips = new List<Chip>();
        foreach (Transform tile in grid)
        {
            if (tile.childCount > 0)
            {
                Chip chipScript = tile.GetChild(0).GetComponent<Chip>();
                if (chipScript.Type == chipType)
                {
                    chips.Add(chipScript);
                    if (chipScript.IsActive)
                        activeChipIndex = chips.Count - 1;
                }
            }
        }

        if (chips.Count == 0)
            return;

        // get the next socket
        Chip activeChip = chips[activeChipIndex];
        Chip nextChip = null;
        if (isNextForward)
        {
            if (activeChipIndex == chips.Count - 1)
                nextChip = chips[0];
            else
                nextChip = chips[activeChipIndex + 1];
        }
        else
        {
            if (activeChipIndex == 0)
                nextChip = chips[chips.Count - 1];
            else
                nextChip = chips[activeChipIndex - 1];
        }

        // deactivate current active chip
        activeChip.IsActive = false;

        // active next chip
        if (activeChip == nextChip)
        {
            if (isStayingActiveIfNextOneNotExist)
                nextChip.IsActive = true;
        }
        else
        {
            nextChip.IsActive = true;
        }
    }

    public Chip GetActiveSkill()
    {
        foreach (Transform tile in SkillsGrid.transform)
        {
            if (tile.childCount > 0)
            {
                Chip chipScript = tile.GetChild(0).GetComponent<Chip>();
                if (chipScript.Type == Chip.ChipType.SKILL && chipScript.IsActive)
                    return chipScript;
            }
        }

        return null;
    }
}
