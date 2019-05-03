using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour
{
    public GameObject[] bars;
    public float barChosen;

    // Update is called once per frame
    void Update()
    {
        if (barChosen <= -0.5f)
        {
            barChosen = -0.5f;
        }

        if (barChosen > 4)
        {
            Debug.Log("ITS NOT WORKING");
            barChosen = 4f;
        }

        //if (Input.GetKeyUp(KeyCode.A))
        //{
        //    Increase(barChosen);
        //}

        //if (Input.GetKeyUp(KeyCode.S))
        //{
        //    Decrease(barChosen);
        //}

        //if (Input.GetKeyUp(KeyCode.Z))
        //{
        //    barChosen++;
        //}

        //if (Input.GetKeyUp(KeyCode.X))
        //{
        //    barChosen--;
        //}
    }

    public void Increase()
    {
        bars[(int)barChosen].gameObject.GetComponent<GlowControl>().colorUp = true;
    }

    public void Decrease()
    {
        bars[(int)barChosen].gameObject.GetComponent<GlowControl>().colorDown = true;
    }
}
