using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : State {
    float seekTime = 20f;
    Vector3 direction;
    public override void Enter(AIController aiController) {
        //resets seek time
        seekTime = 20f;
    }

    public override void Execute(AIController aiController) {
        //sets ai destination
        aiController.SeekGoal();


        RaycastHit hit;

        if (Physics.Raycast(aiController.aiObject.transform.position, aiController.aiObject.transform.TransformDirection(Vector3.forward), out hit, 20)) {
            if (hit.collider.gameObject.CompareTag("Player")) {
                //sees player so chase state begins
                aiController.playerLastPosition = hit.point;
                aiController.ChasePlayer(hit.point);
                aiController.SetState(aiController.chaseState, 1);
                return;
            } 
        } 

        if (seekTime < 0) {
            //unable to find player so resumes patrol
            aiController.SetState(aiController.patrolState, 0);
            seekTime = 20;
        } else {
            seekTime -= Time.deltaTime;
        }
    }

    public override void Exit(AIController aiController) {
    }
}
