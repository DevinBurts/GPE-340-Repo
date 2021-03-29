using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    public GameObject player;
    // List to store all of the active enemies
    public List<GameObject> activeEnemies;
    public int lives;
    public int maxLives;
    public bool gamePaused;
    public KeyCode pause;
    public int enemiesDefeated;
    public int quota;
    public enum State {menu, game};
    public State state;
    [Header("Canvas")]
    public GameObject MenuCanvas;
    public GameObject SettingsCanvas;
    public GameObject GameCanvas;
    public GameObject VictoryCanvas;
    public GameObject LossCanvas;
    public GameObject PauseCanvas;
    public Text enemiesDefeatedText;
    public Text playerHealthText;
    public Text playerLivesText;
    [Header("Audio")]
    public float sfxVol;
    public float musicVol;
    public AudioClip button;
    public float vol;
    // Destroy additional instances of the Game Manager if any
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Find the player
        player = GameObject.FindWithTag("Player");
        lives = maxLives;
        state = State.game;
        LoadVolPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        // Have the music clip volume change with the music variable
        GetComponent<AudioSource>().volume = musicVol;

        if (state == State.game)
        {
            // Keep track of how many enemies have been defeated on screen
            enemiesDefeatedText.text = enemiesDefeated + " out of " + quota + " defeated!";
            // Keep track of lives on screen
            playerLivesText.text = lives + "out of " + maxLives + "lives left";
            if (Input.GetKey(pause))
            {
                // Assign paused to the inverse of paused
                gamePaused = true;
            }
            for (int i = 0; i < activeEnemies.Count; i++)
            {
                if (activeEnemies[i] == null)
                {
                    // Remove the destroyed enemy from the list
                    activeEnemies.Remove(activeEnemies[i]);
                    enemiesDefeated++;
                }
            }
            if (enemiesDefeated >= quota)
            {
                PauseGame();
                VictoryMenu();
            }
            if (lives <= 0 )
            {
                PauseGame();
                LossMenu();
            }
            if (gamePaused == true)
            {
                // Activate the Pause Menu
                PauseCanvas.SetActive(true);
            }
            else
            {
                // Deactivate the Pause Menu
                PauseCanvas.SetActive(false);
            }
        }
    }

    public void Settings()
    {
        SettingsCanvas.SetActive(true);
            MenuCanvas.SetActive(false);
    }
    public void LoadVolPrefs()
    {
        sfxVol = PlayerPrefs.GetFloat("SFX");
        musicVol = PlayerPrefs.GetFloat("Music");

    }
    // Increase the volume of the music
    public void MusicUp()
    {
        AudioSource.PlayClipAtPoint(button, transform.position, vol);
        musicVol += 0.1f;

        // Ensure the volume cannot exceed maximum value
        if (musicVol >= 1.0f)
        {
            musicVol = 1.0f;
        }

        // Save the change in volume
        PlayerPrefs.SetFloat("Music", musicVol);
        PlayerPrefs.Save();
    }

    // Decrease the volume of the music
    public void MusicDown()
    {
        AudioSource.PlayClipAtPoint(button, transform.position, vol);
        musicVol -= 0.1f;

        // Ensure the volume cannot exceed maximum value
        if (musicVol <= 0)
        {
            musicVol = 0;
        }

        // Save the change in volume
        PlayerPrefs.SetFloat("Music", musicVol);
        PlayerPrefs.Save();
    }
    // Increase the SFX volume
    public void SFXUp()
    {
        AudioSource.PlayClipAtPoint(button, transform.position, vol);
        sfxVol += 0.1f;

        // Ensure the volume cannot exceed maximum value
        if (sfxVol >= 1.0f)
        {
            sfxVol = 1.0f;
        }
        // Update the volume of the button
        vol = sfxVol;
        // Save the change in volume
        PlayerPrefs.SetFloat("SFX", sfxVol);
        PlayerPrefs.Save();
    }

    public void SFXDown()
    {
        AudioSource.PlayClipAtPoint(button, transform.position, vol);
        sfxVol -= 0.1f;

        // Ensure the volume cannot exceed maximum value
        if (sfxVol <= 0)
        {
            sfxVol = 0;
        }
        // Update the volume of the button
        vol = sfxVol;
        // Save the change in volume
        PlayerPrefs.SetFloat("SFX", sfxVol);
        PlayerPrefs.Save();
    }

    public void Play()
    {
        // Load the game scene
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        state = State.game;
        // Activate the Game Canvas
        GameCanvas.SetActive(true);
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            if (activeEnemies[i] != null)
            {
                // Assgin the player to be the enemies' target
                activeEnemies[i].GetComponent<EnemyPawn>().target = player;
            }
        }
    }

    public void QuitGame()
    {
        // Exit the game
        Application.Quit();
    }

    public void Menu()
    {
        // Load the menu scene
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void PauseGame()
    {
        // Set the time scale to zero to pause all movement in the game
        gamePaused = true;
        Time.timeScale = 0f;
    }
    public void UnpauseGame()
    {
        // Set the time scale to zero to pause all movement in the game
        gamePaused = false;
        Time.timeScale = 1f;
    }

    public void VictoryMenu()
    {
        // Show the Victory Screen
        VictoryCanvas.SetActive(true);
        LossCanvas.SetActive(false);
        GameCanvas.SetActive(false);
        gamePaused = false;
    }

    public void LossMenu()
    {
        // SHow the Loss Screen
        VictoryCanvas.SetActive(false);
        LossCanvas.SetActive(true);
        GameCanvas.SetActive(false);
        gamePaused = false;
    }



}
