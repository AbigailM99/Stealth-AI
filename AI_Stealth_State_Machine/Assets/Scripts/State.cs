using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract void Enter(AIController aiController);
    public abstract void Execute(AIController aiController);

    public abstract void Exit(AIController aiController);
}
