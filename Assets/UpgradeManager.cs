using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    // Upgrades
    public bool fuelTankUnlocked = false;
    public bool ArmorUnlocked = false;
    public bool TurretUnlocked = false;
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
        {
            Debug.Log("Already purchased fuel tank upgrade");
            return false;
            
        }

        // Check gems
        if (!GemManager.Instance.SpendGems(cost))
        {
            Debug.Log("Not enough gems to buy Fuel Tank upgrade");
            return false;
        }

        // Unlock upgrade
        fuelTankUnlocked = true;
        SaveUpgrades();

        Debug.Log("Fuel Tak purchased");
        return true;
    }
    public bool UnlockArmor(int cost)
    {
        // Already unlocked
        if (ArmorUnlocked)
        {
            Debug.Log("Already purchased Armor upgrade");
            return false;
        }
       

        // Check gems
        if (!GemManager.Instance.SpendGems(cost))
        {
            Debug.Log("Not enough gems to buy Armor upgrade");
            return false;
        }

        // Unlock upgrade
        ArmorUnlocked = true;
        SaveArmorUpgrades();

        Debug.Log("Armor purchased");
        return true;
    }

    public bool UnlockTurret(int cost)
    {
        // Already unlocked
        if (TurretUnlocked)
        {
            Debug.Log("Already purchased turet upgrade");
            return false;
        }


        // Check gems
        if (!GemManager.Instance.SpendGems(cost))
        {
            Debug.Log("Not enough gems to buy Turret upgrade");
            return false;
        }

        // Unlock upgrade
        TurretUnlocked = true;
        SaveTurretUpgrades();

        Debug.Log("Turret purchased");
        return true;
    }
    //different ones for each for when I add different levels for each one if i decide to
    public void SaveUpgrades()
    {
        PlayerPrefs.SetInt("FuelTankUnlocked", fuelTankUnlocked ? 1 : 0);
        
    }
    public void SaveArmorUpgrades()
    {
        PlayerPrefs.SetInt("ArmorUnlocked", ArmorUnlocked ? 1 : 0);

    }
    public void SaveTurretUpgrades()
    {
        PlayerPrefs.SetInt("TurretUnlocked", TurretUnlocked ? 1 : 0);
    }

    public void LoadUpgrades()
    {
        fuelTankUnlocked = PlayerPrefs.GetInt("FuelTankUnlocked", 0) == 1;
        ArmorUnlocked = PlayerPrefs.GetInt("ArmorUnlocked", 0) == 1;
        TurretUnlocked = PlayerPrefs.GetInt("TurretUnlocked", 0) == 1;

    }
}