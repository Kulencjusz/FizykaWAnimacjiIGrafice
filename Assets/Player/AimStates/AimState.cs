using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : AimBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        aim.anim.SetBool("isAiming", true);
        if(aim.actionManager.currentWeapon.name == "SciFiSniper")
        {
            aim.OnSniperScope();
            aim.currentFov = aim.sniperAdsFov;
            aim.uiManager.DisableCrosshair();
        }
        else
        {
            aim.currentFov = aim.adsFov;
        }
    }

    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            aim.OffSniperScope();
            aim.uiManager.EnableCrosshair();
            aim.SwitchState(aim.Hip);
        }
    }
}
