using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunComponent : WeaponComponent
{
    protected override void FireWeapon()
    {
        int pellets = 6;
        Vector3 hitLocation;

        if (weaponStats.bulletInClip > 0 && !isReloading) 
        {
            base.FireWeapon();
            if (firingEffect)
            {
                firingEffect.Play();
            }
            //Ray screenRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            Ray screenRay = mainCamera.ViewportPointToRay(new Vector2(.5f,.5f));
            List<Ray> rays = new List<Ray>();
            for (int i = 0; i < pellets; i++)
            {
                Ray ray = mainCamera.ViewportPointToRay(new Vector2(.5f + Random.Range(-0.05f, 0.05f),
                    .5f + Random.Range(-0.05f, 0.05f)));
                rays.Add(ray);
            }
            foreach (var ray in rays)
            {
                if (Physics.Raycast(ray,out RaycastHit hit, weaponStats.fireDistance,weaponStats.weaponHitLayer))
                {
                    hitLocation = hit.point;
                    Vector3 hitDirection = hit.point - mainCamera.transform.position;
                    Debug.DrawRay(mainCamera.transform.position, hitDirection.normalized * weaponStats.fireDistance, Color.red,1);
                }
            }
           
            
        }
        else if (weaponStats.bulletInClip <= 0)
        {
            //we can trigger a reload here
            Debug.Log("Reload now!");
            weaponHolder.StartReload(); 
        }
    }

    public void bulletSpray()
    {
        
    }
}
