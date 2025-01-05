using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterDeath : MonoBehaviour
{
    public void KillCharacter()
    {
        SceneManager.LoadScene("DeathMenu");
    }
}
