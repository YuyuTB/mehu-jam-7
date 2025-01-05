using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEnemy : MonoBehaviour
{
    private void OnDestroy()
    {
        SceneManager.LoadScene("Victory");
    }
}
