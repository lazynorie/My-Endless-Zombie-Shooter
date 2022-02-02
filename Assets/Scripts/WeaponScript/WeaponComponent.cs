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
   
    public LayerMask weaponHitLayer;
}
public class WeaponComponent : MonoBehaviour
{
    public Transform gripLocation;

    public WeaponStats weaponStats;

    protected WeaponHolder weaponHolder;
    public bool isFiring;

    public bool isReloading;

    protected Camera mainCamera;
    // Start is called before the first frame update

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    protected virtual void FireWeapon()
    {
        weaponStats.bulletInClip--;
        Debug.Log("firing!"+ weaponStats.bulletInClip);
    }
}
