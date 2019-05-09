using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hajjo;

public class Grid : MonoBehaviour
{
    public static Grid instance;
    public SpacePartitioning fk;

    public Color ChangeColors;
    public int OffSetX, OffSetY;
    public GridcellData[,] grid;
    public int gridSizeX, GridSizeY;


        public class GridcellData
        {
            public List<Transform> ContainObjects = new List<Transform>();
        }


    void Awake()
    {
        instance = this;
        CreatGrid();
       
    }


    void CreatGrid()
    {
        grid = new GridcellData[gridSizeX, GridSizeY];
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < GridSizeY; y++)
            {
                grid[x, y] = new GridcellData();
            }
        }
    }

    public Vector2Int GetCell(Vector3 pos)
    {
        Vector2Int cell=Vector2Int.zero;
        cell.x = (int)pos.x / OffSetX;
        cell.y = (int)pos.z / OffSetY;

        return cell;
    }
   
    public void OnDrawGizmos()
    {
       
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < GridSizeY; y++)
            {
                Gizmos.color = Color.blue; 
                Gizmos.DrawSphere(new Vector3(x * OffSetX, 0, y * OffSetY), 4f);
                Gizmos.color = ChangeColors;
                Gizmos.DrawWireCube(new Vector3((x * OffSetX) + OffSetX* 0.5f, 0f, (y * OffSetY) + OffSetY * 0.5f), new Vector3(OffSetX,0,OffSetY));
                
            }
        }
    }
}

