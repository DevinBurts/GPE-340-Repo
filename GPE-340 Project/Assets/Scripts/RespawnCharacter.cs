using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnCharacter : MonoBehaviour
{
    [SerializeField]
    private GameObject character;
    public Text healthText;
    [SerializeField]
    private bool isAlive;
    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive == false)
        {
            Respawn();
        }
    }
    public void Die(GameObject character)
    {
        // Play the death animation, remove the character, and respawn them
        if (character.tag == "Player")
        {
            character.GetComponent<PlayerPawn>().characterAnimator.SetBool("Alive", false);
        }
        else if (character.tag == "Enemy")
        {
            character.GetComponent<EnemyPawn>().characterAnimator.SetBool("Alive", false);
        }
        // Delete the character after it dies
        Destroy(character);
        isAlive = false;
    }
    public void Respawn()
    {
        isAlive = true;
        // Respawn the character and make the respawner the parent
        GameObject newCharacter = Instantiate(character, transform.position, Quaternion.identity);
        newCharacter.transform.parent = this.transform;
        character = newCharacter;
        if (character.tag == "Player")
        {
            character.GetComponent<PlayerPawn>().respawner = this.gameObject;
            // Assign the character animator to the player
            character.GetComponent<PlayerPawn>().characterAnimator = character.GetComponent<Animator>();
            // Ensure the animator knows the character is alive 
            character.GetComponent<PlayerPawn>().characterAnimator.SetBool("Alive", true);
        }
        else
        {
            character.GetComponent<EnemyPawn>().respawner = this.gameObject;
            // Assign the character animator to the player
            character.GetComponent<EnemyPawn>().characterAnimator = character.GetComponent<Animator>();
            // Ensure the animator knows the character is alive 
            character.GetComponent<EnemyPawn>().characterAnimator.SetBool("Alive", true);
        }
    }
}
