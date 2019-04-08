using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DkjisTest : MonoBehaviour
{
    static public DjisPathFindHajjo instance;
    [SerializeField] GameObject[] Nodes;

    public Node[] allnodeedd;



    private List<int> routPoints = new List<int>();

    public struct Node
    {
        public int id;
        public int SourceId;
        public int[] ChildsNode;
        public Transform trans;
        public float distance;
    }

    void Start()
    {
    //    if (instance == null) instance = this;
    //    else Destroy(this);

        Nodes = GameObject.FindGameObjectsWithTag("Nodes");
        allnodeedd = new Node[Nodes.Length];
        // initialize node list
        for (int i = 0; i < Nodes.Length; i++)
        {

            allnodeedd[i] = new Node();
            allnodeedd[i].trans = Nodes[i].transform;
            allnodeedd[i].id = i;
            allnodeedd[i].SourceId = -1;
            allnodeedd[i].distance = Mathf.Infinity;

        }
        //loop finds children for a certain node
        for (int i = 0; i < Nodes.Length; i++)
        {
            allnodeedd[i].ChildsNode = GetNearbyPoint(Nodes[i].transform.position);
        }

    }

    //Find the Child Index Nodes
    int[] GetNearbyPoint(Vector3 pos)
    {

        List<int> nearpoints = new List<int>();
        for (int i = 0; i < allnodeedd.Length; i++)

        {
            float dis = Vector3.Distance(pos, allnodeedd[i].trans.position);

            if (!Physics.Raycast(pos, allnodeedd[i].trans.position - pos, dis))
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

        for (int i = 0; i < allnodeedd.Length; i++)
        {
            float dis = Vector3.Distance(pos, allnodeedd[i].trans.position);

            Vector3 dir = allnodeedd[i].trans.position - pos;

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



    public int[] DjiPath(Vector3 startPos, Vector3 FinalPos)
    {
        // all traval point indices...
        List<int> travalPoints = new List<int>();


        List<int> Visited = new List<int>();
        List<int> Closed = new List<int>();

        int StartNode = GetNearbyNode(startPos);
        int FinalNode = GetNearbyNode(FinalPos);
        Debug.Log("Start Node ID is : " + StartNode + " & " + "Finish Node ID is : " + FinalNode);

        // reset all calculation data
        for (int i = 0; i < allnodeedd.Length; i++)
        {
            allnodeedd[i].SourceId = -1;
            allnodeedd[i].distance = Mathf.Infinity;
        }

        //removes every elements from the list
        routPoints.Clear();




        allnodeedd[StartNode].distance = 0;
        Visited.Add(StartNode);

        while (true)
        {
            int closectNodeId = 0;
            float shortestDis = Mathf.Infinity;
            // finds the index of the node whos distance is the shortest.
            for (int i = 0; i < Visited.Count; i++)
            {
                if (allnodeedd[Visited[i]].distance < shortestDis)
                {
                    closectNodeId = Visited[i];
                    shortestDis = allnodeedd[Visited[i]].distance;
                }
            }
            // checks if the current node is thelast node.
            if (allnodeedd[closectNodeId].id == allnodeedd[FinalNode].id)
            {

                string path = "Path reached .";
                int nextPointId = FinalNode;
                for (int i = 0; i < 50; i++)
                {
                    path += " | " + allnodeedd[nextPointId].id;
                    travalPoints.Add(nextPointId);
                    routPoints.Add(nextPointId);
                    nextPointId = allnodeedd[nextPointId].SourceId;
                    if (nextPointId == -1)
                    {
                        Debug.Log(path);
                        travalPoints.Reverse();
                        return travalPoints.ToArray();
                    }
                }
            }

            //adding the childe node of the current node to the visited list

            for (int i = 0; i < allnodeedd[closectNodeId].ChildsNode.Length; i++)
            {
                int childIndex = allnodeedd[closectNodeId].ChildsNode[i];
                float Dis = Vector3.Distance(allnodeedd[closectNodeId].trans.position, allnodeedd[childIndex].trans.position);
                float newDistance = allnodeedd[closectNodeId].distance + Dis;
                if (newDistance < allnodeedd[childIndex].distance)
                {
                    // allNodes[closectNodeId] is the current node
                    // allNodes[closectNodeId].ChildsNode[i] is the index of the childe in allNodes[]

                    allnodeedd[childIndex].distance = newDistance;
                    allnodeedd[childIndex].SourceId = allnodeedd[closectNodeId].id;

                    if (!Visited.Contains(childIndex))
                    {

                        Visited.Add(childIndex);
                    }
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

        for (int i = 0; i < allnodeedd.Length; i++)
        {
            for (int n = 0; n < allnodeedd[i].ChildsNode.Length; n++)
            {

                Gizmos.color = Color.red;
                Gizmos.DrawLine(allnodeedd[i].trans.position, allnodeedd[allnodeedd[i].ChildsNode[n]].trans.position);

            }
            for (int n = 0; n < routPoints.Count; n++)
            {

                Gizmos.color = Color.green;
                Gizmos.DrawSphere(allnodeedd[routPoints[n]].trans.position, 0.5f);
                if (n > 0) Gizmos.DrawLine(allnodeedd[routPoints[n]].trans.position, allnodeedd[routPoints[n - 1]].trans.position);

            }
        }
    }


}

