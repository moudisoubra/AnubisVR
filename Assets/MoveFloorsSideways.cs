using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveFloorsSideways : MonoBehaviour
{
    public GameObject floorMovableRight;
    public GameObject floorMovableLeft;
    public GameObject Stones;

    public float changeRate;
    public float timer;

    public bool floorLeftDone;
    public bool floorRightDone;
    public bool sceneLoaded;

    public Vector3 originalTransformLeft;
    public Vector3 originalTransformRight;

    //public DjisPathFindHajjo script;
    public ChangeHint changeHint;
    // Start is called before the first frame update
    void Start()
    {
        floorRightDone = false;
        floorLeftDone = false;
        originalTransformLeft = floorMovableLeft.transform.position;
        originalTransformRight = floorMovableRight.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (floorMovableLeft.transform.position.x < 296.58f && !floorLeftDone)
        {
            floorMovableLeft.transform.position = new Vector3(floorMovableLeft.transform.position.x + changeRate, floorMovableLeft.transform.position.y, floorMovableLeft.transform.position.z);
        }
        else
        {
            floorLeftDone = true;
        }
        if (floorMovableRight.transform.position.x > 277.82f && !floorRightDone)
        {
            floorMovableRight.transform.position = new Vector3(floorMovableRight.transform.position.x - changeRate, floorMovableRight.transform.position.y, floorMovableRight.transform.position.z);
        }
        else
        {
            floorRightDone = true;
        }

        if (floorLeftDone)
        {
            Stones.gameObject.AddComponent<Rigidbody>();
            timer += Time.deltaTime;

            if (timer > 1.3f)
            {
                floorMovableLeft.transform.position = Vector3.Lerp(floorMovableLeft.transform.position, originalTransformLeft, 0.1f);
                floorMovableRight.transform.position = Vector3.Lerp(floorMovableRight.transform.position, originalTransformRight, 0.1f);
                script.ReFindNodes();
                changeHint.solved = true;
                if (timer > 5)
                {
                    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Level2 Final", LoadSceneMode.Additive);
                    if (asyncLoad.isDone)
                    {
                        sceneLoaded = true;
                    }
                    //GetComponent<MoveFloorsSideways>().enabled = false;
                }
            }

        }
    }
}
