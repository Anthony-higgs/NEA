using UnityEngine;
using UnityEngine.SceneManagement;
public class ArmoryUI : MonoBehaviour
{
    public string gameSceneName = "MainMenu";
    public int fuelTankCost = 200;
    public void ToMenu()
    {
        SceneManager.LoadScene(gameSceneName);
    }
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