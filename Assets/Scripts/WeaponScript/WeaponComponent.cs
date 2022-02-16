using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    None,Pistol,MachineGun
}

public enum WeaponFirePattern
{
    SemiAuto, FullAuto, ThreeShotBurst, FiveShotBurst, PumpAction
}

[System.Serializable]
public struct WeaponStats
{
    public WeaponType weaponType;
    public WeaponFirePattern firePatter;
    public string weaponName;
    public float damage;
    public int bulletInClip;
    public int clipSize;
    public float fireStartDelay;
    public float fireRate;
    public float fireDistance;
    public bool repeating;
    public int totalBullets;
    

    public LayerMask weaponHitLayer;
}
public class WeaponComponent : MonoBehaviour
{
    public Transform gripLocation;
    public WeaponStats weaponStats;
    protected WeaponHolder weaponHolder;
    
    public bool isFiring;
    public bool isReloading;
    
    [SerializeField] protected ParticleSystem firingEffect;
    
    protected Camera mainCamera;
    // Start is called before the first frame update

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    
    public void Initialize(WeaponHolder _weaponHolder)
    {
        weaponHolder = _weaponHolder;
    }
    public virtual void StartFiringWeapon()
    {
        isFiring = true;
        if (weaponStats.repeating)
        {
            //fire weapon
            CancelInvoke(nameof(FireWeapon));
            InvokeRepeating(nameof(FireWeapon), weaponStats.fireStartDelay,weaponStats.fireRate);
        }
        else
        {
            FireWeapon();
        }
    }

    public virtual void StopFiringWeapon()
    {
        isFiring = false;
        CancelInvoke(nameof(FireWeapon));
        if (firingEffect&&firingEffect.isPlaying)
        {
            firingEffect.Stop();
        }
        
    }

    protected virtual void FireWeapon()
    {
        weaponStats.bulletInClip--;
        Debug.Log("firing!"+ weaponStats.bulletInClip);
    }

    public virtual void StartReloading()
    {
        isReloading = true;
        ReloadWeapon();
    }
    
    public virtual void StopReloading()
    {
        isReloading = false;
    }

    //set ammo counts here
    protected virtual void ReloadWeapon()
    {
        //check tot see if there's firing effect and stop it
        if (firingEffect&& firingEffect.isPlaying)
        {
            firingEffect.Stop();
        }
        
        int bulletToReload = weaponStats.clipSize - weaponStats.totalBullets;
        if (bulletToReload<0)
        {
            weaponStats.bulletInClip = weaponStats.clipSize;
            weaponStats.totalBullets -= weaponStats.clipSize;
        }
        else
        {
            weaponStats.bulletInClip = weaponStats.totalBullets;
            weaponStats.totalBullets = 0;
        }
    }
}
