using UnityEngine;

public class CharacterResolution : MonoBehaviour
{
    public bool isLowDefinition = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetDefinition(string definition)
    {
        /*
            If the definition received is "low", the character is set to low definition, otherwise it is set to high definition
         */
        isLowDefinition = definition == "low";
    }
}
