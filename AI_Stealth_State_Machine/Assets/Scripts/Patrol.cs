using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State {
    private float time=3.0f;
    public override void Enter(AIController aiController) {
        //takes the current position of the AI and uses it to find the nearest patrol point
        int newX = Mathf.RoundToInt(aiController.aiObject.transform.position.x/12);
        newX = newX * 12 - 1;

        int newZ = Mathf.RoundToInt(aiController.aiObject.transform.position.z / 12);
        newZ = newZ * 12 - 7;

        Vector2 newGoal = new Vector2(newX, newZ);

        aiController.ResumePatrol(newGoal);
        
    }

    public override void Execute(AIController aiController) {
        RaycastHit hit;
       
        
        if (Physics.Raycast(aiController.aiObject.transform.position, aiController.aiObject.transform.TransformDirection(Vector3.forward), out hit, 20)) {
            if (hit.collider.gameObject.CompareTag("Player")) {
                //AI can see player and begins to chase
                aiController.playerLastPosition = hit.point;
                aiController.SetState(aiController.chaseState, 1);
            } 
        }
        

        if(Vector3.Distance(aiController.aiObject.transform.position, aiController.goal.position)<1.5f) {
            //once it is close to the patrol point the goal is updated
            aiController.NewRandomGoal();
        }



    }

    public override void Exit(AIController aiController) {
    }
}
