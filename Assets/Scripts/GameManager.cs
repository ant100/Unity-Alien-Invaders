using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI; // include UI namespace so can reference UI elements
using UnityEngine.SceneManagement; // include so we can manipulate SceneManager

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    // levels to move to on victory and lose
    [SerializeField]
    string levelAfterVictory;
    [SerializeField]
    string levelAfterGameOver;

    // game performance
    public int score = 0;

    [SerializeField]
    AudioClip winningSFX;

    // UI elements to control
    [SerializeField]
    Text UIScore;

    // private variables
    GameObject _player;
    Scene _scene;
    [HideInInspector]
    public int _enemiesKilled;

    private void Awake()
    {
        // setup reference to game manager
        if (gm == null)
            gm = this.GetComponent<GameManager>();

        // setup all the variables, the UI, and provide errors if things not setup properly.
        setupDefaults();
    }

    // setup all the variables, the UI, and provide errors if things not setup properly.
    void setupDefaults()
    {
        // setup reference to player
        if (_player == null)
            _player = GameObject.FindGameObjectWithTag("Player");

        if (_player == null)
            Debug.LogError("Player not found in Game Manager");

        // get current scene
        _scene = SceneManager.GetActiveScene();

        // if levels not specified, default to current level
        if (levelAfterVictory == "")
        {
            Debug.LogWarning("levelAfterVictory not specified, defaulted to current level");
            levelAfterVictory = _scene.name;
        }

        if (levelAfterGameOver == "")
        {
            Debug.LogWarning("levelAfterGameOver not specified, defaulted to current level");
            levelAfterGameOver = _scene.name;
        }

        // friendly error messages
        if (UIScore == null)
            Debug.LogError("Need to set UIScore on Game Manager.");

        // get stored player prefs
        refreshPlayerState();

        // get the UI ready for the game
        refreshGUI();
    }

    // get stored Player Prefs if they exist, otherwise go with defaults set on gameObject
    void refreshPlayerState()
    {
        score = PlayerPrefManager.GetScore();
    }

    // refresh all the GUI elements
    void refreshGUI()
    {
        // set the text elements of the UI
        UIScore.text = "Score: " + score.ToString();
    }

    public void addPoints(int amount)
    {
        score += amount;
        UIScore.text = "Score: " + score.ToString();
    }

    public void addEnemiesKilled()
    {
        _enemiesKilled += 1;
    }

    // public function for level complete
    public void LevelCompete()
    {
        // save the current player prefs before moving to the next level
        PlayerPrefManager.SavePlayerState(score);

        // use a coroutine to allow the player to get fanfare before moving to next level
        StartCoroutine(LoadNextLevel());
    }

    // load the nextLevel after delay
    IEnumerator LoadNextLevel()
    {
        // play winning sound effect
        AudioSource audio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        audio.clip = winningSFX;
        audio.Play();

        yield return new WaitForSeconds(2.5f);
        // reset number of killed enemies
        _enemiesKilled = 0;

        if(levelAfterVictory == "You Win")
        {
            // reset number of enemies
            PlayerPrefManager.ResetPlayerState();
        }
        SceneManager.LoadScene(levelAfterVictory);
    }

    // public function to remove player life and reset game accordingly
    public void ResetGame()
    {
        // remove life and update GUI
        PlayerPrefManager.ResetPlayerState();

        // load the gameOver screen
        SceneManager.LoadScene(levelAfterGameOver);
    }
}
