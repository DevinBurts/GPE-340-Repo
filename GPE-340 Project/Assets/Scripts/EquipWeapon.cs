using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipWeapon : MonoBehaviour
{
    private GameObject player;
    public Weapon weapon;
    public GameObject weaponToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            player = col.gameObject;
            EquipGun(weaponToSpawn);
        }
    }
    public void EquipGun(GameObject weaponType)
    {
        // Create a weapon 
        GameObject gun = Instantiate(weaponType, player.GetComponent<PlayerPawn>().attachPoint.position, Quaternion.identity) as GameObject;
        if (gun.tag == "Rifle")
        {
            weapon = gun.GetComponent<Rifle>();
        }
        else if (gun.tag == "RocketLauncher")
        {
            weapon = gun.GetComponent<RocketLauncher>();

        }
        // Make the gun a child of the owner
        gun.transform.SetParent(player.GetComponent<PlayerPawn>().attachPoint, false);
        gun.transform.localPosition = weaponType.transform.localPosition;
        gun.transform.localRotation = weaponType.transform.localRotation;
        // Let Player Pawn know the player is using a weapon
        player.GetComponent<PlayerPawn>().weaponEquipped = true;
        player.GetComponent<PlayerPawn>().gun = gun;
        player.GetComponent<PlayerPawn>().weapon = weapon;
    }
}
