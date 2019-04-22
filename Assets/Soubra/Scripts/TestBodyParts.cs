using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBodyParts : MonoBehaviour
{
    
    public GameObject bone1;
    public GameObject bone2;
    public GameObject ActualBody;
    public GameObject InvisiBody;
    public GameObject leftShoulder;
    public GameObject rightShoulder;
    public GameObject head;
    public static GameObject original;

    public List<GameObject> invisibleBody;
    public List<GameObject> actualBody;

    public List<Transform> actualBodyTransforms;
    public List<Transform> invisiBodyTransforms;

    public List<Transform> fullRigBody;
    public List<Transform> fullRigInvisi;

    public float timer;

    public List<Transform> lastPosition;
    public float timeBeforeBlend;
    public float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        AddAllTransforms(ActualBody.transform, fullRigBody);
        AddAllTransforms(InvisiBody.transform, fullRigInvisi);
        //StartCoroutine(ReturnTransform(new List<GameObject>(),new List<Transform>()));
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ClearAll();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            GetProperParent(bone1.transform, actualBody, actualBodyTransforms);
            GetProperParent(bone2.transform, invisibleBody, invisiBodyTransforms);

            //AddAllTransforms(bone1.transform, actualBodyTransforms);
            //AddAllTransforms(bone2.transform, invisiBodyTransforms);

        }

        if (timer < 1)
        {
            if (timer <= timeBeforeBlend)
            {
                for (int i = 0; i < actualBody.Count; i++)
                {
                    actualBody[i].gameObject.transform.position = invisibleBody[i].gameObject.transform.position;
                    actualBody[i].gameObject.transform.rotation = invisibleBody[i].gameObject.transform.rotation;
                }
            }
            else
            {
                StartCoroutine(ReturnTransform(actualBody, invisiBodyTransforms, actualBodyTransforms));
            }
        }
        else
        {
            for (int i = 0; i < fullRigBody.Count; i++)
            {
                fullRigInvisi[i].gameObject.transform.position = fullRigBody[i].gameObject.transform.position;
                fullRigInvisi[i].gameObject.transform.rotation = fullRigBody[i].gameObject.transform.rotation;
            }
        }
    }

    public void ClearAll()
    {
        timer = 0;
        t = 0;
        invisibleBody.Clear();
        actualBody.Clear();

        actualBodyTransforms.Clear();
        invisiBodyTransforms.Clear();
    }

    public void GetProperParent(Transform child, List<GameObject> bonesList, List<Transform> transformList)
    {
        AddAllChildren(FindParentWithTag(child).transform, bonesList);
        AddAllTransforms(FindParentWithTag(child).transform, transformList);
    }

    public static GameObject FindParentWithTag(Transform childObject)
    {
        Transform t = childObject.transform;
        while (t.parent != null)
        {
            if (t.parent.tag == "Root")
            {
                return t.parent.gameObject;
            }
            t = t.parent.transform;
        }
        return null; // Could not find a parent with given tag.
    }

    public void AddAllChildren(Transform parent, List<GameObject> bonesList)
    {
        if (parent != null)
        {
            foreach (Transform child in parent)
            {
                bonesList.Add(child.gameObject);
                AddAllChildren(child, bonesList);
            }
        }

    }

    public void AddAllTransforms(Transform parent, List<Transform> transformList)
    {
        foreach (Transform child in parent)
        {
            transformList.Add(child);
            AddAllTransforms(child, transformList);
        }
    }
    
    public IEnumerator ReturnTransform(List<GameObject> activeBodyParts, List<Transform> invisiTransform, List<Transform> bodyTransforms)
    {
        while (t <= 0.9)
        {
            t += .01f;

            for (int i = 0; i < 4; i++)
            {
                Debug.Log("FIXING POSITION");
                activeBodyParts[i].transform.position = Vector3.Lerp(invisiTransform[i].transform.position, bodyTransforms[i].transform.position, t);
                activeBodyParts[i].transform.rotation = Quaternion.Slerp(invisiTransform[i].transform.rotation, bodyTransforms[i].transform.rotation, t);
            }

            yield return null;
                
        }
        if (t >= 0.9)
        {
            StopCoroutine(ReturnTransform(activeBodyParts, invisiTransform, bodyTransforms));
        }
    }
}
