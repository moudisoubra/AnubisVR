using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowControl : MonoBehaviour
{
    public Renderer renderer;
    public Material currentMaterial;
    public bool colorUp;
    public bool colorDown;
    public int colorChange;
    // Start is called before the first frame update
    void Start()
    {
        colorChange = 0;
        colorUp = false;
        colorDown = false;
        renderer = GetComponent<MeshRenderer>();
        currentMaterial = renderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        var col = currentMaterial.GetColor("_EmissionColor");
        Debug.Log(col);
        GlowDown();
        GlowUp();
    }

    public void GlowDown()
    {
        var col = currentMaterial.GetColor("_EmissionColor");
        if (colorDown && colorChange >= -1)
        {
            colorChange--;
            currentMaterial.SetColor("_EmissionColor", col /= 1.5f);
            colorDown = false;
            colorUp = false;
        }
    }

    public void GlowUp()
    {
        var col = currentMaterial.GetColor("_EmissionColor");
        if (colorUp && colorChange <= -1)
        {
            colorChange++;
            currentMaterial.SetColor("_EmissionColor", col *= 1.5f);
            colorUp = false;
            colorDown = false;
        }
    }
}
