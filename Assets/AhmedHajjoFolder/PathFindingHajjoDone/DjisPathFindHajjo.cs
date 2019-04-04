using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DjisPathFindHajjo : MonoBehaviour
{
    static public DjisPathFindHajjo instance;
    public GameObject[] Nodes;

    Node[] allNodes;
    Node CurrentNode;
    public GameObject StartPos;
    public GameObject TargetPos;

    private List<int> routPoints = new List<int>();

    public struct Node
    {

        public int id;
        public int SourceId;
        public int[] ChildsNode;
        public Transform trans;
        public float distance;

        //public int gCost;
        //public int Hcost;


        //public int Fcost
        //{
        //    get { return gCost + Hcost; }
        //}
    }

    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        Nodes = GameObject.FindGameObjectsWithTag("Nodes");
        allNodes = new Node[Nodes.Length];
        // initialize node list
        for (int i = 0; i < Nodes.Length; i++)
        {

            allNodes[i] = new Node();
            allNodes[i].trans = Nodes[i].transform;
            allNodes[i].id = i;
            allNodes[i].SourceId = -1;
            allNodes[i].distance = Mathf.Infinity;

        }
        //loop finds children for a certain node
        for (int i = 0; i < Nodes.Length; i++)
        {
            allNodes[i].ChildsNode = GetNearbyPoint(Nodes[i].transform.position);
        }

    }

    //Find the Child Index Nodes
    int[] GetNearbyPoint(Vector3 pos)
    {

        List<int> nearpoints = new List<int>();
        for (int i = 0; i < allNodes.Length; i++)

        {
            float dis = Vector3.Distance(pos, allNodes[i].trans.position);

            if (!Physics.Raycast(pos, allNodes[i].trans.position - pos, dis) && dis > 1)
            {
                nearpoints.Add(i);
            }


        }
        return nearpoints.ToArray();
    }
    


    //Find Closest Node Index Of Start and Last NodeIndex
    int GetNearbyNode(Vector3 pos)
    {
        float shortestDist = Mathf.Infinity;
        int closestId = -1;

        for (int i = 0; i < allNodes.Length; i++)
        {
            float dis = Vector3.Distance(pos, allNodes[i].trans.position);

            Vector3 dir = allNodes[i].trans.position - pos;

            if (!Physics.Raycast(pos, dir, dis))
            {
                if (dir.magnitude < shortestDist)
                {
                    shortestDist = dir.magnitude;
                    closestId = i;
                }
            }
        }
        return closestId;
    }



    public void DjiPath(Vector3 startPos, Vector3 FinalPos)
    {
        List<int> Visited = new List<int>();
        List<int> Closed = new List<int>();

        int StartNode = GetNearbyNode(startPos);
        int FinalNode = GetNearbyNode(FinalPos);
        Debug.Log("Start Node ID is : " + StartNode + " & " + "Finish Node ID is : " + FinalNode);

        // reset all calculation data
        for (int i = 0; i < allNodes.Length; i++)
        {
            allNodes[i].SourceId = -1;
            allNodes[i].distance = Mathf.Infinity;
        }

        //removes every elements from the list
        routPoints.Clear();




        allNodes[StartNode].distance = 0;
        Visited.Add(StartNode);
        // break limit if something goes wrong
        while (true)
        {
            int closectNodeId = 0;
            float shortestDis = Mathf.Infinity;
            // finds the index of the node whos distance is the shortest.
            for (int i = 0; i < Visited.Count; i++)
            {
                if (allNodes[Visited[i]].distance < shortestDis)
                {
                    closectNodeId = Visited[i];
                    shortestDis = allNodes[Visited[i]].distance;
                }
            }
            // checks if the current node is thelast node.
            if (allNodes[closectNodeId].id == allNodes[FinalNode].id)
            {

                string path = "Path reached .";
                int nextPointId = FinalNode;
                for (int i = 0; i < 50; i++)
                {
                    path += " | " + allNodes[nextPointId].id;
                    routPoints.Add(nextPointId);
                    nextPointId = allNodes[nextPointId].SourceId;
                    if (nextPointId == -1)
                    {
                        Debug.Log(path);
                        return;
                    }
                }

                return;

            }
           
            //adding the childe node of the current node to the visited list

            for (int i = 0; i < allNodes[closectNodeId].ChildsNode.Length; i++)
            {
                int childIndex = allNodes[closectNodeId].ChildsNode[i];
                float Dis = Vector3.Distance(allNodes[closectNodeId].trans.position, allNodes[childIndex].trans.position);
                float newDistance = allNodes[closectNodeId].distance + Dis;
                if (newDistance < allNodes[childIndex].distance)
                {
                    // allNodes[closectNodeId] is the current node
                    // allNodes[closectNodeId].ChildsNode[i] is the index of the childe in allNodes[]
                    
                    allNodes[childIndex].distance = newDistance;
                    allNodes[childIndex].SourceId = allNodes[closectNodeId].id;
          
                    if (!Visited.Contains(childIndex)) Visited.Add(childIndex);
                }


            }

            Visited.Remove(closectNodeId);
            Closed.Add(closectNodeId);
        }

    }



    //void findPath(Vector3 startPos, Vector3 targetPos)
    //{

    //    Node[] startNode = GetNearbyPoint(startPos);
    //    Node[] finalNode = GetNearbyPoint(targetPos);

    //    List<Node> OpenSet = new List<Node>();
    //    HashSet<Node> CloseSet = new HashSet<Node>();


    //    for (int i = 0; i < startNode.Length; i++)
    //    {
    //        OpenSet.Add(startNode[i]);
    //    }


    //    while (OpenSet.Count > 0)
    //    {
    //        Node CurrentNode = OpenSet[0];
    //        for (int i = 1; i < OpenSet.Count; i++)
    //        {
    //            Debug.Log("FindPath" + OpenSet[i].Fcost);
    //            if ((OpenSet[i].Fcost <= CurrentNode.Fcost) && OpenSet[i].Hcost < CurrentNode.Hcost)
    //            {
    //                CurrentNode = OpenSet[i];

    //            }
    //        }


    //        OpenSet.Remove(CurrentNode);
    //        CloseSet.Add(CurrentNode);




    //        //    if (CurrentNode == finalNode)
    //        //{
    //        //    return;
    //        //}

    //    }



    //    }\




    private void OnDrawGizmos()
    {

        for (int i = 0; i < allNodes.Length; i++)
        {
            for (int n = 0; n < allNodes[i].ChildsNode.Length; n++)
            {

                Gizmos.color = Color.red;
                Gizmos.DrawLine(allNodes[i].trans.position, allNodes[allNodes[i].ChildsNode[n]].trans.position);

            }
            for (int n = 0; n < routPoints.Count; n++)
            {

                Gizmos.color = Color.green;
                Gizmos.DrawSphere(allNodes[routPoints[n]].trans.position, 0.5f);
                if (n > 0) Gizmos.DrawLine(allNodes[routPoints[n]].trans.position, allNodes[routPoints[n - 1]].trans.position);

            }
        }
    }


}

