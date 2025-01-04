using UnityEngine;

public class DetectionRadius : MonoBehaviour
{
    public bool isPlayerDetected;
    
    private GameObject _enemy;
    
    void Start()
    {
        _enemy = transform.parent.Find("Enemy").gameObject;
    }
    
    void Update()
    {
        FollowEnemy();
    }
    
    // Fixes the detection radius to the enemy
    private void FollowEnemy()
    {
        transform.position = _enemy.transform.position;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerDetected = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerDetected = false;
        }
    }
}
