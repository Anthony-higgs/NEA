using UnityEngine;

public class PlayerUpgradeLoader : MonoBehaviour
{
    public GameObject fuelTankObject;     // assign in inspector

    void Start()
    {
        if (UpgradeManager.Instance.fuelTankUnlocked)
        {
            fuelTankObject.SetActive(true);
        }
        else
        {
            fuelTankObject.SetActive(false);
        }
    }
}