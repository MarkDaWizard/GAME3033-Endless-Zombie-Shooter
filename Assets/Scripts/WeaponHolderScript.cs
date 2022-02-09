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

    public readonly int isFiringHash = Animator.StringToHash("isFiring");
    public readonly int isReloadingHash = Animator.StringToHash("isReloading");

    bool wasFiring = false;
    bool firingPressed = false;

    [SerializeField]
    GameObject weaponSocket;
    [SerializeField]
    Transform gripIKSocket;
    WeaponComponent equippedWeapon;
    void Start()
    {
        
        animator = GetComponent<Animator>();
        GameObject spawnWeapon = Instantiate(weaponToSpawn, weaponSocket.transform.position, weaponSocket.transform.rotation, weaponSocket.transform);
        equippedWeapon = spawnWeapon.GetComponent<WeaponComponent>();
        equippedWeapon.Initialized(this);
        gripIKSocket = equippedWeapon.gripLocation;
    }
    public void Awake()
    {
        playerController = GetComponent<PlayerControllerScript>();
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
        if (equippedWeapon.weaponStats.bulletsInClip <= 0) return;
        animator.SetBool(isFiringHash, true);
        playerController.isFiring = true;
        equippedWeapon.StartFiring();
    }

    public void StopFiring()
    {
        animator.SetBool(isFiringHash, false);
        playerController.isFiring = false;
        equippedWeapon.StopFiring();
    }

    public void OnReload(InputValue value)
    {
        playerController.isReloading = value.isPressed;
        animator.SetBool(isReloadingHash, playerController.isReloading);
    }

    public void StartReloading()
    {
        
    }
}
