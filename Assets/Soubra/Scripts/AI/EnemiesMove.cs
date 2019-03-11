using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMove : BaseState {

    
    public float speed = 5;
    float WPRadius = 1;
    float RotationSpeed;



    int current = 0;

     bool isIdle;

    



    public override void UpdateState(Enemy  EnimeAI)
    {
        base.UpdateState(EnimeAI);

        Debug.Log("Move");

        if (Vector3.Distance(EnimeAI.transform.position, EnimeAI.Waypoints[current].transform.position) < WPRadius)
        {
            Debug.Log("Move");
            current++;
            EnimeAI.StartCoroutine(IdleWhenWayPointReached());
            if (current >= EnimeAI.Waypoints.Length)
            {
                current = 0;
            }

         

        }
        if (!isIdle)
        {
            Debug.Log("IM AI AND IM MOVING");
            Move(EnimeAI);

        }



        Chases(EnimeAI);

    }








    private IEnumerator IdleWhenWayPointReached()
    {
        // Random Amount of time

      
        float randomTime = Random.Range(5,7);
        Debug.Log(" STOP MOVING");


        isIdle = true;

        yield return new WaitForSeconds(randomTime);

        isIdle = false;
        Debug.Log(" Resume MOVING");

    }


    

    void Move(Enemy EnimeAI)
    {
        EnimeAI.transform.position = Vector3.MoveTowards(EnimeAI.transform.position, EnimeAI.Waypoints[current].transform.position, Time.deltaTime * speed);
        EnimeAI.transform.rotation = Quaternion.Slerp(EnimeAI.transform.rotation, Quaternion.LookRotation(EnimeAI.Waypoints[current].transform.position - EnimeAI.transform.position), 0.1f);
        //EnimeAI.transform.position = new Vector3(EnimeAI.transform.position.x, 1.2f, EnimeAI.transform.position.z);
    }





    public void Chases(Enemy EnimeAI)
    {

        Vector3 direction = EnimeAI.playerTransform.position - EnimeAI.transform.position;
        float angle = Vector3.Angle(direction, EnimeAI.transform.forward);

        if (Vector3.Distance(EnimeAI.playerTransform.position, EnimeAI.transform.position) < 20f && angle < 100)
        {

            EnimeAI.CurrentState = new Chase();

            Debug.Log(" I am now chasing");
        }

    }

}

