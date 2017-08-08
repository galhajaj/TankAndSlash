using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Garage_ButtonCommands : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void BuyRegularPack()
    {
        ChipManager.Instance.BuyPack(ChipManager.PackType.REGULAR);
    }

    public void BuyPremiumPack()
    {
        ChipManager.Instance.BuyPack(ChipManager.PackType.PREMIUM);
    }

    public void BuySuperPack()
    {
        ChipManager.Instance.BuyPack(ChipManager.PackType.SUPER);
    }
}
