using UnityEngine;

public class Chase : BaseState {




    public float speed = 10;
   

        public override void UpdateState(Enemy EnimeAI)
        {
            base.UpdateState(EnimeAI);


            Vector3 direction = EnimeAI.playerTransform.position - EnimeAI.transform.position;
            float angle = Vector3.Angle(direction, EnimeAI.transform.forward);

            if (Vector3.Distance(EnimeAI.playerTransform.position, EnimeAI.transform.position) < 20f && angle < 100)
            {

                direction.y = 0;

             EnimeAI.transform.rotation = Quaternion.Slerp(EnimeAI.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

        
                if (direction.magnitude > 3)
                {
                    EnimeAI.transform.Translate(0, 0, 0.5f * Time.deltaTime *speed);

                }



        }





        Patrol(EnimeAI);

        Attack(EnimeAI);

    }


    public void Patrol(Enemy EnimeAI) 
    {
        if (Vector3.Distance(EnimeAI.playerTransform.position, EnimeAI.transform.position) > 20f)
        {
            EnimeAI.CurrentState = new EnemiesMove();

            Debug.Log(" BackToPatrol");

        }
    }


    public void Attack(Enemy EnimeAI)
    {

        Vector3 direction = EnimeAI.playerTransform.position - EnimeAI.transform.position;
        float angle = Vector3.Angle(direction, EnimeAI.transform.forward);
        if (Vector3.Distance(EnimeAI.playerTransform.position, EnimeAI.transform.position) < 10f  && angle <80)
        {
            EnimeAI.CurrentState = new Attack();

            Debug.Log(" Go To attack");

        }
    }




}

    
    
     

