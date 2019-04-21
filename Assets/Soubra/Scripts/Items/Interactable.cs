using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Interactable interactableScript;
    // Start is called before the first frame update
    void Start()
    {
        interactableScript = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        interactableScript.Update();
    }
}
