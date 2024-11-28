using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {
        movementStateManager.anim.SetBool("isWalking", true);
    }

    public override void UpdateState(MovementStateManager movementStateManager)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(movementStateManager, movementStateManager.Run);
        }
        else if(Input.GetKey(KeyCode.C)) 
        {
            ExitState(movementStateManager, movementStateManager.Crounch);
        }
        else if(movementStateManager.dir.magnitude < 0.1f)
        {
            ExitState(movementStateManager, movementStateManager.Idle);
        }
        if(movementStateManager.vrInput < 0)
        {
            movementStateManager.moveSpeed = 2f;
        }
        else
        {
            movementStateManager.moveSpeed = 3f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movementStateManager.previousState = this;
            ExitState(movementStateManager, movementStateManager.Jump);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            movementStateManager.previousState = this;
            ExitState(movementStateManager, movementStateManager.Roll);
        }
    }

    void ExitState(MovementStateManager movementStateManager, MovementBaseState movementBaseState)
    {
        movementStateManager.anim.SetBool("isWalking", false);
        movementStateManager.SwitchState(movementBaseState);
    }
}
