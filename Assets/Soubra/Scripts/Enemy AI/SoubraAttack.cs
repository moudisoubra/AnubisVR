using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoubraAttack : NodeSoubra
{

  
    RaycastHit hit;

    public override Result Execute(BehaviourTreeSoubra BTS)
    {
        if (BTS.fail)
        {
            Debug.Log("Attack fail");
            return Result.failure;
        }
        Debug.Log("Attack success");
        return Result.success;

/*
        Vector3 direction = BTS.selfObject.transform.position - transform.position;
        float angle = Vector3.Angle(direction, BTS.transform.forward);


        if (Physics.Raycast(BTS.eyes.position, BTS.eyes.transform.forward, out hit, BTS.RangeRayCAST) && hit.collider.CompareTag("Player"))
        {
            Debug.Log("HITCOLID");
           
            // RemoveHealth
            {
                //HEALTH  DEDUCT 
                //hit.transform.GetComponent<Health>().RemoveHealth(Damage);


            }
        }

    */

    }
}
