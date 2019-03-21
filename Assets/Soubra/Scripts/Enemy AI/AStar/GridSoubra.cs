using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridSoubra : MonoBehaviour
{
    public List<AStarNode> grid;
    public AStarNode Node;
    public Vector3 topLeftPosition;
    public int gridSizeX;
    public int gridSizeZ;
    public GameObject plane;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            CreatGrid();
    }

    void CreatGrid()
    {
        topLeftPosition = new Vector3(plane.transform.localScale.x/2, 0, plane.transform.localScale.z/2);
        grid = new List<AStarNode>();

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int i = 0; i < gridSizeZ; i++)
            {
                AStarNode tempNode = new AStarNode(new Vector3(i, 0.5f,x));
                grid.Add(tempNode);
            }
        }


    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < grid.Count; i++)
        {
            Gizmos.DrawCube(grid[i].worldPosition, Vector3.one);
        }
    }
}
