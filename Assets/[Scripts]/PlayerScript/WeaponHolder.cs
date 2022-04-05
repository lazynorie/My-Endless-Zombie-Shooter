using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class WeaponHolder : MonoBehaviour
{
    [Header("WeaponToSpawn"), SerializeField]
    private GameObject weaponToSpawn;

    public PlayerController playerController;
    private Animator playeranimator;

    private WeaponComponent equippedWeapon;
    
    public readonly int isFiringHash = Animator.StringToHash("isFiring");
    public readonly int isReloadHash = Animator.StringToHash("isReloading");

    public Sprite crossHairImage;

    [SerializeField] private GameObject weaponSocketLocation;
    [SerializeField] private Transform GripSocketLocation;


    private bool wasFiring = false;
    private bool firingPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        GameObject spawnedWeapon = Instantiate(weaponToSpawn, weaponSocketLocation.transform.position,
            weaponSocketLocation.transform.rotation, weaponSocketLocation.transform);
        playeranimator = GetComponent<Animator>();

        equippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();
        GripSocketLocation = equippedWeapon.gripLocation;
        
        equippedWeapon.Initialize(this);
        
        PlayerEvents.InvokeOnWeaponEquipped(equippedWeapon);
    }

    // Update is called once per frame
    private void OnAnimatorIK(int layerIndex)
    {
        
        if (!playerController.isReloading)
        {
            playeranimator.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
            playeranimator.SetIKPosition(AvatarIKGoal.LeftHand,GripSocketLocation.transform.position);
        }
        
    }
    
    public void OnFire(InputValue value)
    {
        firingPressed = value.isPressed;
        //playerController.isFiring = value.isPressed;
        if (firingPressed)
        {
            StartFiring();
        }
        else
        {
            StopFiring();
        }
        //set up firing animation
    }

    void StartFiring()
    {
        if (equippedWeapon.weaponStats.bulletInClip <= 0)
        {
            StartReload();
            return;
        }
        //playeranimator.SetBool(isFiringHash,playerController.isFiring);
        playeranimator.SetBool(isFiringHash, true);
        playerController.isFiring = true;
        equippedWeapon.StartFiringWeapon();
    }

    void StopFiring()
    {
        playeranimator.SetBool(isFiringHash, false);
        playerController.isFiring = false;
        equippedWeapon.StopFiringWeapon();
    }
    
    public void OnReload(InputValue value)
    {
        playerController.isReloading = value.isPressed;
        StartReload();
    }
    
    public void StartReload()
    {
        if (playerController.isFiring)
        {
            StopFiring();
        }
        //don't reload if there's not enough bullets
        if (equippedWeapon.weaponStats.totalBullets <=0)
        {
            return;
        }
        //refill ammo
        equippedWeapon.StartReloading();
        playerController.isReloading = true;
        playeranimator.SetBool(isReloadHash,true);
        //playeranimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
        
        InvokeRepeating(nameof(StopReloading),0,0.1f);
    }

    public void StopReloading()
    {
        if (playeranimator.GetBool(isReloadHash)) return;

        playerController.isReloading = false;
        playeranimator.SetBool(isReloadHash,false);
        equippedWeapon.StopReloading();
        CancelInvoke(nameof(StopReloading));

        if (firingPressed)
        {
            StartFiring();
        }
    }
}
