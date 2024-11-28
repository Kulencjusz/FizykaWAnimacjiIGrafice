using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {

    }

    public override void UpdateState(MovementStateManager movementStateManager)
    {
        if(movementStateManager.dir.magnitude > 0.1f) 
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                movementStateManager.SwitchState(movementStateManager.Run);
            }
            else
            {
                movementStateManager.SwitchState(movementStateManager.Walk);
            }
            if(Input.GetKey(KeyCode.C))
            {
                movementStateManager.SwitchState(movementStateManager.Crounch);
            }
        }
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            movementStateManager.previousState = this;
            movementStateManager.SwitchState(movementStateManager.Jump);
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            movementStateManager.previousState = this;
            movementStateManager.SwitchState(movementStateManager.Roll);
        }
    }
}
