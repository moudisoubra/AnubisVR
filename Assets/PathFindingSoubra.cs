//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PathFindingSoubra : MonoBehaviour
//{
//    static public PathFindingSoubra instance;
//    [SerializeField] GameObject[] Nodes;

//    public Node[] completeNodes;
//    public LayerMask nodeLayer;
//    public bool drawGizmo;

//    public List<int> points = new List<int>();

//    public struct Node
//    {
//        public int nodeID;
//        public int nodeSourceID;
//        public int[] childrenNodes;
//        public Transform nodeTransform;
//        public float nodeDistance;
//    }


//    void Start()
//    {
//        if (instance == null) instance = this;
//        else Destroy(this);

//        Nodes = GameObject.FindGameObjectsWithTag("Nodes");
//        completeNodes = new Node[Nodes.Length];

//        for (int i = 0; i < Nodes.Length; i++)
//        {
//            completeNodes[i] = new Node();
//            completeNodes[i].nodeID = i;
//            completeNodes[i].nodeSourceID = -1;
//            completeNodes[i].nodeTransform = Nodes[i].transform;
//            completeNodes[i].nodeDistance = Mathf.Infinity;

//        }

//        for (int i = 0; i < Nodes.Length; i++)
//        {
//            completeNodes[i].childrenNodes = GetNearbyPoint(Nodes[i].transform.position);
//        }

//    }

//    void Update()
//    {
        
//    }

//    int[] GetNearbyPoint(Vector3 pos)
//    {

//        List<int> nearpoints = new List<int>();
//        for (int i = 0; i < completeNodes.Length; i++)

//        {
//            float dis = Vector3.Distance(pos, completeNodes[i].trans.position);

//            if (!Physics.Raycast(pos, completeNodes[i].trans.position - pos, dis, ignoreLayers) && dis > 0.5f)
//            {
//                nearpoints.Add(i);
//            }


//        }
//        return nearpoints.ToArray();
//    }
//}
