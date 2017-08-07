using System.Collections;
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

        // save
        DataManager.Instance.SaveDataToFile();
    }

    private void createRandomChip(Transform socket)
    {
        GameObject newChip = Instantiate(ChipObject);
        MoveChip(newChip, socket);

        Chip chipScript = newChip.GetComponent<Chip>();
        chipScript.init(); // TODO: add chances by the packType

        chipScript.ChipName = "TempName";
        chipScript.ChipID = DataManager.Instance.GetNextChipID();

        DataManager.Instance.TankParams.Chips.Add(chipScript.GetChipData());

        // save
        DataManager.Instance.SaveDataToFile();
    }
}
