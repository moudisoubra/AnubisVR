using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DjisPathFindHajjo : MonoBehaviour
{
    static public DjisPathFindHajjo instance;
    [SerializeField] GameObject[] Nodes;

    public Node[] allNodes;
    public LayerMask ignoreLayers;
    public bool DrawGizmo;


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

    public void ReFindNodes()
    {
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

            if (!Physics.Raycast(pos, allNodes[i].trans.position - pos, dis, ignoreLayers) && dis > 0.5f)
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
            
            if (!Physics.Raycast(pos, dir, dis, ignoreLayers))
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
        List<int> FollowPath = new List<int>();
        List<int> Visiting = new List<int>();
        List<int> Visited = new List<int>();

        int StartNode = GetNearbyNode(startPos);
        int FinalNode = GetNearbyNode(FinalPos);

        // reset all calculation data
        for (int i = 0; i < allNodes.Length; i++)
        {
            allNodes[i].SourceId = -1;
            allNodes[i].distance = Mathf.Infinity;
        }

        //removes every elements from the list
        routPoints.Clear();




        allNodes[StartNode].distance = 0;
        Visiting.Add(StartNode);

        while (Visiting.Count >0)
        {
            int closectNodeId = 0;
            float shortestDis = Mathf.Infinity;
            // finds the index of the node whos distance is the shortest.
            for (int i = 0; i < Visiting.Count; i++)
            {
                if (allNodes[Visiting[i]].distance < shortestDis)
                {
                    closectNodeId = Visiting[i];

                    shortestDis = allNodes[Visiting[i]].distance;


                }
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

                    if (!Visiting.Contains(childIndex)) {

                        Visiting.Add(childIndex);
                            }
                }


            }

            Visiting.Remove(closectNodeId);
            Visited.Add(closectNodeId);
        }



        //---------------------------------
        //back track
        int nullDist = -1;
        int tempNodeID = FinalNode;
     //   if (allNodes[tempNodeID].SourceId != nullDist)
        
            while (tempNodeID != nullDist)
            {
                routPoints.Insert(0, tempNodeID);
                tempNodeID = allNodes[tempNodeID].SourceId;
            }
            return routPoints.ToArray();
        

    }


    private void OnDrawGizmos()
    {
        if (DrawGizmo)
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


}

