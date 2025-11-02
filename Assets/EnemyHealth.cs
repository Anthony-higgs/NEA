using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;

    // Event for when enemy dies
    private Action onDeath;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    // Call this to damage the enemy
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Enemy death logic
    public void Die()
    {
        Debug.Log("enemy died");
        // notify spawner
        onDeath?.Invoke();

        // give player 10 gems
        GemManager.Instance?.AddGems(10);
        

        // Destroy this enemy
        Destroy(gameObject);
    }

    // Allow spawner to subscribe
    public void OnDeath(Action callback)
    {
        onDeath += callback;
    }
}