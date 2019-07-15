using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonLoadLevel : MonoBehaviour
{
    public void loadLevel(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
