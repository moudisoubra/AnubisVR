using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintSpawn : MonoBehaviour
{
    public Transform currentHint;
    public Transform[] Hints;
    public int trackHint;
    public GameObject hintTrail;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentHint = Hints[trackHint];

        if (currentHint.GetComponent<ChangeHint>().solved)
        {
            trackHint++;
        }
    }

    public void SendHint()
    {
        GameObject Trail = Instantiate(hintTrail, this.transform.position, Quaternion.identity);
        Trail.transform.parent = this.transform;
    }
}
