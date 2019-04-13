using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObjects : MonoBehaviour
{
    public List<GameObject> childObjects;
    public bool breakNow;
    public bool collided;

    // Start is called before the first frame update
    void Start()
    {
        AddAllChildren(this.gameObject.transform, childObjects);
    }

    // Update is called once per frame
    void Update()
    {
        if (breakNow && collided)
        {
            BreakApart(childObjects);
        }
    }

    public void AddAllChildren(Transform parent, List<GameObject> childObjects)
    {
        if (parent != null)
        {
            foreach (Transform child in parent)
            {
                childObjects.Add(child.gameObject);
                AddAllChildren(child, childObjects);
            }
        }

    }

    public void BreakApart(List<GameObject> childObjects)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        Destroy(this.gameObject.GetComponent<Rigidbody>());
        for (int i = 0; i < childObjects.Count; i++)
        {
            childObjects[i].GetComponent<MeshCollider>().enabled = true;
            childObjects[i].GetComponent<MeshCollider>().isTrigger = false;
            childObjects[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            childObjects[i].GetComponent<Rigidbody>().isKinematic = false;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            collided = true;
        }
    }
}
