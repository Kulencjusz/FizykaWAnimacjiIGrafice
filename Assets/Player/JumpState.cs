using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {
        if(movementStateManager.previousState == movementStateManager.Idle)
        {
            movementStateManager.anim.SetTrigger("IdleJump");
        }
        else if(movementStateManager.previousState == movementStateManager.Walk || movementStateManager.previousState == movementStateManager.Run)
        {
            movementStateManager.anim.SetTrigger("RunJump");
        }
    }

    public override void UpdateState(MovementStateManager movementStateManager)
    {
        if(movementStateManager.jumped && movementStateManager.IsGrounded())
        {
            movementStateManager.jumped = false;
            if(movementStateManager.hzInput == 0 && movementStateManager.vrInput == 0)
            {
                movementStateManager.SwitchState(movementStateManager.Idle);
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                movementStateManager.SwitchState(movementStateManager.Run);
            }
            else
            {
                movementStateManager.SwitchState(movementStateManager.Walk);
            }
        }
    }
}
