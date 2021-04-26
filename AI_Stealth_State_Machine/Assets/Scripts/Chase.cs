using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State {
    public override void Enter(AIController aiController) {
    }

    public override void Execute(AIController aiController) {
        RaycastHit hit;
        
        aiController.aiObject.transform.LookAt(aiController.playerObject.transform.position);//point towards player then checks whether it can see them

        if (Physics.Raycast(aiController.aiObject.transform.position, aiController.aiObject.transform.TransformDirection(Vector3.forward), out hit, 20)) {
            if (hit.collider.gameObject.CompareTag("Player")) {
                //sees the player so chase resumes
                aiController.playerLastPosition=hit.point;
                aiController.ChasePlayer(hit.point);

            } else {
                //can no longer see the player so state changes
                aiController.SetState(aiController.getToPlayerLastPosition, 2);
            }
        } else {
            aiController.SetState(aiController.getToPlayerLastPosition, 2);
        }
    }

    public override void Exit(AIController aiController) {
    }
}
