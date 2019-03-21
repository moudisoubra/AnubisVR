using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{

    public bool attack;
    public GameObject projectile;
    public GameObject target;
    public List<GameObject> projectileList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            projectileList.Add(Instantiate(projectile, transform.position, Quaternion.identity));
            
        }

        if (projectileList.Count > 0)
        {
            for (int i = 0; i < projectileList.Count; i++)
            {
                projectileList[i].gameObject.transform.position += Vector3.MoveTowards(transform.position, target.transform.position, 1 * Time.deltaTime);
            }
        }

    }
}
