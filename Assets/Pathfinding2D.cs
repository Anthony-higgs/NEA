/*
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding2D : MonoBehaviour
{
    [Header("Who chases who")]
    public Transform seeker; // enemy Transform
    public Transform target; // player Transform

    [Header("When to chase")]
    public float followRange = 12f;      // start following if within this distance
    public float repathInterval = 0.2f;  // recompute path this often (seconds)

    Grid2D grid;
    float nextRepathTime;

    void Awake()
    {
        grid = GetComponent<Grid2D>();
    }

    void Update()
    {
        if (seeker == null || target == null || grid == null) return;

        float dist = Vector2.Distance(seeker.position, target.position);
        if (dist > followRange) return; // out of range: do nothing

        if (Time.time >= nextRepathTime)
        {
            nextRepathTime = Time.time + repathInterval;
            FindPath(seeker.position, target.position);
        }
    }

    void FindPath(Vector2 startPos, Vector2 targetPos)
    {
        // map world positions to nodes
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node rawTargetNode = grid.NodeFromWorldPoint(targetPos);

        // If target tile is blocked (e.g., player overlapping), pick nearest walkable
        Node targetNode = grid.FindNearestWalkable(rawTargetNode) ?? rawTargetNode;

        // If start itself is blocked, we can't begin
        if (!startNode.walkable)
        {
            Debug.Log("Start node blocked, abort.");
            return;
        }

        // Reset any stale path data on the fly (only for nodes we touch)
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        startNode.ResetPathData();
        startNode.gCost = 0;
        startNode.hCost = Heuristic(startNode, targetNode);
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            // find node with lowest fCost
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
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(current))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour)) continue;

                // ensure clean costs for neighbours we touch
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

        // no path
        Debug.Log("No path found.");
        GetComponent<EnemyMovement2D>()?.SetPath(null);
        grid.SetDebugPath(null);
    }
    public EnemyMovement2D enemyMovement;

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new();
        Node currentNode = endNode;

        // build backwards
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        // send the path to the enemy movement script + draw gizmos
        if (enemyMovement != null)
        {
            enemyMovement.SetPath(path);
        }
        else
        {
            Debug.LogWarning("EnemyMovement2D reference not assigned!");
        }

        Debug.Log("Path found! Steps: " + path.Count);
    }

    int Heuristic(Node a, Node b)
    {
        // diagonal-friendly heuristic (14 ~ sqrt(2)*10)
        int dx = Mathf.Abs(a.gridX - b.gridX);
        int dy = Mathf.Abs(a.gridY - b.gridY);
        return 14 * Mathf.Min(dx, dy) + 10 * Mathf.Abs(dx - dy);
    }
}
*/