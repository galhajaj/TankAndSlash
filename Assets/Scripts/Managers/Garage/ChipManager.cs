﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipManager : MonoBehaviour
{
    public static ChipManager Instance;

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

    void Awake()
    {
        Instance = this; // init singletone
    }

    void Start ()
    {
        //Application.isLoadingLevel
        fillChipsListFromDataManager();
    }
	
	void Update ()
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
        if (DataManager.Instance.TankParams.Credits < cost)
            return;

        // check free sockets
        List<Transform> freeSockets = SocketManager.Instance.GetFreeSocketsForChipsPack();
        if (freeSockets == null)
            return;

        // pay
        DataManager.Instance.TankParams.Credits -= cost;

        // add chips
        for (int i = 0; i < freeSockets.Count; ++i)
            createRandomChip(freeSockets[i]);
    }

    public void MoveChip(GameObject chip, Transform socket)
    {
        chip.transform.position = socket.position;
        chip.transform.parent = socket;

        Chip chipScript = chip.GetComponent<Chip>();
        chipScript.SocketName = socket.name;
        chipScript.GridName = socket.parent.name;
    }

    private void createRandomChip(Transform socket)
    {
        GameObject newChip = Instantiate(ChipObject);
        MoveChip(newChip, socket);

        Chip chipScript = newChip.GetComponent<Chip>();
        chipScript.init(); // TODO: add chances by the packType

        chipScript.ChipName = "TempName";
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
        if (grid == null)
            Debug.LogWarning("Grid not exist yet!");
        Transform socket = grid.transform.Find(chipData.SocketName);
        if (socket == null)
            Debug.LogWarning("Socket not exist yet!");

        MoveChip(newChip, socket);

        Chip chipScript = newChip.GetComponent<Chip>();
        chipScript.ChipID = chipData.ChipID;
        chipScript.ChipName = chipData.ChipName;
        chipScript.Type = chipData.ChipType;

        Chips.Add(newChip);
    }

    private void fillChipsListFromDataManager()
    {
        foreach (ChipData chipData in DataManager.Instance.TankParams.ChipsData)
        {
            createChipByData(chipData);
        }
    }

    public void UpdateChipsDataInDataManager()
    {
        // clear current data
        DataManager.Instance.TankParams.ChipsData.Clear();

        // renew
        foreach (GameObject chip in Chips)
        {
            ChipData chipData = chip.GetComponent<Chip>().GetChipData();
            DataManager.Instance.TankParams.ChipsData.Add(chipData);
        }

        // save
        DataManager.Instance.SaveDataToFile();
    }
}
