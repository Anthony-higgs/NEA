using UnityEngine;
using System.Collections.Generic;
using static EnemyHealth;
public class EnemyMovement2D : MonoBehaviour
{
    private EnemyHealth health;
    void Awake()
    {
        health = GetComponent<EnemyHealth>();
        
    }
    [Header("Movement Settings")]
    public float moveSpeed = 3f; // how fast the enemy moves

    [Header("Target")]
    public Transform target; // the player this enemy should follow
    private bool initialised = false;

    public void Init(Transform playerTarget)
    {
        target = playerTarget;
        initialised = true;
        Debug.Log("Target initialised");
    }

    
    public bool isAttached = false; //is the enemy holding onto player

    private List<Node> path; // the path this enemy will follow
    private int currentWaypoint = 0; // which node along the path we're heading to

    // Called when PathfindingManager computes a new path
    public void SetPath(List<Node> newPath)
    {
        //checks if there is a path to the palyer
        if (newPath == null || newPath.Count == 0)
        {
            Debug.Log(name + ": Received empty path");
            path = null;
            currentWaypoint = 0;
            return;
        }

        path = newPath;
        currentWaypoint = 0;
        Debug.Log(name + ": New path received, Steps: " + path.Count);
    }

    void Update()
    {
        if (!initialised)
        {
            Debug.Log("Target isnt initialized");
            return;
        }
        if (target == null)
        {
            Debug.Log("Target null");
            return;
        }
        // If attached, no path, or no target, don't move
        if (isAttached || path == null || path.Count == 0) return;

        // If reached the end of the path, stop moving
        if (currentWaypoint >= path.Count)
        {
            Debug.Log(name + ": Reached end of path");
            return;
        }

        // Move toward the next node
        Vector2 targetPos = path[currentWaypoint].worldPosition;
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );

        // If we reach this node, move to the next one
        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
        {
            currentWaypoint++;
            if (currentWaypoint < path.Count)
            {
                Debug.Log(name + ": Moving to next node: " + path[currentWaypoint].worldPosition);
            }
        }
    }

    // Triggered when colliding with something
    void OnCollisionEnter2D(Collision2D collision)
    {
        // If we hit the player and aren't already attached, attach
        if (collision.gameObject.CompareTag("Player") && !isAttached)
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
                StartCoroutine(AttachToPlayer(collision.transform, playerHealth));
        }
    }

    // Coroutine to attach to the player, deal damage, then detach and die
    System.Collections.IEnumerator AttachToPlayer(Transform player, PlayerHealth playerHealth)
    {
        isAttached = true;             // stop moving on path
        transform.SetParent(player);   // attach to player

        Debug.Log(name + ": Attached to player, dealing first damage");
        playerHealth.TakeDamage(10);   // initial damage

        yield return new WaitForSeconds(2f); // wait while attached

        Debug.Log(name + ": Dealing second damage and detaching");
        playerHealth.TakeDamage(5);    // second damage

        transform.SetParent(null);     // detach from player
        isAttached = false;

        Debug.Log(name + ": Enemy dies after attaching");
        health.Die();
    }
}

