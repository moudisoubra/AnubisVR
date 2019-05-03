using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfAABB : MonoBehaviour
{
    [HideInInspector] public Vector3 Max;
    [HideInInspector] public Vector3 Min;
    public Vector3 LocalMax;
    public Vector3 LocalMin;
    public Vector3 vel;
    public Color boxColor;

    void Start()
    {
        AABBphysics.Istance.aabb.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        Max = transform.position + LocalMax;
        Min = transform.position + LocalMin;
        transform.Translate(vel);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = boxColor;
        Gizmos.DrawCube(transform.position, LocalMax + LocalMin *2);
    }
}
