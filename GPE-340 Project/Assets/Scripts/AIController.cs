using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public GameObject target;
    public EnemyPawn pawn;
    public NavMeshAgent agent;
    private Vector3 moveDirection;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        pawn = GetComponent<EnemyPawn>();
        animator = GetComponent<Animator>();
        target = pawn.target;
    }

    // Update is called once per frame
    void Update()
    {
        // Use the nav agent to find a path to the player
        agent.SetDestination(target.transform.position);
        moveDirection = agent.desiredVelocity;
        pawn.Move(moveDirection);
    }
    public void OnAnimatorMove()
    {
        // Have the NavMesh gent move at the same speed as the animator
        agent.velocity = animator.velocity;
    }
}
