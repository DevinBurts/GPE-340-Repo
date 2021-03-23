using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Create key codes for controls
    [Header("Inputs")]
    public KeyCode jump;
    public KeyCode forwards;
    public KeyCode backwards;
    public KeyCode left;
    public KeyCode right;
    public KeyCode attack;
    public KeyCode dropWeapon;

    private PlayerPawn playerPawn;
    // Start is called before the first frame update
    void Start()
    {
        // Access the Player Pawn script
        playerPawn = GetComponent<PlayerPawn>();
    }

    // Update is called once per frame
    void Update()
    {
        // Have the player rotate towards the mouse
        playerPawn.RotateTowards(GetMousePosition());

        // play the running animation that corresponds with the player's input

        if (Input.GetKey(forwards) || Input.GetKey(backwards))
        {
            playerPawn.Walk();
        }
        if (Input.GetKey(left) || Input.GetKey(right))
        {
            playerPawn.Strafe();
        }
        if (Input.GetKeyUp(forwards) && Input.GetKeyUp(backwards))
        {
            playerPawn.Idle();
        }
        if (Input.GetKeyDown(attack))
        {
            // Access the player's current weapon and begin attacking
            playerPawn.weapon.AttackStart();
        }
        if (Input.GetKeyUp(attack))
        {
            // Access the player's current weapon and stop attacking
            playerPawn.weapon.AttackEnd();
        }
        if (Input.GetKey(dropWeapon))
        {
            // Unequip the current weapon
            playerPawn.UnequipWeapon();
        }


    }

    public Vector3 GetMousePosition()
    {
        // Variable to store the position of the mouse 
        Vector3 mousePointInWorld = new Vector3();
           // Create a plane based on the player's position
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        // Generate a ray to point to the mouse's position
        Ray mouseRay;
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDistance;
        // Check if the player's plane and the mouse ray intersect
        if (playerPlane.Raycast(mouseRay, out hitDistance))
        {
            mousePointInWorld = mouseRay.GetPoint(hitDistance);
        }
        return mousePointInWorld;
    }
}
