using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPawn : Pawn
{
    public GameObject target;
    [SerializeField]
    private float distanceToTarget;
    [SerializeField]
    private float attackRange;
    // Start is called before the first frame update
    public override void Start()
    {
        // Gain access to the animation controller
        characterAnimator = GetComponent<Animator>();
        characterAnimator.SetBool("Alive", true);
        currentHealth = maxHealth;
        respawner = transform.parent.gameObject;
        target = GameObject.FindWithTag("Player");
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (currentHealth <= 0)
        {
            // Remove the enemy after it dies
            respawner.GetComponent<RespawnCharacter>().target = target;
            gmCaller.enemiesDefeated += 1;
            Die();
        }
        // Check if the target is in range before attacking
        if (CanAttack())
        {
            weapon.AttackStart();
        }
        base.Update();
    }
    public void Move(Vector3 direction)
    {
        // Switch from world space to local space
        direction = transform.InverseTransformDirection(direction);
        // Normalize movement vector
        direction.Normalize();
        characterAnimator.SetFloat("Vertical", direction.z * moveSpeed * Time.deltaTime);
        characterAnimator.SetFloat("Horizontal", direction.x * moveSpeed * Time.deltaTime);
    }

    private bool CanAttack()
    {
        // Find the distance to the target
        distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
        // Check if the target is in range and if a weapon is equipped
        if ((distanceToTarget <= attackRange) && weaponEquipped == true)
        {
            // The enemy can attack the player
            return true;
        }
        else
        {
            // The enemy cannor attack the player
            return false;
        }
    }

}
