using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // include so we can manipulate SceneManager

public static class PlayerPrefManager
{
    public static int GetScore()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            return PlayerPrefs.GetInt("Score");
        }
        else
        {
            return 0;
        }
    }

    public static void SetScore(int score)
    {
        PlayerPrefs.SetInt("Score", score);
    }

    // story the current player state info into PlayerPrefs
    public static void SavePlayerState(int score)
    {
        // save currentscore to PlayerPrefs for moving to next level
        PlayerPrefs.SetInt("Score", score);
    }

    // reset stored player state and variables back to defaults
    public static void ResetPlayerState()
    {
        Debug.Log("Player State reset.");
        PlayerPrefs.SetInt("Score", 0);
    }

}
