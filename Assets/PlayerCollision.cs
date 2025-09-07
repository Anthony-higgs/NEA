using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Grid2D grid;
    [Header("Health & Collision")]
    public PlayerHealth playerHealth; // reference PlayerHealth
    public int obstacleDamage = 5;    // how much dmg the obstacle does
    //public float hitCooldown = 1f;    // seconds between hits so player doesn't take dmg every frame

    [Header("Car Movement")]
    public float speed = 5f;         // normal movement speed of the car
    public float slowAmount = 2f;    // how much the car slows down when hitting an obstacle
    public float slowDuration = 1f;  // how long the slowdown lasts

   
    private float currentSpeed;                  // current speed, reduced when slowed
    private float slowEndTime = 0f;              // when the slowdown ends

    void Awake()
    {
        // checks PlayerHealth exists
        if (playerHealth == null)
            playerHealth = GetComponent<PlayerHealth>();

        // initialize current speed
        currentSpeed = speed;

        grid = FindObjectOfType<Grid2D>();
    }

    void Update()
    {
        // gradually restores speed if slowdown has ended
        if (Time.time >= slowEndTime)
            currentSpeed = speed;

        // simple forward/backward movement example
        float moveInput = Input.GetAxis("Vertical"); // -1 to 1
        transform.Translate(Vector2.up * moveInput * currentSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // checks that it collides with an obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
                if (playerHealth != null)
                {
                    // deals damage
                    playerHealth.TakeDamage(obstacleDamage);
                    Debug.Log("Player hit an obstacle! Damage: " + obstacleDamage);
                }

                // slow down the car
                currentSpeed = Mathf.Max(0, speed - slowAmount); // avoid negative speed
                slowEndTime = Time.time + slowDuration;
                Debug.Log("Player slowed down for " + slowDuration + " seconds.");

                // destroy obstacle for now
                // replace with breaking barricade animation later
                Destroy(collision.gameObject);
            //after the obstacle is destroyed the grid is rebuilt so it is now registered as walkable
            
            if (grid != null)
            {
                grid.RebuildGrid();
                Debug.Log("rebuildgrid called");
            }
         
        }
    }
}