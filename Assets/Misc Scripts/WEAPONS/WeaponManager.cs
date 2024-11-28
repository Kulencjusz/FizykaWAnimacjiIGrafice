using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] float fireRate;
    private float fireRateTimer;
    [SerializeField] bool semiAuto;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletsPerShot;
    AimStateManager aim;
    public float damage = 20f;

    [SerializeField] AudioClip gunShot;
    public AudioSource audioSource;
    public WeaponAmmo ammo;
    ActionStateManager action;
    WeaponRecoil recoil;

    public Transform leftHandTarget;
    public Transform leftHandHint;

    Light shotPlash;
    ParticleSystem shotPS;
    float lightIntensity;
    [SerializeField] float lightReturnSpeed = 20;

    public float enemyKickBackForce = 100f;

    [SerializeField] private Sprite crosshair;
    [SerializeField] private Image pointerCrosshair;

    WeaponBloom bloom;
    WeaponClassManager weaponClass;

    private void Start()
    {
        bloom = GetComponent<WeaponBloom>();
        aim = GetComponentInParent<AimStateManager>();
        fireRateTimer = fireRate;
        action = GetComponentInParent<ActionStateManager>();
        shotPlash = GetComponentInChildren<Light>();
        lightIntensity = shotPlash.intensity;
        shotPlash.intensity = 0;
        shotPS = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (!UIManager.IsGamePaused && !HealthManager.isDead)
        {
            if (ShouldFire())
            {
                Fire();
            }
            shotPlash.intensity = Mathf.Lerp(shotPlash.intensity, 0, lightReturnSpeed * Time.deltaTime);
        }
        pointerCrosshair.sprite = crosshair;
    }

    private void OnEnable()
    {
        if(weaponClass == null)
        {
            weaponClass = GetComponentInParent<WeaponClassManager>();
            ammo = GetComponent<WeaponAmmo>();
            audioSource = GetComponent<AudioSource>();
            recoil = GetComponent<WeaponRecoil>();
            recoil.recoilFollowPos = weaponClass.recoilFollowPos;
        }
        weaponClass.SetCurrentWeapon(this);
    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if(fireRateTimer < fireRate)
        {
            return false;
        }
        if(ammo.currentAmmo == 0)
        {
            return false;
        }
        if(action.currentState == action.Reload)
        {
            return false;
        }
        if(action.currentState == action.Swap)
        {
            return false;
        }
        if(semiAuto && Input.GetKeyDown(KeyCode.Mouse0))
        {
            return true;
        }
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0))
        {
            return true;
        }
        return false;
    }

    void Fire()
    {
        fireRateTimer = 0;
        barrelPos.LookAt(aim.actualAimPos);
        barrelPos.localEulerAngles = bloom.BloomAngle(barrelPos);
        audioSource.PlayOneShot(gunShot);
        recoil.TriggerRecoil();
        TriggerFlash();
        ammo.currentAmmo--;
        for(int i=0;i<bulletsPerShot;i++)
        {
            GameObject goBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);

            Bullet weaponBullet = goBullet.GetComponent<Bullet>();
            weaponBullet.weapon = this;
            weaponBullet.dir = barrelPos.transform.forward;

            Rigidbody rb = goBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
    }

    void TriggerFlash()
    {
        shotPS.Play();
        shotPlash.intensity = lightIntensity;
    }
}
