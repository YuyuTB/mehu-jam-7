using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float attackStopDuration = 0.3f;
    private bool _isTouchingWallLeft;
    private bool _isTouchingWallRight;

    public bool isGoingRight;
    public bool isGoingLeft;
    public bool isGrounded;
    public bool isMovementStoppedForAttack;
    
    private Rigidbody2D _rb;
    private float _gravity;
    private Vector3 _velocity;
    private CharacterResolution _characterResolution;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _characterResolution = GetComponent<CharacterResolution>();
        _gravity = Physics.gravity.y;
    }

    void Update()
    {
        if (!isMovementStoppedForAttack)
        {
            CheckIfRunning();
        }
    }

    void FixedUpdate()
    {
        if (!isMovementStoppedForAttack)
        {
            Move();
            ApplyGravity();
            Jump();
        }
        
        // Apply the vertical velocity
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _velocity.y);
    }
    
    private void CheckIfRunning()
    {
        isGoingLeft = Input.GetAxis("Horizontal") < 0;
        isGoingRight = Input.GetAxis("Horizontal") > 0;
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        
        // Returns if no input or if the character is touching a wall in the direction of movement
        if (horizontal == 0)
        {
            return;
        }
        if (_isTouchingWallLeft && horizontal < 0)
        {
            return;
        } 
        if (_isTouchingWallRight && horizontal > 0)
        {
            return;
        }
        
        float moveSpeed = speed * horizontal * Time.deltaTime;
        Vector3 movement = new Vector3(moveSpeed, 0, 0);
        _rb.transform.position += movement;
    }

    private void Jump()
    {
        jumpHeight = _characterResolution.isLowDefinition ? 5f : 10f;
        
        if (isGrounded && Input.GetAxis("Jump") > 0)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * _gravity);
        }
    }

    private void ApplyGravity()
    {
        if (!isGrounded)
        {
            _velocity.y += _gravity * fallMultiplier * Time.deltaTime;
        }
        else if (_velocity.y < 0)
        {
            _velocity.y = 0f;
        }
    }
    
    public void StopMovementForAttack()
    {
        StartCoroutine(StopMovementCoroutine());
    }

    // Stops the character's movement for a short duration
    private IEnumerator StopMovementCoroutine()
    {
        isMovementStoppedForAttack = true;
        _velocity.y = 0f;
        yield return new WaitForSeconds(attackStopDuration);
        isMovementStoppedForAttack = false;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (other.gameObject.CompareTag("Wall")
            && isGoingLeft)
        {
            _isTouchingWallLeft = true;
        } 
        else if (other.gameObject.CompareTag("Wall")
            && isGoingRight)
        {
            _isTouchingWallRight = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        
        if (other.gameObject.CompareTag("Wall"))
        {
            _isTouchingWallLeft = false;
            _isTouchingWallRight = false;
        }
    }
}