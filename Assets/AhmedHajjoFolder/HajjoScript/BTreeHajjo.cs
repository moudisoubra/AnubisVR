using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreeHajjo : MonoBehaviour
{
    NodesHajjo root;


    public int Health = 100;
    public int Mana = 100;

    // Start is called before the first frame update
    void Start()
    {
        root = new SelectorHajjo();
        root = new SequencesHajjo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
