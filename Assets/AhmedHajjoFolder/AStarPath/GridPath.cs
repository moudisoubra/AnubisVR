using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPath : MonoBehaviour
{
    public LayerMask UnWalkAbleMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    ANode[,] grid;

    public Transform player;
    float nodeDiameter;
    int gridSizeX, GridSizeY;
    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        GridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreatGrid();
    }


    void CreatGrid()
    {
        grid = new ANode[gridSizeX, GridSizeY];
        Vector3 WorldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < GridSizeY; y++)
            {
                Vector3 worldPoint = WorldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeDiameter + nodeRadius);
                bool walkAble = !(Physics.CheckSphere(worldPoint, nodeRadius, UnWalkAbleMask));
                grid[x, y] = new ANode(walkAble, worldPoint, x, y); 

            }
        }
    }

    public List<ANode> GetNeighbours(ANode nodes)
    {
        List<ANode> neighbours = new List<ANode>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {

                if (x == 0 && y == 0)
                    continue;

                int checkX = nodes.GridX + x;
                int checkY = nodes.GridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < GridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }


            }

        }
        return neighbours;
    }
    public ANode NodeFromWorldPoint(Vector3 worldPosition)
    {

        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);


        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((GridSizeY - 1) * percentY);



        return grid[x, y];

    }
    public List<ANode> path;
    public void OnDrawGizmos()
    {


        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid != null)
        {
            ANode playerNode = NodeFromWorldPoint(player.position);
            foreach (ANode n in grid)
            {

                Gizmos.color = (n.walkAble) ? Color.blue : Color.red;
                if (path != null)
                    if (path.Contains(n))
                        Gizmos.color = Color.yellow;
                if (playerNode == n)
                {
                    Gizmos.color = Color.black;
                }
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter + 0.1f));
            }
        }
    }
}
