using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPawn : Pawn
{
    // Start is called before the first frame update
    void Start()
    {
        // Gain access to the animation controller
        characterAnimator = GetComponent<Animator>();
        characterAnimator.SetBool("Alive", true);
        currentHealth = maxHealth;
        respawner = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            // access the empty gameobject to respawn the killed character
            respawner.GetComponent<RespawnCharacter>().Die(this.gameObject);
        }
    }
}
