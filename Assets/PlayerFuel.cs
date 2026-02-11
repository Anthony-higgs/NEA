using UnityEngine;

public class PlayerFuel : MonoBehaviour
{
    [Header("Fuel Settings")]
    public float baseMaxFuel = 100f;         // Max fuel capacity
    public float currentFuel;            // Current fuel level
    public float fuelUpgradeBonus = 50f;
    public float fuelUsageRate = 5f;     // How fast fuel drains per second when moving

    [Header("References")]
    public PlayerMovement2D playerMovement; 
    public PlayerCollision playerCollision;

    
    void Start()
    {
        if (UpgradeManager.Instance == null)
        {
            Debug.LogError("UpgradeManager missing!");
            return;
        }
        bool hasFuelTank = UpgradeManager.Instance.fuelTankUnlocked;
        if (!hasFuelTank)
        {
            currentFuel = baseMaxFuel;
        }
        else
        {
            ApplyFuelUpgrade(hasFuelTank);
        }

            

        // assign movement and collision scripts
        if (playerMovement == null)
            playerMovement = GetComponent<PlayerMovement2D>();

        if (playerCollision == null)
            playerCollision = GetComponent<PlayerCollision>();
    }

    void Update()
    {
        // Drain fuel only if the car is moving
        if (playerMovement != null && playerMovement.IsMoving() && currentFuel > 0)
        {
            Debug.Log("Fuel used");
            currentFuel -= fuelUsageRate * Time.deltaTime;

            // stops the car when out of fuel
            if (currentFuel <= 0)
            {
                currentFuel = 0;
                playerMovement.currentSpeed = 0f;
                StopMovement();
            }
        }
    }
   

    public void ApplyFuelUpgrade(bool hasFuelTank)
    {
        float maxFuel = hasFuelTank ? baseMaxFuel + fuelUpgradeBonus : baseMaxFuel;
        currentFuel = maxFuel; // refill on apply
        Debug.Log(currentFuel);
    }


    // Called when fuel is gone
    void StopMovement()
    {
        Debug.Log("Out of fuel! Car cannot move.");
        if (playerMovement != null)
        {
            playerMovement.enabled = false; // stops car control
        }
    }
}