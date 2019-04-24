using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCirclePuzzle : MonoBehaviour
{
    public GameObject circle1;
    public GameObject circle2;
    public GameObject circle3;
    public GameObject wall;
    public bool puzzleDone;
    public bool freezePuzzle;

    // Start is called before the first frame update
    void Start()
    {
        puzzleDone = false;
        freezePuzzle = false;   
    }

    // Update is called once per frame
    void Update()
    {
        if (circle1.GetComponent<RotateCircle>().properlyRotated &&
            circle2.GetComponent<RotateCircle>().properlyRotated &&
            circle3.GetComponent<RotateCircle>().properlyRotated)
        {
            puzzleDone = true;
            freezePuzzle = true;
        }

        if (puzzleDone && freezePuzzle)
        {
            wall.GetComponent<MeshRenderer>().enabled = false;
            freezePuzzle = false;
        }
    }
}
