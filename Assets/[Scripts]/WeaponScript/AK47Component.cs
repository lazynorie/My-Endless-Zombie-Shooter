using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47Component : WeaponComponent
{
    Vector3 hitLocation;
    protected override void FireWeapon()
    {
        if (weaponStats.bulletInClip > 0 && !isReloading) 
        {
            base.FireWeapon();
            if (firingEffect)
            {
                firingEffect.Play();
            }
            //Ray screenRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            Ray screenRay = mainCamera.ViewportPointToRay(new Vector2(.5f,.5f));
            if (Physics.Raycast(screenRay,out RaycastHit hit, weaponStats.fireDistance,weaponStats.weaponHitLayer))
            {
                hitLocation = hit.point;
                DealDamage(hit);
                Vector3 hitDirection = hit.point - mainCamera.transform.position;
                Debug.DrawRay(mainCamera.transform.position, hitDirection.normalized * weaponStats.fireDistance, Color.red,1);
            }
            
        }
        else if (weaponStats.bulletInClip <= 0)
        {
            //we can trigger a reload here
            weaponHolder.StartReload(); 
        }

       
    }
    
    void DealDamage(RaycastHit hitInfo)
    {
        IDamagable damagable = hitInfo.collider.GetComponent<IDamagable>();
        damagable?.TakeDamage(weaponStats.damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitLocation, 0.1f);
    }
}
