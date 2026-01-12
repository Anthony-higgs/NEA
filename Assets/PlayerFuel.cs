using UnityEngine;

public class PlayerFuel : MonoBehaviour
{
    [Header("Fuel Settings")]
    public float baseMaxFuel = 100f;         // Max fuel capacity
    public float currentFuel;            // Current fuel level
    public float fuelUpgradeBonus = 50f;
    public float fuelUsageRate = 5f;     // How fast fuel drains per second when moving

    [Header("References")]
    public PlayerMovement2D playerMovement; // The script that handles movement
    public PlayerCollision playerCollision;
    void Start()
    {
        currentFuel = baseMaxFuel;

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

            // Prevent negative fuel
            if (currentFuel <= 0)
            {
                currentFuel = 0;
                StopMovement();
            }
        }
    }
    public void ForceOutOfFuel()
    {
        currentFuel = 0;
        StopMovement();
    }

    public void ApplyFuelUpgrade(bool hasFuelTank)
    {
        float maxFuel = hasFuelTank ? baseMaxFuel + fuelUpgradeBonus : baseMaxFuel;
        currentFuel = maxFuel; // refill on apply
    }


    // Called when fuel is gone
    void StopMovement()
    {
        Debug.Log("Out of fuel! Car cannot move.");
        if (playerMovement != null)
        {
            playerMovement.enabled = false; // stops car control
            playerCollision.enabled = false;
        }
    }
}