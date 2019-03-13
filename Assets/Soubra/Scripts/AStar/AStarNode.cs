using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarNode
{

    public Vector3 worldPosition;
    public bool walkable;

    public int gCost;
    public int hCost;

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
        private set { }
    }

    public Vector3Int gridPosition;

    public AStarNode(Vector3 worldposition)
    {
        worldPosition = worldposition;
    } 
}
