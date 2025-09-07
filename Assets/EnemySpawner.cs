using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab;      // prefab to spawn
    public Transform[] spawnPoints;     // locations to spawn at
    public Transform player;            // player reference
    public float spawnInterval = 3f;    // time between spawns
    public float activationRange = 15f; // only spawn if player is in range

    private float nextSpawnTime;        // timer for next spawn

    void Update()
    {
        // Make sure references exist
        if (player == null || spawnPoints.Length == 0 || enemyPrefab == null) return;

        float dist = Vector2.Distance(transform.position, player.position);
        

        // Only spawn if player is close enough
        if (dist <= activationRange)
        {
            if (Time.time >= nextSpawnTime)
            {
                SpawnEnemy();
                nextSpawnTime = Time.time + spawnInterval;
            }
        }
    }

    void SpawnEnemy()
    {
        // pick a random spawn point
        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemyGO = Instantiate(enemyPrefab, spawn.position, Quaternion.identity);

        // assign player as target and register with PathfindingManager
        EnemyMovement2D movement = enemyGO.GetComponent<EnemyMovement2D>();
        if (movement != null)
        {
            movement.target = player;
            FindObjectOfType<PathfindingManager>()?.RegisterEnemy(movement);
            Debug.Log("Spawned enemy at " + spawn.position);
        }
        else
        {
            Debug.LogError("Spawned enemy has no EnemyMovement2D");
        }
    }
}