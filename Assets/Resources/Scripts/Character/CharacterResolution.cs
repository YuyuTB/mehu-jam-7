using UnityEngine;

public class CharacterResolution : MonoBehaviour
{
    // Character high definition on initialization
    public bool isLowDefinition;
    
    private float _colliderRadius;
    private float _colliderHeight;
    
    private CharacterController _controller;
    
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        DefineCharacterStateByResolution();
    }
    
    public void SetDefinition(string definition)
    {
        /*
            If the definition received is "low", the character is set to low definition, otherwise it is set to high definition
         */
        isLowDefinition = definition == "low";
        DefineCharacterStateByResolution();
        SetCollider();
    }
    
    private void DefineCharacterStateByResolution()
    {
        _colliderRadius = isLowDefinition ? 0.5f : 0.8f;
        _colliderHeight = isLowDefinition ? 1.25f : 2.55f;
    }
    
    private void SetCollider()
    {
        _controller.radius = _colliderRadius;
        _controller.height = _colliderHeight;
    }
}
