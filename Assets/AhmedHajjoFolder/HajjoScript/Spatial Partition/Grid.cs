using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

   
   
    public Vector2 gridWorldSize;
    public float nodeRadius;
    ANode[,] grid;


    float nodeDiameter =1f;
    int gridSizeX = 16, GridSizeY = 16;



    void Start()
    {
        //nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        GridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreatGrid();
    }


    void CreatGrid()
    {
        grid = new ANode[gridSizeX, GridSizeY];
        //Vector3 WorldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < GridSizeY; y++)
            {
                Vector3 worldPoint =Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeDiameter + nodeRadius);
                bool walkAble = !(Physics.CheckSphere(worldPoint, nodeRadius));
                grid[x, y].bound = new Bounds(new Vector2(x * gridWorldSize.x, y * gridWorldSize.y),gridWorldSize);
                grid[x, y] = new ANode(walkAble, worldPoint, x, y); 


            }
        }
    }



   
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 0, gridWorldSize.y));

        if (grid != null)
        {
            foreach (ANode n in grid)
            {

                Gizmos.color = (n.walkAble) ? Color.blue : Color.red;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter + 0.1f));
            }
        }
    }
}

