using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipWeapon : MonoBehaviour
{
    private GameObject owner;
    public Weapon weapon;
    public GameObject weaponToSpawn;
    private PlayerPawn playerPawn;
    private EnemyPawn enemyPawn;
    public AudioClip audio;
    public float vol;
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
        owner = col.gameObject;
        EquipGun(weaponToSpawn);
    }
    public void EquipGun(GameObject weaponType)
    {
        // play the designated audioclip
        AudioSource.PlayClipAtPoint(audio, transform.position, vol);
        // Create a weapon 
        if (owner.tag == "Player")
        {
            playerPawn = owner.GetComponent<PlayerPawn>();
            if (playerPawn.weaponEquipped == true)
            {
                // do nothing if the player already has a weapon
                return;
            }
            GameObject gun = Instantiate(weaponType, playerPawn.attachPoint.position, Quaternion.identity) as GameObject;
            if (gun.tag == "Rifle")
            {
                weapon = gun.GetComponent<Rifle>();
            }
            else if (gun.tag == "RocketLauncher")
            {
                weapon = gun.GetComponent<RocketLauncher>();

            }
            // Make the gun a child of the owner
            gun.transform.SetParent(playerPawn.attachPoint, false);
            gun.transform.localPosition = weaponType.transform.localPosition;
            gun.transform.localRotation = weaponType.transform.localRotation;
            // Let Player Pawn know the player is using a weapon
            playerPawn.weaponEquipped = true;
            playerPawn.gun = gun;
            playerPawn.weapon = weapon;
        }
        else if(owner.tag == "Enemy")
        {
            if (enemyPawn.weaponEquipped == true)
            {
                // do nothing if the enemy already has a weapon
                return;
            }
            enemyPawn = owner.GetComponent<EnemyPawn>();
            GameObject gun = Instantiate(weaponType, enemyPawn.attachPoint.position, Quaternion.identity) as GameObject;
            if (gun.tag == "Rifle")
            {
                weapon = gun.GetComponent<Rifle>();
            }
            else if (gun.tag == "RocketLauncher")
            {
                weapon = gun.GetComponent<RocketLauncher>();

            }
            // Make the gun a child of the owner
            gun.transform.SetParent(enemyPawn.attachPoint, false);
            gun.transform.localPosition = weaponType.transform.localPosition;
            gun.transform.localRotation = weaponType.transform.localRotation;
            // Let Player Pawn know the player is using a weapon
            enemyPawn.weaponEquipped = true;
            enemyPawn.gun = gun;
            enemyPawn.weapon = weapon;
        }
    }
}
