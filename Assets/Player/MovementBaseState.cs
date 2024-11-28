using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementBaseState
{
    public abstract void EnterState(MovementStateManager movementStateManager);
    public abstract void UpdateState(MovementStateManager movementStateManager);
}
