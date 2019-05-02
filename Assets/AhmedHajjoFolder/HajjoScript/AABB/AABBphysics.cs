using System.Collections.Generic;
using UnityEngine;

public class AABBphysics : MonoBehaviour
{

    public static AABBphysics Istance;
  
    public List<ListOfAABB> aabb = new List<ListOfAABB>();


    private void Awake()
    {
        Istance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        intersect();
    }

    public void intersect()
    {
        for (int i = 0; i < aabb.Count; i++)
        {
            for (int j = 0; j < aabb.Count; j++)
            {
                if (i != j)
                {
                   if((aabb[i].Min.x <=aabb[j].Max.x && aabb[i].Max.x >= aabb[j].Min.x)
                    && (aabb[i].Min.y <= aabb[j].Max.y && aabb[i].Max.y >= aabb[j].Min.y) 
                    && (aabb[i].Min.z <= aabb[j].Max.z && aabb[i].Max.z >= aabb[j].Min.z))
                    {
                        aabb[i].transform.position -= aabb[i].vel * 10f;
                        aabb[j].transform.position -= aabb[j].vel * 10f;
                        aabb[i].vel *= -1;
                        aabb[j].vel *= -1;
                        Debug.Log("Col");
                    }   
                }
            }
        }
    }

}

