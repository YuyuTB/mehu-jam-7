using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool isAttacking;
    
    private readonly float _attackCooldown = 3f;
    private float _attackTimer;
    private Vector3 _leftTravelingStopPosition;
    private Vector3 _rightTravelingStopPosition;
    
    private EnemyMovement _enemyMovement;
    private Bullet _bullet;
    private GetTravelingStops _getSiblingStops;
    private DetectionRadius _detectionRadius;
    private Transform _playerTransform;
    
    void Start()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        _getSiblingStops = GetComponent<GetTravelingStops>();
        _bullet = Resources.Load<Bullet>("Prefabs/Enemy/Bullet");
        _detectionRadius = transform.parent.Find("Detection_Radius").GetComponent<DetectionRadius>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        SetTravelingStops();
    }
    
    void Update()
    {
        AttackPlayer();
    }

    private void SetTravelingStops()
    {
        List<Vector3> travelingStops = _getSiblingStops.DetectSiblingStops();

        if (travelingStops.Count != 2)
        {
            throw new Exception("The enemy must have exactly two sibling stops.");
        }
        
        if (travelingStops[0].x < travelingStops[1].x)
        {
            _leftTravelingStopPosition = travelingStops[0];
            _rightTravelingStopPosition = travelingStops[1];
        }
        else
        {
            _leftTravelingStopPosition = travelingStops[1];
            _rightTravelingStopPosition = travelingStops[0];
        } 
    }

    private void AttackPlayer()
    {
        // Check if the player is within the enemy's detection radius
        if (_detectionRadius.isPlayerDetected)
        {
            isAttacking = true;
            AttackCooldown();
            return;
        }

        isAttacking = false;
    }
    
    private void AttackCooldown()
    {
        if (_attackTimer > 0)
        {
            _attackTimer -= Time.deltaTime;
        }
        
        if (_attackTimer <= 0)
        {
            Attack();
        }
    }
    
    private void Attack()
    {
        _attackTimer = _attackCooldown;
        
        Vector2 direction = (_playerTransform.position - transform.position).normalized;
        Vector2 bulletPosition = transform.position;
        bulletPosition.y -= 0.2f;
        bulletPosition.x += _enemyMovement.isGoingLeft ? -transform.localScale.x : transform.localScale.x;
        Bullet bullet = Instantiate(_bullet, bulletPosition, Quaternion.identity);
        bullet.Initialize(_enemyMovement, direction);
    }
    
    // Kill the player if it touches the enemy
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}
