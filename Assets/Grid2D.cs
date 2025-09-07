using System.Collections.Generic;
using UnityEngine;

public class Grid2D : MonoBehaviour
{
    [Header("Grid Size")]
    public Vector2 gridWorldSize = new Vector2(30, 30); // width/height in world units
    public float nodeRadius = 0.5f;                     // half tile size (tile size = 2*radius)

    [Header("Blocking Layers (NO 'Player' layer here!)")]
    public LayerMask unwalkableMask; // check Wall + Obstacle only

    [Header("Diagonal Movement")]
    public bool allowDiagonals = true;
    public bool preventDiagonalCornerCut = true;

    public Node[,] grid;
    float nodeDiameter;
    int gridSizeX, gridSizeY;

    // Optional: store a path for gizmo drawing
    public List<Node> debugPath;

    void Awake()
    {
        BuildGrid();
    }

    public void BuildGrid()
    {
        nodeDiameter = nodeRadius * 2f;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        grid = new Node[gridSizeX, gridSizeY];

        // bottom-left corner in world space
        Vector2 bottomLeft = (Vector2)transform.position
                           - Vector2.right * (gridWorldSize.x * 0.5f)
                           - Vector2.up * (gridWorldSize.y * 0.5f);

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = bottomLeft
                    + Vector2.right * (x * nodeDiameter + nodeRadius)
                    + Vector2.up * (y * nodeDiameter + nodeRadius);

                // 0.9f shrinks the probe slightly to avoid false positives on edges
                bool walkable = !Physics2D.OverlapCircle(worldPoint, nodeRadius * 0.9f, unwalkableMask);

                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public Node NodeFromWorldPoint(Vector2 worldPosition)
    {
        // Convert world position ? [0..1] across grid, clamp to be safe
        float percentX = Mathf.Clamp01((worldPosition.x - (transform.position.x - gridWorldSize.x * 0.5f)) / gridWorldSize.x);
        float percentY = Mathf.Clamp01((worldPosition.y - (transform.position.y - gridWorldSize.y * 0.5f)) / gridWorldSize.y);

        int x = Mathf.Clamp(Mathf.RoundToInt((gridSizeX - 1) * percentX), 0, gridSizeX - 1);
        int y = Mathf.Clamp(Mathf.RoundToInt((gridSizeY - 1) * percentY), 0, gridSizeY - 1);

        return grid[x, y];
    }

    public IEnumerable<Node> GetNeighbours(Node node)
    {
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue; // itself

                if (!allowDiagonals && Mathf.Abs(dx) + Mathf.Abs(dy) == 2) continue;

                int nx = node.gridX + dx;
                int ny = node.gridY + dy;

                if (nx < 0 || nx >= gridSizeX || ny < 0 || ny >= gridSizeY) continue;

                // optional: prevent “corner cutting” through two touching walls
                if (preventDiagonalCornerCut && Mathf.Abs(dx) + Mathf.Abs(dy) == 2)
                {
                    Node n1 = grid[node.gridX + dx, node.gridY];
                    Node n2 = grid[node.gridX, node.gridY + dy];
                    if (!n1.walkable || !n2.walkable) continue;
                }

                yield return grid[nx, ny];
            }
        }
    }

    public Node FindNearestWalkable(Node fromNode, int maxRadius = 3)
    {
        // If target is blocked, find closest walkable around it (small BFS ring)
        if (fromNode.walkable) return fromNode;

        for (int r = 1; r <= maxRadius; r++)
        {
            for (int dx = -r; dx <= r; dx++)
                for (int dy = -r; dy <= r; dy++)
                {
                    int nx = fromNode.gridX + dx;
                    int ny = fromNode.gridY + dy;
                    if (nx < 0 || nx >= gridSizeX || ny < 0 || ny >= gridSizeY) continue;
                    if (grid[nx, ny].walkable) return grid[nx, ny];
                }
        }
        return null; // none nearby
    }

    public void SetDebugPath(List<Node> path) => debugPath = path;
    public void RebuildGrid()
    {
        BuildGrid();
        Debug.Log("rebuilt grid");
    }

    void OnDrawGizmos()
    {
        // Draw grid bounds
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 0.1f));

        if (grid == null) return;

        foreach (Node n in grid)
        {
            Gizmos.color = n.walkable ? Color.white : Color.red;
            Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeRadius * 2f - 0.05f));
        }

        if (debugPath != null)
        {
            Gizmos.color = Color.black;
            foreach (var n in debugPath)
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeRadius * 2f - 0.15f));
        }
    }
}