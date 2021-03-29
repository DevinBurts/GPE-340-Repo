using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pawn : MonoBehaviour
{
    [Header("Weapons")]
    public Weapon weapon;
    public GameObject gun;
    [Header("Data")]
    public float moveSpeed;
    public float rotateSpeed;
    public int maxHealth;
    public float currentHealth;
    public bool weaponEquipped;
    public Transform attachPoint;
    public float timer;
    [Header("Components")]
    public Animator characterAnimator;
    public GameObject respawner;
    public GameObject GameManager;
    public GameManager gmCaller;
    [Header("Healthbar")]
    public Slider healthbar;
    public Text healthbarText;
    // Start is called before the first frame update
    public virtual void Start()
    {
        // Gain access to the animation controller
        characterAnimator = GetComponent<Animator>();
        currentHealth = maxHealth;
        characterAnimator.SetBool("Alive", true);
        respawner = transform.parent.gameObject;
        gmCaller = GameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // Calculate the percentage value of the character's current health
        healthbar.value = currentHealth / maxHealth;
    }

    public void UnequipWeapon()
    {
        // unequip the current weapon if there is one
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
    }
    public void Die()
    {
        // Ragdoll the character if it has a ragdoll component
        if (GetComponent<Ragdoll>() != null)
        {
            GetComponent<Ragdoll>().RagdollOn();
        }
        // Delete the character after it dies
        Destroy(gameObject, timer);
    }
}