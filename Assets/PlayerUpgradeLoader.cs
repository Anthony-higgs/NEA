using UnityEngine;

public class PlayerUpgradeLoader : MonoBehaviour
{
    public GameObject fuelTankObject;
    public GameObject ArmorObject;
    public GameObject TurretObject;
    public PlayerFuel playerFuel;
    public PlayerHealth playerHealth;

    void Start()
    {
        if (UpgradeManager.Instance == null)
        {
            Debug.LogError("UpgradeManager missing!");
            return;
        }

        bool hasFuelTank = UpgradeManager.Instance.fuelTankUnlocked;
        bool hasArmor = UpgradeManager.Instance.ArmorUnlocked;
        bool hasTurret = UpgradeManager.Instance.TurretUnlocked;

        // Enable / disable visual tank
        fuelTankObject.SetActive(hasFuelTank);
        ArmorObject.SetActive(hasArmor);
        TurretObject.SetActive(hasTurret);
        // Apply fuel upgrade
        if (playerFuel != null)
            playerFuel.ApplyFuelUpgrade(hasFuelTank);
        if (playerHealth != null)
            playerHealth.ApplyArmorUpgrade(hasArmor);
    }
}