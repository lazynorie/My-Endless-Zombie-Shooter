using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponAmmoUI : MonoBehaviour
{
    [SerializeField] private TMP_Text WeaponNameText;
    [SerializeField] private TMP_Text currentBulletCountText;
    [SerializeField] private TMP_Text TotalBulletCountText;

    [SerializeField] private WeaponComponent weaponComponent;

    /// <summary>
    /// set up events for onweaponequipped to handle the weaponcomponnent we grab
    /// </summary>
    private void OnEnable()
    {
        PlayerEvents.OnWeaponEquipped += OnWeaponEquipped;
    }

    private void OnDisable()
    {
        PlayerEvents.OnWeaponEquipped -= OnWeaponEquipped;
    }

    void OnWeaponEquipped(WeaponComponent _weaponComponent)
    {
        weaponComponent = _weaponComponent;
    }

    void Update()
    {
        if (!weaponComponent)
        {
            return;
        }

        WeaponNameText.text = weaponComponent.weaponStats.weaponName;
        currentBulletCountText.text = weaponComponent.weaponStats.bulletInClip.ToString();
        TotalBulletCountText.text = weaponComponent.weaponStats.totalBullets.ToString();
    }
}
