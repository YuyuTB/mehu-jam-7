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
        SetOffset();
        SetPosition();
    }
    
    private void LateUpdate()
    {
        SetPosition();
        SetOffset();
        if (player != null)
        {
            FollowPlayer();
        }
    }
    
    // Makes the camera follow the player
    private void FollowPlayer()
    {
        transform.position = player.position + _offset;
    }

    // Sets the offset of the camera
    private void SetOffset()
    {
        float yOffset = _characterResolution.isLowDefinition ? 1.2f : 3.8f;
        _offset = new Vector3(0, yOffset, -10f);
    }

    // Determines the camera's position relative to the player
    private void SetPosition()
    {
        _camera.orthographicSize = _characterResolution.isLowDefinition ? 3 : 6;
    }
}
