using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingSoubra : MonoBehaviour
{
    static public PathFindingSoubra instance;
    [SerializeField] GameObject[] Nodes;

    public Node[] completeNodes;
    public LayerMask nodeLayer;
    public bool drawGizmo;

    public List<int> points = new List<int>();

    public struct Node
    {
        public int nodeID;
        public int nodeSourceID;
        public int[] childrenNodes;
        public Transform nodeTransform;
        public float nodeDistance;
    }


    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        Nodes = GameObject.FindGameObjectsWithTag("Nodes");
        completeNodes = new Node[Nodes.Length];

        for (int i = 0; i < Nodes.Length; i++)
        {
            completeNodes[i] = new Node();
            completeNodes[i].nodeID = i;
            completeNodes[i].nodeSourceID = -1;
            completeNodes[i].nodeTransform = Nodes[i].transform;
            completeNodes[i].nodeDistance = Mathf.Infinity;

        }

        for (int i = 0; i < Nodes.Length; i++)
        {
            completeNodes[i].childrenNodes = CloseNodes(Nodes[i].transform.position);
        }

    }

    void Update()
    {
        
    }

    int[] CloseNodes(Vector3 position)
    {

        List<int> closeNodes = new List<int>();
        for (int i = 0; i < completeNodes.Length; i++)

        {
            float distance = Vector3.Distance(position, completeNodes[i].nodeTransform.position);

            if (!Physics.Raycast(position, completeNodes[i].nodeTransform.position - position, distance, nodeLayer) && distance > 0.5f)
            {
                closeNodes.Add(i);
            }


        }
        return closeNodes.ToArray();
    }

    int ClosestID(Vector3 position)
    {
        float shortestDist = Mathf.Infinity;
        int closestId = -1;

        for (int i = 0; i < completeNodes.Length; i++)
        {
            float distance = Vector3.Distance(position, completeNodes[i].nodeTransform.position);

            Vector3 direction = completeNodes[i].nodeTransform.position - position;

            if (!Physics.Raycast(position, direction, distance, nodeLayer))
            {
                if (direction.magnitude < shortestDist)
                {
                    shortestDist = direction.magnitude;
                    closestId = i;
                }
            }
        }
        return closestId;
    }
}
