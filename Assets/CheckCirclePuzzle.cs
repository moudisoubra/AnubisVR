using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCirclePuzzle : MonoBehaviour
{
    public GameObject rotatedSarco;
    public GameObject circle1;
    public GameObject circle2;
    public GameObject circle3;
    public GameObject frontSarco;
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
        if (circle1.GetComponent<RotatingObject>().properlyRotated &&
            circle2.GetComponent<RotatingObject>().properlyRotated &&
            circle3.GetComponent<RotatingObject>().properlyRotated)
        {
            puzzleDone = true;
            freezePuzzle = true;
        }

        if (puzzleDone)
        {
            frontSarco.transform.rotation = Quaternion.Slerp(frontSarco.transform.rotation, rotatedSarco.transform.rotation, 0.1f * Time.deltaTime);
            freezePuzzle = false;
        }
    }
}
