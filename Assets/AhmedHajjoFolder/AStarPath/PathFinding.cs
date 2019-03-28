using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{

    
    GridPath grid;

    public Transform seeker , target;
     void Awake() {
        grid = GetComponent<GridPath>();
    }

     void Update() {
        findPath(seeker.position,target.position);
    }
    void findPath (Vector3 startPos, Vector3 targetPos){

        ANode startNode = grid.NodeFromWorldPoint(startPos);
        ANode TargetNode = grid.NodeFromWorldPoint(targetPos);

        List<ANode> openSet = new List <ANode>();
        HashSet<ANode> closeSet = new HashSet<ANode>();
        openSet.Add(startNode);

        while(openSet.Count > 0){
        ANode currentNode = openSet[0];
        for (int i = 1; i<openSet.Count; i ++){
            if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost){
                currentNode = openSet[i];
            }
        }

        openSet.Remove(currentNode);
        closeSet.Add(currentNode);

        if (currentNode ==TargetNode){
        RetracePath(startNode, TargetNode);
        return;
        }

        foreach (ANode neighbour in grid.GetNeighbours(currentNode))
        {
            if (!neighbour.walkAble || closeSet.Contains(neighbour)){
                continue;
            }
            int newMoveMentCostToNeighbour = currentNode.gCost + GetDistance(currentNode,neighbour);
            if (newMoveMentCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
                neighbour.gCost = newMoveMentCostToNeighbour;
                neighbour.hCost = GetDistance(neighbour,TargetNode);
                neighbour.parent = currentNode;


                if (!openSet.Contains(neighbour))
                    openSet.Add(neighbour);
            }
        }

        } 

    }


    void RetracePath(ANode startNode, ANode endNode){
        List <ANode> path = new List<ANode>();
        ANode currentNode = endNode;

    while(currentNode != startNode){
        path.Add(currentNode);
        currentNode = currentNode.parent;
    }
    path.Reverse();

    grid.path = path;
    }
    int GetDistance(ANode nodeA, ANode nodeB){
        int distX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
        int distY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

        if(distX > distY)

        return 14*distY + 10*(distX - distY);
        return 14* distX + 10 *(distY - distX);
    }
    
}
