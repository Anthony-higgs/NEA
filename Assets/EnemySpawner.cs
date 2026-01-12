using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawning")]
    public GameObject enemyPrefab;        // prefab only (do not destroy)
    public Transform[] spawnPoints;       // spawn locations
    public Transform player;              // player reference

    [Header("Timing & Range")]
    public float spawnInterval = 3f;
    public float activationRange = 15f;
    public int maxEnemiesAlive = 5;       // prevents infinite spawning

    private float nextSpawnTime;

    void Update()
    {
        // Safety checks
        if (player == null || enemyPrefab == null || spawnPoints.Length == 0)
            return;

        // Check distance to player
        float dist = Vector2.Distance(transform.position, player.position);
        if (dist > activationRange)
            return;

        // Count current enemies in the scene
        int enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemiesAlive >= maxEnemiesAlive)
            return;

        // Spawn timer
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        // Pick random spawn point
        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Spawn enemy
        GameObject enemyGO = Instantiate(enemyPrefab, spawn.position, Quaternion.identity);

        // Make sure enemy is tagged
        enemyGO.tag = "Enemy";

        // Initialise movement
        EnemyMovement2D movement = enemyGO.GetComponent<EnemyMovement2D>();
        if (movement == null)
        {
            Debug.LogError("Enemy prefab missing EnemyMovement2D!");
            return;
        }

        // this should fix the issue with enemy prefabs 
        movement.Init(player);

        // Register with pathfinding
        PathfindingManager pf = FindObjectOfType<PathfindingManager>();
        if (pf != null)
        {
            pf.RegisterEnemy(movement);
        }
        else
        {
            Debug.LogError("PathfindingManager not found in scene!");
        }

        Debug.Log("Enemy spawned at " + spawn.position);
    }
}