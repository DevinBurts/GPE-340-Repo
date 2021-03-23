using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon
{
    public GameObject rocketLauncher;
    // Start is called before the first frame update
    public override void Start()
    {
        shoot = GetComponent<Shoot>();
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }
    public override void AttackStart()
    {
        // Instantiate a rocket
        shoot.Fire(projectile, firePoint, damage, projectileSpeed, lifeSpan);
        base.AttackStart();
    }
    public override void AttackEnd()
    {

        base.AttackEnd();
    }
}
