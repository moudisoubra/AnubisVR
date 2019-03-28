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
            allNodes[i].ChildsNode = GetChildPoint(Nodes[i].transform);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }


    Node[] GetChildPoint(Transform pos)
    {
        List<Node> nearpoints = new List<Node>();
        for (int i = 0; i < allNodes.Length; i++)
        {
            Vector3 dir = allNodes[i].trans.position - pos.position;

            if (Physics.Raycast(pos.position, dir.normalized, dir.magnitude))
            {
                
            }
            else
            {
                nearpoints.Add(allNodes[i]);
            }
        }
        return nearpoints.ToArray();
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
    // you need to make core of the script now...
    // you need to make a public function which can be called from any where..
    // well this is a class...
    // well any after getting the stuff done you could easly figure it out your self...
    // good...
    // it will be a function return Vector3[] the route...
    // you know, i mean perent id -> parent id -> and so on...
    // using the A* pathfinding method... i mean F value stuff...
    // good luck...

}

