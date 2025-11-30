using UnityEngine;

public class FuelTank : MonoBehaviour
{
    public int maxHealth = 25;
    public int currentHealth;

    public PlayerFuel playerFuel; // reference to player fuel

    void Start()
    {
        currentHealth = maxHealth;

        if (playerFuel == null)
            Debug.LogError("FuelTank: Missing PlayerFuel reference");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            DestroyTank();
        }
    }

    void DestroyTank()
    {
        Debug.Log("Fuel Tank destroyed");

        if (playerFuel != null)
        {
            playerFuel.ForceOutOfFuel();  // instantly drain fuel
        }

        Destroy(gameObject); // destroy the tank model
    }
}