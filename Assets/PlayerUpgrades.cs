using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [Header("Upgrade Objects")]
    public GameObject fuelTank;
    void Start()
    {
        ApplyUpgrades();
    }

    void ApplyUpgrades()
    {
        // Load if fuel tank should be enabled
        bool fuelTankUnlocked = PlayerPrefs.GetInt("FuelTankUnlocked", 0) == 1;

        // Activate or deactivate the object
        if (fuelTank != null)
            fuelTank.SetActive(fuelTankUnlocked);
    }
}