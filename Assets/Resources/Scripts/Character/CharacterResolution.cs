using UnityEngine;

public class CharacterResolution : MonoBehaviour
{
    // Character high definition on initialization
    public bool isLowDefinition;
    
    private float _colliderRadius;
    private float _colliderHeight;

    private BoxCollider2D _boxCollider2D;
    
    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
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
        _colliderRadius = isLowDefinition ? 0.75f : 1.7f;
        _colliderHeight = isLowDefinition ? 1.15f : 2.4f;
    }
    
    private void SetCollider()
    {
        Vector2 newSize = _boxCollider2D.size;
        newSize.x = _colliderRadius;
        newSize.y = _colliderHeight;
        _boxCollider2D.size = newSize;
    }
}
