using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponent : MonoBehaviour
{
    public Transform gripLocation;
    public Camera mainCamera;
    public enum WeaponType {None, Pistol, Rifle }
    public enum WeaponFiringPattern {Single, Burst, Auto }

    [System.Serializable]
    public struct WeaponStats
    {
        WeaponType weaponType;
        WeaponFiringPattern firingMode;
        public string weaponName;
        public float damage;
        public int bulletsInClip;
        public int clipSize;
        public float fireStartDelay;
        public float fireRate;
        public float fireDistance;
        public bool isAutomatic;
        public LayerMask weaponHitLayer;
    }

    protected WeaponHolderScript weaponHolder;

    [SerializeField]
    public WeaponStats weaponStats;

    public bool isFiring = false;
    public bool isReloading = false;
    // Start is called before the first frame update 
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialized(WeaponHolderScript _weaponHolder)
    {
        weaponHolder = _weaponHolder;
    }

    public virtual void StartFiring()
    {
        isFiring = true;
        if(weaponStats.isAutomatic)
        {
            InvokeRepeating(nameof(FireWeapon), weaponStats.fireStartDelay, weaponStats.fireRate); 
        }
        else
        {
            FireWeapon();
        }
    }

    public virtual void StopFiring()
    {
        isFiring = false;
        CancelInvoke(nameof(FireWeapon));
    }

    protected virtual void FireWeapon()
    {
        print("firing weapon");
        weaponStats.bulletsInClip--;
    }
}
