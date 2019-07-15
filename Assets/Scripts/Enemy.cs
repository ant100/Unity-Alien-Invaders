using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // include so we can load new scenes

public class Enemy : MonoBehaviour
{
    Animator _animator;
    [SerializeField]
    int enemyValue = 1;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null) // if Animator is missing
            Debug.LogError("Animator component missing from this gameobject");
    }

    private void Update()
    {
        // check if y position is equal to edge
        Vector3 pos = transform.position;
        if (pos.y < -4.7f)
        {
            if (GameManager.gm) // if the gameManager is available, tell it to reset the game
                GameManager.gm.ResetGame();
            else // otherwise, just reload the current level
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {        
            // kill the alien
            StartCoroutine(KillEnemy());
        }
    }

    // coroutine to kill the enemy
    IEnumerator KillEnemy()
    {

        // play the death animation
        _animator.SetTrigger("hit");

        // After waiting tell the GameManager to reset the game
        yield return new WaitForSeconds(0.3f);

        // increase points
        if (GameManager.gm)
        {
            GameManager.gm.addPoints(enemyValue);
            GameManager.gm.addEnemiesKilled();
        }

        // check if it is the last alien
        if (GameManager.gm)
        {
            // there are 35 enemies, so this means we killed them all!
            if (GameManager.gm._enemiesKilled >= 35)
            {
                GameManager.gm.LevelCompete();
            }
        }
        Destroy(this.gameObject);
    }
}
