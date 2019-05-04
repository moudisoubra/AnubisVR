using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDataHajjo : MonoBehaviour
{
    public bool solved = false;
    public bool facing = false;
    public int keyType = 0;
    public Animation anim;

    private void Start()
    {
        anim = GetComponent<Animation>();
    }

    public void Reset()
    {
        Debug.Log("Reseting...");
        anim.Play("SpinBackEye");
        facing = false;
    }

}
