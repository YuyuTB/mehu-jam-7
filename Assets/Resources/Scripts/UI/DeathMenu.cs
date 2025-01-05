using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    private Button _retryButton;
    private Button _mainMenuButton;
    
    void Start()
    {
        _retryButton = transform.Find("Retry").GetComponent<Button>();
        _mainMenuButton = transform.Find("Menu").GetComponent<Button>();
        
        // Set the initial selected button for gamepad navigation
        EventSystem.current.SetSelectedGameObject(_retryButton.gameObject);
        
        _retryButton.onClick.AddListener(Retry);
        _mainMenuButton.onClick.AddListener(GoToMainMenu);
    }
    
    void Update()
    {
        GameObject selectedButton = EventSystem.current.currentSelectedGameObject;
        
        if (selectedButton == null)
        {
            return;
        }
        
        HandleGamepadInput(selectedButton.GetComponent<Button>());
    }
    
    private void HandleGamepadInput(Button button)
    {
        // Check if the "Submit" button (usually the "A" button on a gamepad) is pressed
        if (Input.GetButtonDown("Submit"))
        {
            // Invoke the button's click event
            button.onClick.Invoke();
        }
    }
    
    private void UnsubscribeAllListeners()
    {
        _retryButton.onClick.RemoveAllListeners();
        _mainMenuButton.onClick.RemoveAllListeners();
    }
    
    private void Retry()
    {
        UnsubscribeAllListeners();
        SceneManager.LoadScene("GameScene");
    }
    
    private void GoToMainMenu()
    {
        UnsubscribeAllListeners();
        SceneManager.LoadScene("MainMenu");
    }
}
