using System.Collections.Generic;
using UnityEngine;

public class AStarPathTestSystem : MonoBehaviour
{
    static public AStarPathTestSystem instance;
    [SerializeField] Node[] allNods;
    Node currentNode;

 [System.Serializable]
    public struct Node
    {
        public int iD;
        public int sorceId;
        public float f;
        public Transform trans;
        public Node[] childNodes;
    }

    void Start()
    {

        if (instance == null) instance = this;
        else Destroy(this);
        //

        GameObject[] pointList = GameObject.FindGameObjectsWithTag("node");
        allNods = new Node[pointList.Length];

        for (int i = 0; i < pointList.Length; i++)
        {
            allNods[i] = new Node();
            allNods[i].trans = pointList[i].transform;
            allNods[i].iD = i;
            allNods[i].sorceId = -1;
        }

        for (int i = 0; i < pointList.Length; i++)
        {
            allNods[i].childNodes = GetChildPoint(allNods[i]);
        }

    }

    Node[] GetChildPoint(Node sendNode)
    {
        List<Node> movablePoints = new List<Node>();
        for (int i = 0; i < allNods.Length; i++)
        {
            if (sendNode.iD != allNods[i].iD && !Physics.Raycast(sendNode.trans.position, allNods[i].trans.position - sendNode.trans.position))
            {
                movablePoints.Add(allNods[i]);
            }
        }
        return movablePoints.ToArray();
    }

    Node[] GetNearbyPoints(Transform pos)
    {
        List<Node> nearPoints = new List<Node>();
        for (int i = 0; i < allNods.Length; i++)
        {
            if (!Physics.Raycast(pos.position, allNods[i].trans.position - pos.position))
            {
                nearPoints.Add(allNods[i]);
            }
        }
        return nearPoints.ToArray();
    }

    private void setHcost(int nodeId, Transform start, Transform finish)
    {
        allNods[nodeId].f = Vector3.Distance(start.position, allNods[nodeId].trans.position) + Vector3.Distance(finish.position, allNods[nodeId].trans.position);
    }

    // need to call this function from another script to see if it CRASH the programe or not... 
    Node finalNode;
    public Transform[] ReturnRout(Transform start, Transform finish)
    {
        Node[] startNeibor = GetNearbyPoints(start);
        Node[] finalNeibor = GetNearbyPoints(finish);
        List<int> checkList = new List<int>();
        List<int> finishedList = new List<int>();

        int trys = 0;

        for (int i = 0; i < startNeibor.Length; i++)
        {
            checkList.Add(startNeibor[i].iD);
        }

        while (trys < 80)
        {
            float lowestFcost = Mathf.Infinity;

            for (int i = 0; i < checkList.Count; i++)
            {
                setHcost(checkList[i], start, finish);
                if (lowestFcost > allNods[checkList[i]].f)
                {
                    //considers the one with the lowest f cost
                    lowestFcost = allNods[checkList[i]].f;
                    currentNode = allNods[checkList[i]];
                }
            }

            Debug.Log("lowest node id at run count " + trys + " |-| " + currentNode.iD);

            // adds child nodes to the check list
            Debug.Log("Childe count " + currentNode.childNodes.Length);
            for (int i = 0; i < currentNode.childNodes.Length; i++)
            {
                if (!finishedList.Contains(currentNode.childNodes[i].iD) && currentNode.childNodes[i].iD != currentNode.iD)
                {
                    setHcost(currentNode.childNodes[i].iD, start, finish);
                    allNods[currentNode.childNodes[i].iD].sorceId = currentNode.iD;
                    checkList.Add(currentNode.childNodes[i].iD);
                    Debug.Log("Childe added at try " + trys);
                }
            }

            //goes through active node to see if anyone matches the final
            for (int i = 0; i < checkList.Count; i++)
            {
                if (checkList[i] == finalNeibor[0].iD)
                {
                    finalNode = allNods[checkList[i]];
                    Debug.Log("Reached");
                    // need to add the route generateion code
                    return null;
                }
            }
            checkList.Remove(currentNode.iD);
            finishedList.Add(currentNode.iD);
            //
            trys++;
        }

        return null;
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < allNods.Length; i++)
        {
            for (int n = 0; n < allNods[i].childNodes.Length; n++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(allNods[i].trans.position, allNods[i].childNodes[n].trans.position);
            }
        }
       
            bool drawPoint = true;
            int parentId = finalNode.iD;
            int maxRun = 50;
            while (drawPoint || maxRun == 0)
            {
                maxRun--;
                if (allNods[parentId].sorceId != -1)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawSphere(allNods[parentId].trans.position, 1.0f);
                    Gizmos.DrawLine(allNods[parentId].trans.position, allNods[allNods[parentId].sorceId].trans.position);
                    parentId = allNods[parentId].sorceId;
                }

                else
                {
                    Gizmos.DrawSphere(allNods[parentId].trans.position, 1.0f);
                    drawPoint = false;
                }

            if (maxRun <= 0)
            {
                drawPoint = false;
            }


            }
         
    }
}
