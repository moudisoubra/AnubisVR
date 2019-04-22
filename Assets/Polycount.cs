using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polycount : MonoBehaviour
{
    public int numTriangles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        numTriangles = gameObject.GetComponent<MeshFilter>().mesh.triangles.Length / 3;
    }
}
