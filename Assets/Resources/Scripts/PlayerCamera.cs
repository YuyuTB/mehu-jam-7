using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 _playerPosition;
    private Vector3 _offset;
    
    private CharacterResolution _characterResolution;
    private Camera _camera;
    
    private void Start()
    {
        _characterResolution = player.GetComponent<CharacterResolution>();
        _camera = GetComponent<Camera>();
        _playerPosition = player.position;
        SetOffset();
        SetPosition();
    }
    
    private void LateUpdate()
    {
        SetPosition();
        SetOffset();
        FollowPlayer();
    }
    
    // Makes the camera follow the player
    private void FollowPlayer()
    {
        // Ensures the camera follows the player on the x-axis but not on the y-axis
        _playerPosition = new Vector3(player.position.x, _playerPosition.y);
        transform.position = _playerPosition + _offset;
    }

    // Sets the offset of the camera
    private void SetOffset()
    {
        float yOffset = _characterResolution.isLowDefinition ? 0.5f : 2f;
        _offset = new Vector3(0, yOffset, -10f);
    }

    // Determines the camera's position relative to the player
    private void SetPosition()
    {
        _camera.orthographicSize = _characterResolution.isLowDefinition ? 2 : 4;
    }
}
