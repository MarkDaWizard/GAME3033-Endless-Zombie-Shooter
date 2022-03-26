using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolderScript : MonoBehaviour
{
    [Header("WeaponToSpawn"), SerializeField]
    GameObject weaponToSpawn;

    public PlayerControllerScript playerController;
    Animator animator; 
    Sprite crosshairImage;
    WeaponComponent equippedWeapon;

    public readonly int isFiringHash = Animator.StringToHash("isFiring");
    public readonly int isReloadingHash = Animator.StringToHash("isReloading");

    bool wasFiring = false;
    bool firingPressed = false;

    [SerializeField]
    GameObject weaponSocket;
    [SerializeField]
    Transform gripIKSocket;
    
    void Start()
    {
        playerController = GetComponent<PlayerControllerScript>();
        animator = GetComponent<Animator>();
        GameObject spawnWeapon = Instantiate(weaponToSpawn, weaponSocket.transform.position, weaponSocket.transform.rotation, weaponSocket.transform);
        equippedWeapon = spawnWeapon.GetComponent<WeaponComponent>();
        equippedWeapon.Initialized(this);
        PlayerEvents.InvokeOnWeaponEquipped(equippedWeapon);
        gripIKSocket = equippedWeapon.gripLocation;
    }
    public void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, gripIKSocket.transform.position);
    }


    public void OnFire(InputValue value)
    {

        firingPressed = value.isPressed;
        if (firingPressed)
        {
            StartFiring();
        }
        else
            StopFiring();  
    }

    public void StartFiring()
    {
        if (equippedWeapon.weaponStats.bulletsInClip <= 0)
        {
            StartReloading();
            return;
        }
        animator.SetBool(isFiringHash, true);
        playerController.isFiring = true;
        equippedWeapon.StartFiringWeapon();
    }

    public void StopFiring()
    {
        playerController.isFiring = false;
        animator.SetBool(isFiringHash, false);
        equippedWeapon.StopFiringWeapon();
    }

    public void OnReload(InputValue value)
    {
        playerController.isReloading = value.isPressed;
       
        StartReloading();
    }

    public void StartReloading()
    {
        if (equippedWeapon.isReloading || equippedWeapon.weaponStats.bulletsInClip == equippedWeapon.weaponStats.clipSize) return;
        if (playerController.isFiring)
        {
            StopFiring();
        }
        

        animator.SetBool(isReloadingHash, true);
        equippedWeapon.StartReloading();
        
        InvokeRepeating(nameof(StopReloading), 0, 0.1f);
    }

    public void StopReloading()
    {
        if (animator.GetBool(isReloadingHash)) return;

        playerController.isReloading = false;
        equippedWeapon.StopReloading();
        print("cancel reload");
        animator.SetBool(isReloadingHash, false);
        CancelInvoke(nameof(StopReloading));
    }
}
