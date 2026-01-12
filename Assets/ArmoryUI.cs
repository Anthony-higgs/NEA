using UnityEngine;
using UnityEngine.SceneManagement;
public class ArmoryUI : MonoBehaviour
{
    public string gameSceneName = "MainMenu";
    public int fuelTankCost = 200;
    public int armorCost = 200;
    public void ToMenu()
    {
        SceneManager.LoadScene(gameSceneName);
    }
    public void BuyFuelTank()
    {
        bool success = UpgradeManager.Instance.UnlockFuelTank(fuelTankCost);

        if (success)
        {
            Debug.Log("Fuel Tank Upgrade Bought");
        }
        else
        {
            Debug.Log("Purchase failed");
        }
    }
    public void BuyArmor()
    {
        bool success = UpgradeManager.Instance.UnlockArmor(armorCost);
        if (success)
        {
            Debug.Log("Armor upgrade Bought");
        }
        else
        {
            Debug.Log("Purchase failed");
        }
    }
    public void BuyTurret()
    {
        bool success = UpgradeManager.Instance.UnlockTurret(armorCost);
        if (success)
        {
            Debug.Log("Turret upgrade Bought");
        }
        else
        {
            Debug.Log("Purchase failed");
        }
    }
}