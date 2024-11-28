using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrounchState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {
        movementStateManager.anim.SetBool("isCrounching", true);
    }

    public override void UpdateState(MovementStateManager movementStateManager)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(movementStateManager, movementStateManager.Run);
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            if (movementStateManager.dir.magnitude < 0.1f)
            {
                ExitState(movementStateManager, movementStateManager.Idle);
            }
            else
            {
                ExitState(movementStateManager, movementStateManager.Walk);
            }
        }
        if (movementStateManager.vrInput < 0)
        {
            movementStateManager.moveSpeed = 1f;
        }
        else
        {
            movementStateManager.moveSpeed = 2f;
        }

    }

    void ExitState(MovementStateManager movementStateManager, MovementBaseState movementBaseState)
    {
        movementStateManager.anim.SetBool("isCrounching", false);
        movementStateManager.SwitchState(movementBaseState);
    }
}

