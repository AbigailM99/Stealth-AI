using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetToPlayerLastPosition : State
{
    public override void Enter(AIController aiController) {
    }

    public override void Execute(AIController aiController) {
        //AI is moving towards player's last known position then will seek
        if (Vector3.Distance(aiController.aiObject.transform.position, aiController.playerLastPosition)<2) {
            aiController.SetState(aiController.seekState, 3);

        }
    }

    public override void Exit(AIController aiController) {
        
    }

    
}
