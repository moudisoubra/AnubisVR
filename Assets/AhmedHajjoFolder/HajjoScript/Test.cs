using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Test : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

        MeshFilter meshfilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRender = gameObject.AddComponent<MeshRenderer>();
        
      


        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-1, 0, -1), //Bottom LEFT
            new Vector3( 0, 0,  1), //top
            new Vector3( 1,  0, -1) //bottom right


        };

        Vector2[] uv = new Vector2[]
         {
            new Vector2(0,0), //Bottom LEFT
            new Vector2(0,1), //top
            new Vector2(1,1) //bottom right


         };

        int[] indices = new int[]
        {
            0, 1 ,2
        };

        Mesh mesh = meshfilter.mesh;
        mesh.vertices = vertices;
        mesh.triangles = indices; 




        

        for (int i = 0; i < vertices.Length; i++)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
