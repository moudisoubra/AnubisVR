using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBodyParts : MonoBehaviour
{
    
    public GameObject bone1;
    public GameObject bone2;

    public List<GameObject> invisibleBody;
    public List<GameObject> actualBody;

    public List<Transform> actualBodyTransforms;
    public List<Transform> invisiBodyTransforms;

    public float timer;

    public List<Transform> lastPosition;
    public int qwe;
    public float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        qwe = 0;
        //StartCoroutine(ReturnTransform(new List<GameObject>(),new List<Transform>()));
    }

    private void Update()
    {
        timer += Time.deltaTime;

        //if (timer > 5 && timer <= 700)
        //{
        //    ReturnTransform(actualBodyTransforms, invisiBodyTransforms);
        //}
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timer = 0;
            t = 0;
            invisibleBody.Clear();
            actualBody.Clear();

            actualBodyTransforms.Clear();
            invisiBodyTransforms.Clear();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            AddAllChildren(bone1.transform, actualBody);
            AddAllChildren(bone2.transform, invisibleBody);

            AddAllTransforms(bone1.transform, actualBodyTransforms);
            AddAllTransforms(bone2.transform, invisiBodyTransforms);

        }

        if (timer < 5000)
        {
            if (timer <= 2)
            {
                for (int i = 0; i < actualBody.Count; i++)
                {
                    actualBody[i].gameObject.transform.position = invisibleBody[i].gameObject.transform.position;
                    actualBody[i].gameObject.transform.rotation = invisibleBody[i].gameObject.transform.rotation;
                }
            }
            else
            {
                StartCoroutine(ReturnTransform(actualBody,invisiBodyTransforms, actualBodyTransforms));
            }
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

    private void AddAllTransforms(Transform parent, List<Transform> transformList)
    {
        foreach (Transform child in parent)
        {
            transformList.Add(child);
            AddAllTransforms(child, transformList);
        }
    }
    public Transform begin, end, movable;
    
    public IEnumerator ReturnTransform(List<GameObject> activeBodyParts, List<Transform> invisiTransform, List<Transform> bodyTransforms)
    {

        
        while (t<=1)
        {
            t += .05f;
            for (int i = 0; i < activeBodyParts.Count; i++)
            {
                Debug.Log("FIXING POSITION");
                activeBodyParts[i].transform.position = Vector3.Lerp(invisiTransform[i].transform.position, bodyTransforms[i].transform.position, t);
                activeBodyParts[i].transform.rotation = Quaternion.Slerp(invisiTransform[i].transform.rotation, bodyTransforms[i].transform.rotation, t);
            }
            yield return null;
                
        }
        if (t > 1)
        {
            StopCoroutine(ReturnTransform(activeBodyParts, invisiTransform, bodyTransforms));
        }

        //for (int i = 0; i < activeBodyParts.Count; i++)
        //{
        //    Debug.Log("FIXING POSITION");
        //    activeBodyParts[i].transform.position = Vector3.Lerp(activeBodyParts[i].transform.position, bodyTransforms[i].transform.position, Time.deltaTime * .000000000000001f);
        //    activeBodyParts[i].transform.rotation = Quaternion.Slerp(activeBodyParts[i].transform.rotation, bodyTransforms[i].transform.rotation, Time.deltaTime * .000000000000001f);
        //    yield return null; 
        //}
    }
}
