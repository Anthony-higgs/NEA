using UnityEngine;
using System.Collections;
using static EnemyHealth;
public class EnemyCollision2D : MonoBehaviour
{
    public int damageOnHit = 10;       // first hit damage
    public int damageWhileAttached = 5; // second hit damage
    public float attachDuration = 2f;   // seconds to stick to player
    private EnemyHealth health;
    private bool isAttached = false;
    public FuelTank fuelTank;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isAttached)
        {
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            if (player != null)
            {
                // Phase 1: initial hit
                player.TakeDamage(damageOnHit);

                // Phase 2: attach to player
                StartCoroutine(AttachToPlayer(collision.gameObject, player));
            }
            if (collision.gameObject.CompareTag("Enemy"))
            {
                fuelTank.TakeDamage(10);
            }
            else
            {
                // This will print the object name if it doesn’t have PlayerHealth
                Debug.LogError("No PlayerHealth found on " + collision.gameObject.name);
            } 
        }
    }

    IEnumerator AttachToPlayer(GameObject playerObject, PlayerHealth player)
    {
        isAttached = true;

        // Make enemy a child of player so it moves with the car
        transform.SetParent(playerObject.transform);

        // Stop enemy pathfinding while attached
        EnemyMovement2D enemyMovement = GetComponent<EnemyMovement2D>();
        if (enemyMovement != null)
            enemyMovement.enabled = false;

        // Wait for the attach duration
        yield return new WaitForSeconds(attachDuration);

        // Phase 3: second damage
        if (player != null)
            player.TakeDamage(damageWhileAttached);

        // Detach enemy
        transform.SetParent(null);

        // Enemy dies immediately after detaching
        Debug.Log($"{gameObject.name} died after detaching from player!");
        health.Die();
    }

}