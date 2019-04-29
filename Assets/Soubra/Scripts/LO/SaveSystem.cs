using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public ObjectSaved objectSaved;
    public List<ObjectSaved> objectsSaves;
    public GameObject objectUsed;
    public GameObject[] objectsSaved;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            for (int i = 0; i < objectsSaved.Length; i++)
            {
                SaveScene(objectsSaved[i].gameObject);
            }
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            for (int i = 0; i < objectsSaved.Length; i++)
            {
                LoadScene(objectsSaved[i].gameObject);
            }
        }
    }

    [System.Serializable]
    public struct SavedPosition
    {
        public float x;
        public float y;
        public float z;

        public Vector3 GetPosition()
        {
            return new Vector3(x, y, z);
        }

        public void SetPosition(Vector3 position)
        {
            x = position.x;
            y = position.y;
            z = position.z;
        }
    }


    [System.Serializable]
    public class ObjectSaved
    {
        public SavedPosition objectPosition;

    }

    public void SaveScene(GameObject saveObject)
    {
        ObjectSaved saveFile = new ObjectSaved();
        saveFile.objectPosition.SetPosition(saveObject.transform.position);

        SoubraSaveLoad.Save(saveFile);

    }

    public void LoadScene(GameObject loadedObject)
    {
        SoubraSaveLoad.Load(this);
        loadedObject.transform.position = objectSaved.objectPosition.GetPosition();
    }
}
