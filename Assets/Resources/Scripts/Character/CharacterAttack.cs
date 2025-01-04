using System.Collections;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    private float _fireballTransformX;
    private bool _isMoving;

    private GameObject _fireball;
    private CharacterMovement _characterMovement;
    private CharacterResolution _characterResolution;
    
    void Start()
    {
        // Define the fireball prefab
        _fireball = Resources.Load<GameObject>("Prefabs/Character/Fireball");
        _characterMovement = GetComponent<CharacterMovement>();
        _characterResolution = GetComponent<CharacterResolution>();
    }
    
    void Update()
    {
        CheckIfMoving();
        GetAttackInput();
    }
    
    private void GetAttackInput()
    {
        // Returns if the character is in low definition
        if (_characterResolution.isLowDefinition)
        {
            return;
        }
        
        bool attackInput = Input.GetButtonDown("Fire1");
        
        // Returns if no input or if the character is already attacking
        if (!attackInput 
            || _characterMovement.isMovementStoppedForAttack
            || !_isMoving
            || !_characterMovement.isGrounded)
        {
            return;
        }
        
        SetFireballDirection();
        
        Attack();
    }
    
    private void CheckIfMoving()
    {
        _isMoving = _characterMovement.isGoingLeft || _characterMovement.isGoingRight;
    }

    private void SetFireballDirection()
    {
        if (_characterMovement.isGoingLeft)
        {
            _fireballTransformX = -transform.localScale.x;
        }
        else if (_characterMovement.isGoingRight)
        {
            _fireballTransformX = transform.localScale.x;
        }
    }

    private void Attack()
    {
        _characterMovement.StopMovementForAttack();
        InstantiateFireball();
    }
    
    private void InstantiateFireball()
    {
        // Instantiates the fireball in front of the player and passes it the character's direction
        Vector3 fireballPosition = transform.position;
        float directionOffset = _characterMovement.isGoingLeft ? -transform.localScale.x : transform.localScale.x;
        fireballPosition.x += directionOffset + _fireballTransformX;
        fireballPosition.y -= 0.2f;
        
        GameObject fireballInstance = Instantiate(_fireball, fireballPosition, Quaternion.identity);
        Fireball fireballScript = fireballInstance.GetComponent<Fireball>();
        fireballScript.Initialize(_characterMovement);
    }
}
