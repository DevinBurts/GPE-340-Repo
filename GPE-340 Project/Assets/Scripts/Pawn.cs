using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [Header("Weapons")]
    public Weapon weapon;
    public GameObject gun;
    [Header("Data")]
    public int maxHealth;
    public float currentHealth;
    public bool weaponEquipped;
    public Transform attachPoint;
    [Header("Components")]
    public Animator characterAnimator;

    public GameObject respawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public enum WeaponAnimationType
    {
        // allow the character to swap between handplacements based on the equipped weapon
        None = 0,
        Rifle = 1,
        RocketLauncher = 2
    }
    public void UnequipWeapon()
    {
        // unequipped the current weapon if there is one
        if (weaponEquipped == true)
        {
            Destroy(weapon);
            Destroy(gun);
            weapon = null;
            gun = null;
            weaponEquipped = false;
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            // access the empty gameobject to respawn the killed character
            respawner.GetComponent<RespawnCharacter>().Die(this.gameObject);
        }
    }
}