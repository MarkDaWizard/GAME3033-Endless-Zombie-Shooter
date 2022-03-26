using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponAmmoUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI weaponNameText;
    [SerializeField] TextMeshProUGUI currentBulletText;
    [SerializeField] TextMeshProUGUI totalBulletText;
    [SerializeField] WeaponComponent weaponComponent;
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        if (!weaponComponent)
            return;

        weaponNameText.text = weaponComponent.weaponStats.weaponName;
        currentBulletText.text = weaponComponent.weaponStats.bulletsInClip.ToString();
        totalBulletText.text = weaponComponent.weaponStats.totalBullets.ToString();

    }
}
