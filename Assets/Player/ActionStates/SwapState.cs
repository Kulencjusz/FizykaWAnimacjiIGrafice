using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapState : ActionBaseState
{
    public override void EnterState(ActionStateManager action)
    {
        action.anim.SetTrigger("Swap");
        action.LHandIK.weight = 0f;
        action.rHandAim.weight = 0f;
    }

    public override void UpdateState(ActionStateManager action)
    {
        
    }
}
