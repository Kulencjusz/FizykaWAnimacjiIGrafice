using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : ActionBaseState
{
    public float scrollDir;
    public override void EnterState(ActionStateManager action)
    {

    }

    public override void UpdateState(ActionStateManager action)
    {
        action.rHandAim.weight = Mathf.Lerp(action.rHandAim.weight, 1, 5 * Time.deltaTime);
        if(action.LHandIK.weight == 0 )
        {
            action.LHandIK.weight = 1;
        }

        if (Input.GetKeyDown(KeyCode.R) && CanReload(action))
        {
            action.SwitchState(action.Reload);
        }
        else if(Input.mouseScrollDelta.y != 0)
        {
            scrollDir = Input.mouseScrollDelta.y;
            action.SwitchState(action.Swap);
        }
    }

    bool CanReload(ActionStateManager action)
    {
        if(action.ammo.currentAmmo == action.ammo.clipSize)
        {
            return false;
        }
        else if(action.ammo.extraAmmo == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
