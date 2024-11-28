using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {
        movementStateManager.anim.SetTrigger("Dive");
    }

    public override void UpdateState(MovementStateManager movementStateManager)
    {
        if (movementStateManager.rolled && movementStateManager.IsGrounded())
        {
            movementStateManager.rolled = false;
            if (movementStateManager.hzInput == 0 && movementStateManager.vrInput == 0)
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
