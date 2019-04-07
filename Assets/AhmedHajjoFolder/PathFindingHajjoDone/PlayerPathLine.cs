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



    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        newPos = new Vector3[line.positionCount];
    }

    // Update is called once per frame
    void Update()
    {
        PathPoints = DjisPathFindHajjo.instance.DjiPath(startPoint.position, lastPoint.position);

        newPos[0] = startPoint.position;
        newPos[1] = DjisPathFindHajjo.instance.allNodes[PathPoints[0]].trans.position;
        newPos[2] = DjisPathFindHajjo.instance.allNodes[PathPoints[1]].trans.position;

        for (int i = 2; i > line.positionCount; i++)
        {
            newPos[i] = DjisPathFindHajjo.instance.allNodes[PathPoints[i]].trans.position;
        }

        line.SetPositions(newPos);
    }
}
