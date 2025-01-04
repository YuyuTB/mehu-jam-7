using UnityEngine;

public class Soup : MonoBehaviour
{
    [SerializeField] private Sprite soupSprite;
    [SerializeField] private SoupColor soupColor;
    
    private float _scale;
    
    private enum SoupColor { Yellow, Blue }
    private enum SoupEffect { LowDefinition, HighDefinition }
    
    private SpriteRenderer _spriteRenderer;
    private SoupEffect _soupEffect;

    void Start()
    {
        DefineSoupType();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Apply the soup effect to the player
            switch (_soupEffect)
            {
                case SoupEffect.LowDefinition:
                    other.GetComponent<CharacterResolution>().SetDefinition("low");
                    break;
                case SoupEffect.HighDefinition:
                    other.GetComponent<CharacterResolution>().SetDefinition("high");
                    break;
            }
            
            //Destroy the soup
            Destroy(gameObject);
        }
    }
    
    // Defines what type of soup this object is
    private void DefineSoupType()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Defines the base path for the sprites
        string basePath = "Sprites/Collectibles/";
        
        // Sets the soup's properties based on its color
        switch (soupColor)
        {
            // The yellow soup makes the player low definition
            case SoupColor.Yellow:
                soupSprite = Resources.Load<Sprite>(basePath + "yellow_soup");
                _scale = 0.4f;
                _soupEffect = SoupEffect.LowDefinition;
                break;
            // The blue soup makes the player high definition
            case SoupColor.Blue:
                soupSprite = Resources.Load<Sprite>(basePath + "blue_soup");
                _scale = 0.2f;
                _soupEffect = SoupEffect.HighDefinition;
                break;
        }
        
        // Apply the sprite and _scale of the soup
        _spriteRenderer.sprite = soupSprite;
        transform.localScale = new Vector3(_scale, _scale, 1);
    }
    
    
}
