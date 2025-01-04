using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    
    public bool isGoingLeft;
    
    private Rigidbody2D _rb;
    private EnemyAttack _enemyAttack;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _enemyAttack = GetComponent<EnemyAttack>();
    }

    private void Update()
    {
        // Stop to shoot if attacking
        if (!_enemyAttack.isAttacking)
        {
            Move();
        }
    }
    
    private void Move()
    {
        float moveSpeed = speed * Time.deltaTime;
        Vector3 movement = new Vector3(moveSpeed, 0, 0);
        _rb.transform.position += movement;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Traveling_Stop"))
        {
            speed *= -1;
            isGoingLeft = !isGoingLeft;
        }
    }
}
