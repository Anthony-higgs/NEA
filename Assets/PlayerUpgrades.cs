using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [Header("Upgrade Objects")]
    public GameObject fuelTank;
    public GameObject Armor;
    public GameObject Turret;
    void Start()
    {
        ApplyUpgrades();
    }

    void ApplyUpgrades()
    {
        // Load if fuel tank should be enabled
        bool fuelTankUnlocked = PlayerPrefs.GetInt("FuelTankUnlocked", 0) == 1;
        bool ArmorUnlocked = PlayerPrefs.GetInt("ArmorUnlocked", 0) == 1;
        bool TurretUnlocked = PlayerPrefs.GetInt("TurretUnlocked",0) == 1;

        // Activate or deactivate the object
        if (fuelTank != null)
        {
            fuelTank.SetActive(fuelTankUnlocked);
        }
        if (Armor != null)
        {
            Armor.SetActive(ArmorUnlocked);
        }
        if (Turret != null)
        {
            Turret.SetActive(TurretUnlocked);
        }
    }
}