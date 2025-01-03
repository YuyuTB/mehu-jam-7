using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jumpHeight = 3.0f;

    public bool isGoingRight;
    public bool isGoingLeft;
    
    private CharacterController _controller;
    private float _gravity;
    private Vector3 _velocity;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _gravity = Physics.gravity.y;
    }

    void Update()
    {
        Move();
        ApplyGravity();
        Jump();
        CheckIfRunning();
        
        // Apply the vertical velocity
        _controller.Move(_velocity * Time.deltaTime);
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
        _controller.Move(movement);
    }

    private void Jump()
    {
        if (_controller.isGrounded && Input.GetAxis("Jump") > 0)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * _gravity);
        }
    }

    private void ApplyGravity()
    {
        if (!_controller.isGrounded)
        {
            _velocity.y += _gravity * Time.deltaTime;
        }
        else if (_velocity.y < 0)
        {
            _velocity.y = 0f;
        }
    }
}