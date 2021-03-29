using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnCharacter : MonoBehaviour
{
    [SerializeField]
    private GameObject character;
    [SerializeField]
    private GameObject charToTrack;
    public Text healthText;
    public bool isAlive;
    public GameObject target;
    public GameObject GameManager;
    public GameManager gmCaller;
    // Start is called before the first frame update
    void Start()
    {
        gmCaller = GameManager.GetComponent<GameManager>();
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive == false)
        {
            Respawn();
        }
        if (charToTrack == null)
        {
            isAlive = false;
        }
    }
    
    public void Respawn()
    {
        isAlive = true;
        // Respawn the character and make the respawner the parent
        GameObject newCharacter = Instantiate(character, transform.position, Quaternion.identity);
        newCharacter.transform.parent = this.transform;
        charToTrack = newCharacter;
        if (charToTrack.tag == "Player")
        {
            charToTrack.GetComponent<PlayerPawn>().respawner = this.gameObject;
            charToTrack.GetComponent<PlayerPawn>().gmCaller = gmCaller;
            // Assign the character animator to the player
            charToTrack.GetComponent<PlayerPawn>().characterAnimator = charToTrack.GetComponent<Animator>();
            // Ensure the animator knows the character is alive 
            charToTrack.GetComponent<PlayerPawn>().characterAnimator.SetBool("Alive", true);
        }
        else
        {
            charToTrack.GetComponent<EnemyPawn>().respawner = this.gameObject;
            charToTrack.GetComponent<EnemyPawn>().gmCaller = gmCaller;
            // Assign the character animator to the player
            charToTrack.GetComponent<EnemyPawn>().characterAnimator = charToTrack.GetComponent<Animator>();
            // Ensure the animator knows the character is alive 
            charToTrack.GetComponent<EnemyPawn>().characterAnimator.SetBool("Alive", true);
            charToTrack.GetComponent<AIController>().target = target;
        }
    }
}
