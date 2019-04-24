using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPathLine : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform lastPoint;
    public LineRenderer line;
    [SerializeField] private int[] PathPoints;
    Vector3[] newPos;
    public int currentPointCount = 0;
    public int pointCountLength = 0;

    public float dist;
    public float counter;
    public float lineSpeed = 5f;

   

    // Start is called before the first frame update
    void Start()
    {
      
        currentPointCount = line.positionCount;
        pointCountLength = PathPoints.Length;
    }

    // Update is called once per frame
    void Update()
    {
        PathPoints = DjisPathFindHajjo.instance.DjiPath(transform.position, lastPoint.position);
        //line.SetPosition(0, startPoint.position);

        List<Vector3> newPoints = new List<Vector3>();
        newPoints.Add(transform.position);
        for (int i = 0; i < PathPoints.Length; i++)
        {
            newPoints.Add(DjisPathFindHajjo.instance.allNodes[PathPoints[i]].trans.position);
        }

        newPoints.Add(lastPoint.position);

        line.SetPositions(newPoints.ToArray());

    }
}
