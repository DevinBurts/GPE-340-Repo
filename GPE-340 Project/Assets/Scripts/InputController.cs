using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Create key codes for controls
    [SerializeField]
    private KeyCode jump;
    [SerializeField]
    private KeyCode forwards;
    [SerializeField]
    private KeyCode backwards;
    [SerializeField]
    private KeyCode left;
    [SerializeField]
    private KeyCode right;
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
        // play the running animation that corresponds with the player's input
        
        if (Input.GetKey(forwards) || Input.GetKey(backwards))
        {
            playerPawn.Walk();
        }
        if (Input.GetKey(left) || Input.GetKey(right))
        {
            playerPawn.Strafe();
        }

        playerPawn.RotateTowards(GetMousePosition());

        // play the jump animation when the jump key is pressed
        if (Input.GetKeyDown(jump))
        {
            playerPawn.Jump();

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
