using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEnemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Fireball"))
        {
            SceneManager.LoadScene("Victory");
        }
    }
}
