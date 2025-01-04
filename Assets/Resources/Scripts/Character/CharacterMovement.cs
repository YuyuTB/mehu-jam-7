using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float jumpHeight = 1.5f;

    public bool isGoingRight;
    public bool isGoingLeft;
    public bool isGrounded;
    
    private Rigidbody2D _rb;
    private float _gravity;
    private Vector3 _velocity;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gravity = Physics.gravity.y;
    }

    void Update()
    {
        Move();
        ApplyGravity();
        Jump();
        CheckIfRunning();
        
        // Apply the vertical velocity
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _velocity.y);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    
    private void CheckIfRunning()
    {
        isGoingLeft = Input.GetAxis("Horizontal") < 0;
        isGoingRight = Input.GetAxis("Horizontal") > 0;
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        
        if (horizontal == 0)
        {
            return;
        }
        
        float moveSpeed = speed * horizontal * Time.deltaTime;
        Vector3 movement = new Vector3(moveSpeed, 0, 0);
        _rb.transform.position += movement;
    }

    private void Jump()
    {
        if (isGrounded && Input.GetAxis("Jump") > 0)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * _gravity);
        }
    }

    private void ApplyGravity()
    {
        if (!isGrounded)
        {
            _velocity.y += _gravity * Time.deltaTime;
        }
        else if (_velocity.y < 0)
        {
            _velocity.y = 0f;
        }
    }
}