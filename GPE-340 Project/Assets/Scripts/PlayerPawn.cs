using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPawn : Pawn
{
    private Animator characterAnimator;
    public float moveSpeed;
    public float rotateSpeed;
    private Quaternion targetRotation;
    private Vector3 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        // Gain access to the animation controller
        characterAnimator = GetComponent<Animator>();
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Walk()
    {
        // normalize movement vector
        moveDirection.Normalize();

        //Change the move direction to be based on world space
        moveDirection = transform.InverseTransformDirection(moveDirection);
        moveDirection.z = Input.GetAxis("Vertical");
        // Walk forwards and backwards
        characterAnimator.SetFloat("Forward", moveDirection.z * moveSpeed);

    }

    public void Jump()
    {
        // Enter the Jump animation and elevate the player
        characterAnimator.SetBool("Jumping", true);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up, ForceMode.Impulse);
        RaycastHit hit;
        // Check if the player is grounded
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
            characterAnimator.SetBool("Airborne", true);
        }
        else
        {
            characterAnimator.SetBool("Airborne", false);
            characterAnimator.SetBool("Jumping", false);
        }
    }

    public void Strafe()
    {
        // normalize movement vector
        moveDirection.Normalize();

        //Change the movedirection to be based on world space
        moveDirection = transform.InverseTransformDirection(moveDirection);
        moveDirection.x = Input.GetAxis("Horizontal");
        // Move to the left and right
        characterAnimator.SetFloat("Right", moveDirection.x * moveSpeed);
    }
    public void RotateTowards(Vector3 targetPoint)
    {
        // Find the distance between the target and the player's posiiton
        Vector3 vectorToTarget = targetPoint - transform.position;
        // Create a rotation 
        targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);

        // rotate towards a target per frame
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}
