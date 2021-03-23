using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPawn : Pawn
{
    [Header("Data")]
    public float moveSpeed;
    public float rotateSpeed;
    private Quaternion targetRotation;
    private Vector3 moveDirection;
    public Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        // Gain access to the animation controller
        characterAnimator = GetComponent<Animator>();
        currentHealth = maxHealth;
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterAnimator.SetBool("Alive", true);
        respawner = transform.parent.gameObject;
        healthText = respawner.GetComponent<RespawnCharacter>().healthText;
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);
        healthText.text = "Current health " + currentHealth + " / " + maxHealth;
    }

    public void Idle()
    {
        characterAnimator.SetFloat("Forward", 0f);
        characterAnimator.SetFloat("Right", 0f);
    }

    public void Walk()
    {
        // normalize movement vector
        moveDirection.Normalize();

        //Change the move direction to be based on world space
        moveDirection = transform.InverseTransformDirection(moveDirection);
        moveDirection.z = Input.GetAxis("Vertical");
        // Walk forwards and backwards
        characterAnimator.SetFloat("Vertical", moveDirection.z * moveSpeed);

    }

    public void Strafe()
    {
        // normalize movement vector
        moveDirection.Normalize();

        //Change the movedirection to be based on world space
        moveDirection = transform.InverseTransformDirection(moveDirection);
        moveDirection.x = Input.GetAxis("Horizontal");
        // Move to the left and right
        characterAnimator.SetFloat("Horizontal", moveDirection.x * moveSpeed);
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

    public void OnAnimatorIK()
    {
        // Check if there is a weapon
        if (weaponEquipped == true)
        {
            // If the player is weilding a two handed weapon
            if (weapon.RHandPoint && weapon.LHandPoint)
            {
                characterAnimator.SetIKPosition(AvatarIKGoal.LeftHand, weapon.LHandPoint.position);
                characterAnimator.SetIKPosition(AvatarIKGoal.RightHand, weapon.RHandPoint.position);
                characterAnimator.SetIKRotation(AvatarIKGoal.LeftHand, weapon.LHandPoint.rotation);
                characterAnimator.SetIKRotation(AvatarIKGoal.RightHand, weapon.RHandPoint.rotation);

                characterAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
                characterAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
                characterAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
                characterAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
            }
            else if (weapon.RHandPoint && !weapon.LHandPoint)
            {
                // If the player is weilding a weapon for the right hand only
                    characterAnimator.SetIKPosition(AvatarIKGoal.RightHand, weapon.RHandPoint.position);
                    characterAnimator.SetIKRotation(AvatarIKGoal.RightHand, weapon.RHandPoint.rotation);
                    characterAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
                    characterAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
                    characterAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
                    characterAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
            }
            else if (weapon.LHandPoint && !weapon.RHandPoint)
            {
                // If the player is weilding a weapon for the left hand only
                characterAnimator.SetIKPosition(AvatarIKGoal.LeftHand, weapon.LHandPoint.position);
                characterAnimator.SetIKRotation(AvatarIKGoal.LeftHand, weapon.LHandPoint.rotation);
                characterAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
                characterAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
                characterAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
                characterAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
            }
        }
    }
}
