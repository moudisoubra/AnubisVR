using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceToObject : MonoBehaviour
{

    public Rigidbody rb;
    public int clickForce = 500;
    private Plane plane = new Plane(Vector3.up, Vector3.zero);
    public LayerMask enemyLayer;
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, enemyLayer);
            if (hit)
            {
                    Debug.Log("It's working!" + hitInfo.transform.gameObject.name);
            }
            else
            {
                Debug.Log("No Hit");
            }
        }
    }
}
