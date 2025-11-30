using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    // Upgrades
    public bool fuelTankUnlocked = false;

    void Awake()
    {
        // Make this object persistent between scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadUpgrades();
    }

    public bool UnlockFuelTank(int cost)
    {
        // Already unlocked
        if (fuelTankUnlocked)
            return true;

        // Check gems
        if (!GemManager.Instance.SpendGems(cost))
        {
            Debug.Log("Not enough gems to buy Fuel Tank upgrade!");
            return false;
        }

        // Unlock upgrade
        fuelTankUnlocked = true;
        SaveUpgrades();

        Debug.Log("Fuel Tank purchased!");
        return true;
    }

    public void SaveUpgrades()
    {
        PlayerPrefs.SetInt("FuelTankUnlocked", fuelTankUnlocked ? 1 : 0);
    }

    public void LoadUpgrades()
    {
        fuelTankUnlocked = PlayerPrefs.GetInt("FuelTankUnlocked", 0) == 1;
    }
}