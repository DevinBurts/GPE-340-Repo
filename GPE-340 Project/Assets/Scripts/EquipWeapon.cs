using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipWeapon : MonoBehaviour
{
    private GameObject player;
    public Weapon weapon;
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
            EquipGun(gameObject);
        }
    }
    public void EquipGun(GameObject weaponType)
    {
        // create a weapon to give to the player
        GameObject gun = Instantiate(weaponType, player.GetComponent<PlayerPawn>().attachPoint.position, Quaternion.identity) as GameObject;
        if (gun.tag == "Rifle")
        {
            weapon = gun.GetComponent<Rifle>();
        }
        else if (gun.tag == "RocketLauncher")
        {
            weapon = gun.GetComponent<RocketLauncher>();

        }
        gun.transform.SetParent(player.GetComponent<PlayerPawn>().attachPoint);
        gun.transform.localPosition = weaponType.transform.localPosition;
        gun.transform.localRotation = weaponType.transform.localRotation;
        player.GetComponent<PlayerPawn>().weaponEquipped = true;
        player.GetComponent<PlayerPawn>().gun = gun;
        player.GetComponent<PlayerPawn>().weapon = weapon;
    }
}
