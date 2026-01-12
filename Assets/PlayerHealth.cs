using UnityEngine;
using UnityEngine.SceneManagement; // for when i want to reload or change scenes

public class PlayerHealth : MonoBehaviour
{
    public int baseMaxHealth = 100;
    public int currentHealth;
    public int armorUpgradeBonus = 50;
    void Awake()
    {
        
            currentHealth = baseMaxHealth;
        

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage, current HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void ApplyArmorUpgrade(bool hasArmor)
    {
        int maxHealth = hasArmor ? baseMaxHealth + armorUpgradeBonus : baseMaxHealth;
        currentHealth = maxHealth; // refill on apply
    }
    void Die()
    {
        Debug.Log("Player died!");
        GameManager.Instance.GameOver(); // tell GameManager to end the game
    }
}