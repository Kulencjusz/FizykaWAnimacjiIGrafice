using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ActionStateManager : MonoBehaviour
{
    public ActionBaseState currentState;
    public ReloadState Reload = new ReloadState();
    public DefaultState Default = new DefaultState();
    public SwapState Swap = new SwapState();
    public WeaponManager currentWeapon;
    public WeaponAmmo ammo;
    public Animator anim;
    public MultiAimConstraint rHandAim;
    public TwoBoneIKConstraint LHandIK;
    AudioSource audioSource;
    [SerializeField] UIManager uiManager;

    private void Start()
    {
        SwitchState(Default);
        anim = GetComponent<Animator>();
    }

    public void SetWeapon(WeaponManager weapon)
    {
        currentWeapon = weapon;
        ammo = weapon.ammo;
        audioSource = weapon.audioSource;
    }

    private void Update()
    {
        if (!UIManager.IsGamePaused)
        {
            currentState.UpdateState(this);
            uiManager.ChangeAmmo(currentWeapon.ammo.currentAmmo, currentWeapon.ammo.extraAmmo);
        }

    }

    public void SwitchState(ActionBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void ReloadWeapon()
    {
        ammo.Reload();
        SwitchState(Default);
    }

    public void ReloadSound()
    {
        audioSource.PlayOneShot(ammo.reloadSound);
    }
}
