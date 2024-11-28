using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {
        movementStateManager.anim.SetBool("isRunning", true);
        movementStateManager.isRunning = true;
    }

    public override void UpdateState(MovementStateManager movementStateManager)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ExitState(movementStateManager, movementStateManager.Walk);
        }
        else if (Input.GetKey(KeyCode.C))
        {
            ExitState(movementStateManager, movementStateManager.Crounch);
        }
        else if (movementStateManager.dir.magnitude < 0.1f)
        {
            ExitState(movementStateManager, movementStateManager.Idle);
        }
        if (movementStateManager.vrInput < 0)
        {
            movementStateManager.moveSpeed = 5f;
        }
        else
        {
            movementStateManager.moveSpeed = 6f;
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
        movementStateManager.anim.SetBool("isRunning", false);
        movementStateManager.isRunning = false;
        movementStateManager.SwitchState(movementBaseState);
    }
}
