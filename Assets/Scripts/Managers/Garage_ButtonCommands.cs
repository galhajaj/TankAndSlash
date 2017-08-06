using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Garage_ButtonCommands : MonoBehaviour
{
    private enum PackType
    {
        REGULAR,
        PREMIUM,
        SUPER
    }

    private const int NUMBER_OF_CHIPS_IN_PACK = 5;

    public GameObject ChipObject;
    public int RegularPackCost;
    public int PremiumPackCost;
    public int SuperPackCost;
    public Button RegularPackButton;
    public Button PremiumPackButton;
    public Button SuperPackButton;

    void Start()
    {
        
    }

    void Update()
    {
        updatePackButtonsState();
    }

    public void BuyRegularPack()
    {
        addPack(PackType.REGULAR);
    }

    public void BuyPremiumPack()
    {
        addPack(PackType.PREMIUM);
    }

    public void BuySuperPack()
    {
        addPack(PackType.SUPER);
    }

    private List<Transform> getFreeSocketsForChipsPack()
    {
        GameObject grid = GameObject.Find("Grid_Inventory");
        List<Transform> freeSockets = new List<Transform>();

        foreach (Transform tile in grid.transform)
        {
            if (tile.childCount == 0)
            {
                freeSockets.Add(tile);
                if (freeSockets.Count >= NUMBER_OF_CHIPS_IN_PACK)
                    return freeSockets;
            }
        }

        return null;
    }

    private bool hasEnoughCredits(PackType packType)
    {
        int cost = getCost(packType);
        if (DataManager.Instance.TankParams.Credits < cost)
            return false;
        return true;
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

    private void updatePackButtonsState()
    {
        bool isInventoryFull = (getFreeSocketsForChipsPack() == null);

        RegularPackButton.enabled = (!isInventoryFull && hasEnoughCredits(PackType.REGULAR));
        PremiumPackButton.enabled = (!isInventoryFull && hasEnoughCredits(PackType.PREMIUM));
        SuperPackButton.enabled = (!isInventoryFull && hasEnoughCredits(PackType.SUPER));
    }

    private void pay(PackType packType)
    {
        int cost = getCost(packType);
        DataManager.Instance.TankParams.Credits -= cost;
        DataManager.Instance.SaveDataToFile();
        updatePackButtonsState(); // just in case...
    }

    private void addPack(PackType packType)
    {
        List<Transform> freeSockets = getFreeSocketsForChipsPack();

        bool isInventoryFull = (freeSockets == null);
        if (isInventoryFull || !hasEnoughCredits(packType))
            return;

        pay(packType);

        for (int i = 0; i < freeSockets.Count; ++i)
        {
            GameObject newChip = Instantiate(ChipObject, freeSockets[i].position, Quaternion.identity);
            newChip.transform.parent = freeSockets[i];
            newChip.GetComponent<Chip>().initRandomType(); // TODO: add chances by the packType
        }
    }
}
