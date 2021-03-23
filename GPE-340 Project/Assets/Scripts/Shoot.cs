using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float timeTillNextShot;
    public float timeReset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeTillNextShot -= Time.deltaTime;
    }
    public void Fire(GameObject projectile, Transform firePoint, int damage, float projectileSpeed, float lifeSpan)
    {
        // Create the projectile at the designated point
        GameObject projectileClone = Instantiate(projectile, firePoint.position, firePoint.rotation) as GameObject;
        // Check if the projectile has been destroyed
        if (projectileClone != null)
        {
            // Assign the damage and speed values to the projectile
            projectileClone.GetComponent<Projectile>().damage = damage;
            projectileClone.GetComponent<Projectile>().projectileSpeed = projectileSpeed;

            // Let the projectile know their who their owner is 
            projectileClone.GetComponent<Projectile>().owner = transform.parent.parent.gameObject;

            // If the projectile does not chit anything after a set time, destroy it
            Destroy(projectileClone, lifeSpan);
            // Reset timer
            timeTillNextShot = timeReset;
        }
    }

}
