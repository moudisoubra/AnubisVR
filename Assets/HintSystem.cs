using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintSystem : MonoBehaviour
{


    [SerializeField] Transform startPoint;
    [SerializeField] Transform lastPoint;
    [SerializeField] private int[] PathPoints;

    private int currentPoint = 0;
    private int lastNodeID = -1;

    public LayerMask Wall;

    public bool pathCollected = false;

    float speed = 10f;
    public float MaxDis = 5f;
    float timer = 0;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(DjisPathFindHajjo.instance.gameObject);
        lastPoint = GetComponentInParent<HintSpawn>().currentHint;
        Vector3 pos;
        Vector3 dir = lastPoint.position - transform.position;
        if (!Physics.Raycast(transform.position, dir, MaxDis, Wall) && dir.magnitude < MaxDis)
        {
            //seek the player
            transform.position = Vector3.MoveTowards(transform.position, lastPoint.position, Time.deltaTime * speed);
            pathCollected = false;
        }
        else
        {

            if (!pathCollected)
            {
                currentPoint = 0;
                PathPoints = DjisPathFindHajjo.instance.DjiPath(transform.position, lastPoint.position);

                pathCollected = true;
            }

            pos = DjisPathFindHajjo.instance.allNodes[PathPoints[currentPoint]].trans.position;



            if (Vector3.Distance(transform.position, pos) < 0.5f)
            {
                currentPoint++;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
            }

            if (currentPoint == PathPoints.Length)
            {
                pathCollected = false;
                currentPoint = -1;
            }

        }
        if (Vector3.Distance(this.transform.position, lastPoint.position) < 1)
        {
            timer += Time.deltaTime;
            if (timer > 3)
            {
                Debug.Log("done");
                Destroy(this.gameObject);
            }
        }
    }
}
