using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    public GameObject floorMovable;
    public GameObject particleEffect;
    public float changeRate;
    public GameObject[] walls;
    public DjisPathFindHajjo script;
    public ChangeHint changeHint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (floorMovable.transform.position.z < 602)
        {
            floorMovable.transform.position = new Vector3(floorMovable.transform.position.x, floorMovable.transform.position.y, floorMovable.transform.position.z + changeRate);
        }
        else
        {
            for (int i = 0; i < walls.Length; i++)
            {
                GameObject.Destroy(walls[i].gameObject);
            }
            GameObject.Destroy(particleEffect);
            script.ReFindNodes();
            changeHint.solved = true;
        }
    }
}
