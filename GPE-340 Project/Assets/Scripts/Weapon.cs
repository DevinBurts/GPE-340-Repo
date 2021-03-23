using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [Header("Projectile Prefab")]
    public GameObject projectile;

    [Header("Shoot Script")]
    public Shoot shoot;

    [Header("Projectile Variables")]
    public Transform firePoint;
    public float projectileSpeed;
    public int damage;
    public float lifeSpan;

    [Header("Unity Events")]
    public UnityEvent OnAttackStart;
    public UnityEvent OnAttackEnd;

    [Header("IK Points")]
    public Transform RHandPoint;
    public Transform LHandPoint;

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void AttackStart()
    {
        // Call all functions attached
        OnAttackStart.Invoke();
    }

    public virtual void AttackEnd()
    {
        // Call all functions attached
        OnAttackEnd.Invoke();
    }
}
