using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    public GameObject rifle;
    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override void AttackStart()
    {
        shoot = GetComponent<Shoot>();
        if (fullAuto == true)
        {
            // Give the bullets a small delay to avoid having them spawn ontop of each other
            shoot.timeReset = 0.2f;
        }
        else
        {
            shoot.timeReset = timeReset;
        }
        // Instantiate a bullet
        shoot.Fire(projectile, firePoint, damage, projectileSpeed, lifeSpan);
        base.AttackStart();
    }
    public override void AttackEnd()
    {

        base.AttackEnd();
    }
}
