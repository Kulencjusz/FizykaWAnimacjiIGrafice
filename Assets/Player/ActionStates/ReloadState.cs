using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : ActionBaseState
{
    public override void EnterState(ActionStateManager action)
    {
        action.ReloadSound();
        action.ReloadWeapon();
    }

    public override void UpdateState(ActionStateManager action)
    {
        
    }
}
