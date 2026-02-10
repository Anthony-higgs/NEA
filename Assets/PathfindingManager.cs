using UnityEngine;
using System.Collections.Generic;

public class PathfindingManager : MonoBehaviour
{
    public Grid2D grid;             // reference to the grid of nodes
    public Transform player;         // the player to follow
    public float followRange = 12f;  // how close player must be for enemies to follow
    public float repathInterval = 0.2f; // how often to recalc paths

    private float nextRepathTime;             // timer for next path calculation
    private List<EnemyMovement2D> enemies = new List<EnemyMovement2D>(); // all registered enemies

    // Call this when a new enemy spawns to register them
    public void RegisterEnemy(EnemyMovement2D enemy)
    {
        if (!enemies.Contains(enemy))
            enemies.Add(enemy);
    }

    void Update()
    {
        // Make sure we have a player and a grid
        if (player == null || grid == null) return;
        if (Time.time < nextRepathTime) return; // not time yet

        nextRepathTime = Time.time + repathInterval; // schedule next update

        // Update all enemies
        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;

            float dist = Vector2.Distance(enemy.transform.position, player.position);

            // If player is far or enemy attached, clear path
            if (dist > followRange || enemy.isAttached)
            {
                enemy.SetPath(null);
                continue;
            }

            // Compute a new path and give it to enemy
            List<Node> path = FindPath(enemy.transform.position, player.position);
            enemy.SetPath(path);
        }
    }

    //A* pathfinding
    List<Node> FindPath(Vector2 startPos, Vector2 targetPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node rawTargetNode = grid.NodeFromWorldPoint(targetPos);
        Node targetNode = grid.FindNearestWalkable(rawTargetNode) ?? rawTargetNode;

        if (!startNode.walkable)
        {
            Debug.LogWarning("Start node blocked at " + startNode.worldPosition);
            return null;
        }

        List<Node> openSet = new();
        HashSet<Node> closedSet = new();

        startNode.ResetPathData();
        startNode.gCost = 0;
        startNode.hCost = Heuristic(startNode, targetNode);
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node current = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < current.fCost ||
                    (openSet[i].fCost == current.fCost && openSet[i].hCost < current.hCost))
                    current = openSet[i];
            }

            openSet.Remove(current);
            closedSet.Add(current);

            if (current == targetNode)
                return RetracePath(startNode, targetNode);

            foreach (Node neighbour in grid.GetNeighbours(current))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour)) continue;

                if (neighbour.gCost == int.MaxValue) neighbour.ResetPathData();

                int tentativeG = current.gCost + Heuristic(current, neighbour);
                if (tentativeG < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = tentativeG;
                    neighbour.hCost = Heuristic(neighbour, targetNode);
                    neighbour.parent = current;

                    if (!openSet.Contains(neighbour)) openSet.Add(neighbour);
                }
            }
        }

        Debug.LogWarning("PathfindingManager: No path found from " + startPos + " to " + targetPos);
        return null;
    }

    // Backtrack from end to start to make path
    List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new();
        Node current = endNode;

        while (current != startNode)
        {
            path.Add(current);
            current = current.parent;
        }

        path.Reverse(); // path is from start ? end
        return path;
    }

    // Estimate distance between nodes
    int Heuristic(Node a, Node b)
    {
        int dx = Mathf.Abs(a.gridX - b.gridX);
        int dy = Mathf.Abs(a.gridY - b.gridY);
        return 14 * Mathf.Min(dx, dy) + 10 * Mathf.Abs(dx - dy);
    }
}