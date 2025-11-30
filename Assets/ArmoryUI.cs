using UnityEngine;

public class ArmoryUI : MonoBehaviour
{
    public int fuelTankCost = 200;

    public void BuyFuelTank()
    {
        bool success = UpgradeManager.Instance.UnlockFuelTank(fuelTankCost);

        if (success)
        {
            Debug.Log("Fuel Tank Upgrade Bought!");
        }
        else
        {
            Debug.Log("Purchase failed — not enough gems!");
        }
    }
}