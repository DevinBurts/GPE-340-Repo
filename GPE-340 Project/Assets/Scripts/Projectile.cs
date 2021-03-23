using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public float projectileSpeed;
    public GameObject owner;
    private GameObject target;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * projectileSpeed;

    }

    public void OnTriggerEnter(Collider col)
    {
        // Check if the person who fired hit an enemy or an ally
        if (owner.tag == "Player" && col.gameObject.tag == "Enemy")
        {
            // Have the enemy take damage
            target = col.gameObject;
            target.GetComponent<EnemyPawn>().TakeDamage(damage);
            
        }
        else if (owner.tag == "Enemy" && col.gameObject.tag == "Player")
        {
            // Have the player take damage
            target = col.gameObject;
            target.GetComponent<PlayerPawn>().TakeDamage(damage);
        }
        // Destroy the projectile after collision
        Destroy(gameObject);
    }

}
