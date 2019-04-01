using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNodesHajjo : MonoBehaviour
{
    public GameObject[] Nodes;

    Node[] allNodes;
    Node CurrentNode;


    public struct Node
    {
        
        public int id;
        public int SourceId;
        public Node[] ChildsNode;
        public Transform trans;


        public int gCost;
        public int Hcost;


        public int Fcost
        {
            get { return gCost + Hcost; }
        }
    }

    void Start()
    {
        Nodes = GameObject.FindGameObjectsWithTag("Nodes");
        allNodes = new Node[Nodes.Length];
        // initialize node list
        for (int i = 0; i < Nodes.Length; i++)
        {
            allNodes[i] = new Node();
            allNodes[i].trans = Nodes[i].transform;
            allNodes[i].id = i;
            allNodes[i].SourceId = -1;

            
        }
        //loop finds children for a certain node
        for (int i = 0; i < Nodes.Length; i++)
        {
            allNodes[i].ChildsNode = GetNearbyPoint(Nodes[i].transform.position);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }


    Node[] GetNearbyPoint(Vector3 pos)
    {
        List<Node> nearpoints = new List<Node>();
        for (int i = 0; i < allNodes.Length; i++)
        {
            Vector3 dir = allNodes[i].trans.position - pos;

            if (Physics.Raycast(pos, dir.normalized, dir.magnitude))
            {
                
            }
            else
            {
                nearpoints.Add(allNodes[i]);
            }
        }
        return nearpoints.ToArray();
    }



    void findPath(Vector3 startPos, Vector3 targetPos)
    {
       
        Node[] startNode = GetNearbyPoint(startPos);
        Node[] finalNode = GetNearbyPoint(targetPos);

        List<Node> OpenSet = new List<Node>();
        HashSet<Node> CloseSet = new HashSet<Node>();


        for (int i = 0; i < startNode.Length; i++)
        {
            OpenSet.Add(startNode[i]);
        }


        while (OpenSet.Count > 0)
        {
            Node CurrentNode = OpenSet[0];
            for (int i = 1; i < OpenSet.Count; i++)
            {
                if (OpenSet[i].Fcost < CurrentNode.Fcost || OpenSet[i].Fcost == CurrentNode.Fcost && OpenSet[i].Hcost < CurrentNode.Hcost)
                {
                    CurrentNode = OpenSet[i];
                }
            }


            OpenSet.Remove(CurrentNode);
            CloseSet.Add(CurrentNode);

          //  if(CurrentNode == finalNode[0].id)
            {
                return;
            }
         
        }


        
    }


    private void OnDrawGizmos()
    {
       
        for (int i = 0; i < allNodes.Length; i++)
        {
            for (int n = 0; n < allNodes[i].ChildsNode.Length; n++)
            {
                
                Gizmos.color = Color.red;
                Gizmos.DrawLine(allNodes[i].trans.position, allNodes[i].ChildsNode[n].trans.position);

            }
        }
    }
    

}

