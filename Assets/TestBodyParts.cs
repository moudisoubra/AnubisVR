using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBodyParts : MonoBehaviour
{

    public bool Follow;

    public GameObject bone1;
    public GameObject bone2;

    public List<GameObject> invisibleBody;
    public List<GameObject> actualBody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Follow)
        {
            for (int i = 0; i < actualBody.Count; i++)
            {
                actualBody[i].gameObject.transform.position = invisibleBody[i].gameObject.transform.position;
                actualBody[i].gameObject.transform.rotation = invisibleBody[i].gameObject.transform.rotation;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            AddAllChildren(bone1.transform, actualBody);
            AddAllChildren(bone2.transform, invisibleBody);
        }
    }

    private void AddAllChildren(Transform parent, List<GameObject> bonesList)
    {

        foreach (Transform child in parent)
        {
            bonesList.Add(child.gameObject);

            AddAllChildren(child, bonesList);
        }
    }
}
