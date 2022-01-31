using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolderScript : MonoBehaviour
{
    [Header("WeaponToSpawn"), SerializeField]
    GameObject weaponToSpawn;

    PlayerControllerScript playerController;
    Sprite crosshairImage;

    [SerializeField]
    GameObject weaponSocket;

    void Start()
    {
        GameObject spawnWeapon = Instantiate(weaponToSpawn, weaponSocket.transform.position, weaponSocket.transform.rotation, weaponSocket.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
