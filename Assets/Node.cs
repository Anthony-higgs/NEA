using UnityEngine;

public class Node
{
    public bool walkable;          // can enemy go to this tile
    public Vector2 worldPosition;  // world-space position of the tile center
    public int gridX, gridY;       // index in the grid array

    // A* costs
    public int gCost;              // cost from start to this node
    public int hCost;              // heuristic to target
    public Node parent;            // used to reconstruct the path

    public int fCost => (gCost == int.MaxValue) ? int.MaxValue : gCost + hCost; //fcost is g cost if it is the max value otherwise it is gcost + hcost

    public Node(bool walkable, Vector2 worldPos, int x, int y)
    {
        this.walkable = walkable;
        worldPosition = worldPos;
        gridX = x; gridY = y;
        ResetPathData();
    }

    public void ResetPathData()
    {
        gCost = int.MaxValue;
        hCost = 0;
        parent = null;
    }
}