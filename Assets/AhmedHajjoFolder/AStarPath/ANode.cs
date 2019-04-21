using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANode
{

    public int gCost;
    public int hCost;
    public bool walkAble;
    public Vector3 worldPosition;
    public int GridX;
    public int GridY;
    public Bounds bound;
    public ANode parent;
    public ANode(bool _walkAble, Vector3 _WorldPos , int _GridX, int _GridY){
        walkAble = _walkAble;
        worldPosition = _WorldPos;
        GridX = _GridX;
        GridY = _GridY;
    }


    public int fCost{

get {
    return gCost + hCost;

}

    }

}
