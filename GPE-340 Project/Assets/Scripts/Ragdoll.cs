using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField]
    private Rigidbody[] childRBs;
    [SerializeField]
    private Collider[] childCol;
    [SerializeField]
    private Rigidbody parentRB;
    [SerializeField]
    private Collider parentCol;
    [SerializeField]
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        // Access the object's Collider and Rigidbody
        parentCol = GetComponent<Collider>();
        parentRB = GetComponent<Rigidbody>();
        // Access the children objects' Collider and Rigidbody
        childCol = GetComponentsInChildren<Collider>();
        childRBs = GetComponentsInChildren<Rigidbody>();
        // Access the animator
        animator = GetComponent<Animator>();
        RagdollOff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RagdollOn()
    {
        // Turn on child colliders and rigidbodies
        foreach (Collider collider in childCol)
        {
            // Go through each child object and enable the colliders
            collider.enabled = true;
        }
        foreach (Rigidbody rb in childRBs)
        {
            // Go through each child object and deactivate kinematics to allow the physics engine to apply to the children
            rb.isKinematic = false;
        }

        // Turn off parent collider and rigidbody
        parentCol.enabled = false;
        parentRB.isKinematic = true;

        // Turn off animator
        animator.enabled = false;
    }

    public void RagdollOff()
    {
        // Turn off child colliders and rigidbodies
        foreach (Collider collider in childCol)
        {
            // Go through each child object and disable the colliders
            collider.enabled = false;
        }
        foreach (Rigidbody rb in childRBs)
        {
            // Go through each child object and activate kinematics to stop the physics engine from applying to the children
            rb.isKinematic = true;
        }

        // Turn on parent collider and rigidbody
        parentCol.enabled = true;
        parentRB.isKinematic = false;

        // Turn on animator
        animator.enabled = true;
    }
}
