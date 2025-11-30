using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Grid2D grid;
    [Header("Health & Collision")]
    public PlayerHealth playerHealth; // reference PlayerHealth
    public int obstacleDamage = 5;    // how much dmg the obstacle does
    //public float hitCooldown = 1f;    // seconds between hits so player doesn't take dmg every frame

    [Header("Car Movement")]
    // normal movement speed of the car
    public float slowAmount = 5f;    // how much the car slows down when hitting an obstacle
    public float slowDuration = 1f;  // how long the slowdown lasts


    

    void Awake()
    {
        // checks PlayerHealth exists
        if (playerHealth == null)
            playerHealth = GetComponent<PlayerHealth>();



        grid = FindObjectOfType<Grid2D>();
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // deal damage
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(obstacleDamage);
                Debug.Log("Player hit an obstacle! Damage: " + obstacleDamage);
            }

            // slowdown
            PlayerMovement2D movement = GetComponent<PlayerMovement2D>();
            if (movement != null)
            {
                movement.SlowDown(slowAmount, slowDuration);
                Debug.Log("Car slowed using PlayerMovement2D for " + slowDuration + " seconds.");
            }

            // destroy obstacle
            Destroy(collision.gameObject);

            // rebuild grid so enemies know this tile is now clear
            if (grid != null)
            {
                grid.RebuildGrid();
                Debug.Log("rebuildgrid called");
            }
        }
    }
}