using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : BaseState {

    public float speed = 10;
    public float Damage = 15f;

    public override void UpdateState(Enemy EnimeAI)
    {
        base.UpdateState(EnimeAI);

        RaycastHit hit;

        //Debug.DrawRay(EnimeAI.eye.position, EnimeAI.eye.forward.normalized * EnimeAI.weaponRange, Color.red);
        Vector3 direction = EnimeAI.playerTransform.position - EnimeAI.transform.position;
        float angle = Vector3.Angle(direction, EnimeAI.transform.forward);


        if (Physics.Raycast(EnimeAI.eye.position, EnimeAI.eye.transform.forward, out hit, EnimeAI.weaponRange) && hit.collider.CompareTag("Player"))
        {
            Debug.Log("HITCOLID");
            //if (hit.transform.GetComponent<PlayerController>())
            {
                
               // hit.transform.GetComponent<HealthGUI>().RemoveHealth(Damage);

                
            }
        }
        else chases(EnimeAI);     
    }


    public void chases(Enemy EnimeAI)
    {

        Vector3 direction = EnimeAI.playerTransform.position - EnimeAI.transform.position;
        float angle = Vector3.Angle(direction, EnimeAI.transform.forward);

        if (Vector3.Distance(EnimeAI.playerTransform.position, EnimeAI.transform.position) < 20f && angle < 100)
        {

            EnimeAI.CurrentState = new Chase();

            Debug.Log(" Back to chase");
        }

    }

}