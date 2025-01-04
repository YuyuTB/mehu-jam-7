using TMPro;
using UnityEngine;

public class HideAttack : MonoBehaviour
{
    private GameObject _player;
    private CharacterResolution _characterResolution;
    private bool _displayAttack;
    
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _characterResolution = _player.GetComponent<CharacterResolution>();
    }
    
    void Update()
    {
        CheckResolution();
        SetVisibleAttack();
    }

    private void CheckResolution()
    {
        _displayAttack = !_characterResolution.isLowDefinition;
    }
    
    private void SetVisibleAttack()
    {
        TextMeshProUGUI[] attackTexts = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI attackText in attackTexts)
        {
            attackText.enabled = _displayAttack;
        }
    }
}
